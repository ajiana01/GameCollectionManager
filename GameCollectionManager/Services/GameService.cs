using GameCollectionManager.Data;
using GameCollectionManager.DTOs;
using GameCollectionManager.Models;
using GameCollectionManager.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace GameCollectionManager.Services;

public class GameService(AppDbContext context): IGameService
{
    public async Task<GameResponse?> CreateAsync(CreateGameRequest request)
    {

        var developer = await context.Developers
            .FirstOrDefaultAsync(d => d.Id == request.DeveloperId);

        if (developer is null)
        {
            return null;
        }

        var genres = await context.Genres
            .Where(g => request.GenreIds.Contains(g.Id))
            .ToListAsync();

        var platforms = await context.Platforms
            .Where(p => request.PlatformIds.Contains(p.Id))
            .ToListAsync();

        var game = new Game
        {
            Title = request.Title,
            Description = request.Description,
            ReleaseYear = request.ReleaseYear,
            DeveloperId = request.DeveloperId,
            Developer = developer,
            Genres = genres,
            Platforms = platforms
        };

        context.Games.Add(game);
        await context.SaveChangesAsync();

        return MapToResponse(game);
    }

    public async Task<List<GameResponse>> GetAllAsync()
    {
        return await context.Games
            .AsNoTracking()
            .Select(game => new GameResponse
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                ReleaseYear = game.ReleaseYear,

                Developer = new DeveloperResponse
                {
                    Id = game.Developer.Id,
                    Name = game.Developer.Name,
                    Location = game.Developer.Location
                },

                Genres = game.Genres
                    .Select(genre => new GenreResponse
                    {
                        Id = genre.Id,
                        Name = genre.Name
                    })
                    .ToList(),

                Platforms = game.Platforms
                    .Select(platform => new PlatformResponse
                    {
                        Id = platform.Id,
                        Name = platform.Name
                    })
                    .ToList()
            })
            .ToListAsync();
    }

    public async Task<GameResponse?> GetByIdAsync(int id)
    {
        return await context.Games
            .AsNoTracking()
            .Where(game => game.Id == id)
            .Select(game => new GameResponse
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                ReleaseYear = game.ReleaseYear,

                Developer = new DeveloperResponse
                {
                    Id = game.Developer.Id,
                    Name = game.Developer.Name,
                    Location = game.Developer.Location
                },

                Genres = game.Genres
                    .Select(genre => new GenreResponse
                    {
                        Id = genre.Id,
                        Name = genre.Name
                    })
                    .ToList(),

                Platforms = game.Platforms
                    .Select(platform => new PlatformResponse
                    {
                        Id = platform.Id,
                        Name = platform.Name
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateAsync(int id, UpdateGameRequest request)
    {
        var existingGame = await context.Games
            .Include(game => game.Genres)
            .Include(game => game.Platforms)
            .FirstOrDefaultAsync(game => game.Id == id);

        if (existingGame is null)
        {
            return false;
        }

        var developerExists = await context.Developers
            .AnyAsync(developer => developer.Id == request.DeveloperId);

        if (!developerExists)
        {
            return false;
        }

        var genres = await context.Genres
            .Where(game => request.GenreIds.Contains(game.Id))
            .ToListAsync();

        var platforms = await context.Platforms
            .Where(platform => request.PlatformIds.Contains(platform.Id))
            .ToListAsync();

        existingGame.Title = request.Title;
        existingGame.Description = request.Description;
        existingGame.ReleaseYear = request.ReleaseYear;
        existingGame.DeveloperId = request.DeveloperId;

        existingGame.Genres.Clear();

        foreach (var genre in genres)
        {
            existingGame.Genres.Add(genre);
        }

        existingGame.Platforms.Clear();

        foreach (var platform in platforms)
        {
            existingGame.Platforms.Add(platform);
        }

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var game = await context.Games
            .FindAsync(id);

        if (game is null)
        {
            return false;
        }

        context.Games.Remove(game);

        await context.SaveChangesAsync();

        return true;
    }
    
    private static GameResponse MapToResponse(Game game)
    {
        return new GameResponse
        {
            Id = game.Id,
            Title = game.Title,
            Description = game.Description,
            ReleaseYear = game.ReleaseYear,

            Developer = new DeveloperResponse
            {
                Id = game.Developer.Id,
                Name = game.Developer.Name,
                Location = game.Developer.Location
            },

            Genres = game.Genres
                .Select(genre => new GenreResponse
                {
                    Id = genre.Id,
                    Name = genre.Name
                })
                .ToList(),

            Platforms = game.Platforms
                .Select(platform => new PlatformResponse
                {
                    Id = platform.Id,
                    Name = platform.Name
                })
                .ToList()
        };
    }
}