using GameCollectionManager.Data;
using GameCollectionManager.DTOs;
using GameCollectionManager.Models;
using GameCollectionManager.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace GameCollectionManager.Services;

public class GameService(AppDbContext context) : IGameService
{
    public async Task<ApiResponseDto<GameDto>> CreateAsync(CreateGameDto createGameDto, string userId)
    {
        try
        {
            var developer = await context.Developers
                .FirstOrDefaultAsync(d => d.Id == createGameDto.DeveloperId && d.UserId == userId);

            if (developer is null)
            {
                return ApiResponseDto<GameDto>.ErrorResult("Developer not found");
            }

            var genres = await context.Genres
                .Where(g => createGameDto.GenreIds.Contains(g.Id))
                .ToListAsync();

            var platforms = await context.Platforms
                .Where(p => createGameDto.PlatformIds.Contains(p.Id))
                .ToListAsync();

            var game = new Game
            {
                Title = createGameDto.Title,
                Description = createGameDto.Description,
                ReleaseYear = createGameDto.ReleaseYear,
                Developer = developer,
                Genres = genres,
                Platforms = platforms
            };

            context.Games.Add(game);
            await context.SaveChangesAsync();

            return ApiResponseDto<GameDto>.SuccessResult(MapToDto(game), "Game created successfully");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<GameDto>.ErrorResult($"Error creating game: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<List<GameDto>>> GetAllAsync(string userId)
    {
        try
        {
            var games = await context.Games
                .AsNoTracking()
                .Include(game => game.Developer)
                .Include(game => game.Genres)
                .Include(game => game.Platforms)
                .Where(game => game.Developer.UserId == userId)
                .ToListAsync();

            return ApiResponseDto<List<GameDto>>.SuccessResult(games.Select(MapToDto).ToList());
        }
        catch (Exception ex)
        {
            return ApiResponseDto<List<GameDto>>.ErrorResult($"Error retrieving games: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<GameDto>?> GetByIdAsync(int id, string userId)
    {
        try
        {
            var game = await context.Games
                .AsNoTracking()
                .Include(game => game.Developer)
                .Include(game => game.Genres)
                .Include(game => game.Platforms)
                .FirstOrDefaultAsync(game => game.Id == id && game.Developer.UserId == userId);

            return game is null
                ? ApiResponseDto<GameDto>.ErrorResult("Game not found")
                : ApiResponseDto<GameDto>.SuccessResult(MapToDto(game));
        }
        catch (Exception ex)
        {
            return ApiResponseDto<GameDto>.ErrorResult($"Error retrieving game: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<GameDto>> UpdateAsync(int id, UpdateGameDto updateGameDto, string userId)
    {
        try
        {
            var game = await context.Games
                .Include(existingGame => existingGame.Developer)
                .Include(existingGame => existingGame.Genres)
                .Include(existingGame => existingGame.Platforms)
                .FirstOrDefaultAsync(existingGame => existingGame.Id == id && existingGame.Developer.UserId == userId);

            if (game is null)
            {
                return ApiResponseDto<GameDto>.ErrorResult("Game not found");
            }

            var developer = await context.Developers
                .FirstOrDefaultAsync(d => d.Id == updateGameDto.DeveloperId && d.UserId == userId);

            if (developer is null)
            {
                return ApiResponseDto<GameDto>.ErrorResult("Developer not found");
            }

            var genres = await context.Genres
                .Where(g => updateGameDto.GenreIds.Contains(g.Id))
                .ToListAsync();

            var platforms = await context.Platforms
                .Where(p => updateGameDto.PlatformIds.Contains(p.Id))
                .ToListAsync();

            game.Title = updateGameDto.Title;
            game.Description = updateGameDto.Description;
            game.ReleaseYear = updateGameDto.ReleaseYear;
            game.Developer = developer;
            game.Genres = genres;
            game.Platforms = platforms;

            await context.SaveChangesAsync();

            return ApiResponseDto<GameDto>.SuccessResult(MapToDto(game), "Game updated successfully");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<GameDto>.ErrorResult($"Error updating game: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<object>> DeleteAsync(int id, string userId)
    {
        try
        {
            var game = await context.Games
                .Include(existingGame => existingGame.Developer)
                .FirstOrDefaultAsync(existingGame => existingGame.Id == id && existingGame.Developer.UserId == userId);

            if (game is null)
            {
                return ApiResponseDto<object>.ErrorResult("Game not found");
            }

            context.Games.Remove(game);
            await context.SaveChangesAsync();

            return ApiResponseDto<object>.SuccessResult(new object(), "Game deleted successfully");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<object>.ErrorResult($"Error deleting game: {ex.Message}");
        }
    }

    private static GameDto MapToDto(Game game)
    {
        return new GameDto
        {
            Id = game.Id,
            Title = game.Title,
            Description = game.Description,
            ReleaseYear = game.ReleaseYear,
            Developer = new DeveloperDto
            {
                Id = game.Developer.Id,
                Name = game.Developer.Name,
                Location = game.Developer.Location,
                CreatedAt = game.Developer.CreatedAt
            },
            Genres = game.Genres
                .Select(genre => new GenreDto { Id = genre.Id, Name = genre.Name })
                .ToList(),
            Platforms = game.Platforms
                .Select(platform => new PlatformDto { Id = platform.Id, Name = platform.Name })
                .ToList()
        };
    }
}
