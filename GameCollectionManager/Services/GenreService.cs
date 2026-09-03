using GameCollectionManager.Data;
using GameCollectionManager.DTOs;
using GameCollectionManager.Models;
using GameCollectionManager.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace GameCollectionManager.Services;

public class GenreService(AppDbContext context) : IGenreService
{
    public async Task<GenreResponse> CreateAsync(CreateGenreRequest request)
    {
        var genre = new Genre
        {
            Name = request.Name
        };

        context.Genres.Add(genre);
        await context.SaveChangesAsync();

        return MapToResponse(genre);
    }

    public async Task<List<GenreResponse>> GetAllAsync()
    {
        var genres = await context.Genres
            .AsNoTracking()
            .ToListAsync();

        return genres
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<GenreResponse?> GetByIdAsync(int id)
    {
        var genre = await context.Genres
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Id == id);

        if (genre is null)
        {
            return null;
        }

        return MapToResponse(genre);
    }

    public async Task<bool> UpdateAsync(int id, UpdateGenreRequest request)
    {
        var existingGenre = await context.Genres.FindAsync(id);

        if (existingGenre is null)
        {
            return false;
        }

        existingGenre.Name = request.Name;

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var genre = await context.Genres.FindAsync(id);

        if (genre is null)
        {
            return false;
        }

        context.Genres.Remove(genre);

        await context.SaveChangesAsync();

        return true;
    }
    
    private static GenreResponse MapToResponse(Genre genre)
    {
        return new GenreResponse
        {
            Id = genre.Id,
            Name = genre.Name
        };
    }
}