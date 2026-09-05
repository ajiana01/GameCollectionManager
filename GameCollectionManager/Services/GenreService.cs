using GameCollectionManager.Data;
using GameCollectionManager.DTOs;
using GameCollectionManager.Models;
using GameCollectionManager.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace GameCollectionManager.Services;

public class GenreService(AppDbContext context) : IGenreService
{
    public async Task<ApiResponseDto<GenreDto>> CreateAsync(CreateGenreDto createGenreDto)
    {
        try
        {
            var genre = new Genre { Name = createGenreDto.Name };
            context.Genres.Add(genre);
            await context.SaveChangesAsync();

            return ApiResponseDto<GenreDto>.SuccessResult(MapToDto(genre), "Genre created successfully");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<GenreDto>.ErrorResult($"Error creating genre: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<List<GenreDto>>> GetAllAsync()
    {
        try
        {
            var genres = await context.Genres.AsNoTracking().ToListAsync();
            return ApiResponseDto<List<GenreDto>>.SuccessResult(genres.Select(MapToDto).ToList());
        }
        catch (Exception ex)
        {
            return ApiResponseDto<List<GenreDto>>.ErrorResult($"Error retrieving genres: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<GenreDto>?> GetByIdAsync(int id)
    {
        try
        {
            var genre = await context.Genres.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id);
            return genre is null
                ? ApiResponseDto<GenreDto>.ErrorResult("Genre not found")
                : ApiResponseDto<GenreDto>.SuccessResult(MapToDto(genre));
        }
        catch (Exception ex)
        {
            return ApiResponseDto<GenreDto>.ErrorResult($"Error retrieving genre: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<GenreDto>> UpdateAsync(int id, UpdateGenreDto updateGenreDto)
    {
        try
        {
            var genre = await context.Genres.FindAsync(id);
            if (genre is null)
            {
                return ApiResponseDto<GenreDto>.ErrorResult("Genre not found");
            }

            genre.Name = updateGenreDto.Name;
            await context.SaveChangesAsync();

            return ApiResponseDto<GenreDto>.SuccessResult(MapToDto(genre), "Genre updated successfully");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<GenreDto>.ErrorResult($"Error updating genre: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<object>> DeleteAsync(int id)
    {
        try
        {
            var genre = await context.Genres.FindAsync(id);
            if (genre is null)
            {
                return ApiResponseDto<object>.ErrorResult("Genre not found");
            }

            context.Genres.Remove(genre);
            await context.SaveChangesAsync();

            return ApiResponseDto<object>.SuccessResult(new object(), "Genre deleted successfully");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<object>.ErrorResult($"Error deleting genre: {ex.Message}");
        }
    }

    private static GenreDto MapToDto(Genre genre)
    {
        return new GenreDto { Id = genre.Id, Name = genre.Name };
    }
}
