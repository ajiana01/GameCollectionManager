using GameCollectionManager.Data;
using GameCollectionManager.Models;
using GameCollectionManager.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace GameCollectionManager.Services;

public class GenreService(AppDbContext context) : IGenreService
{
    public async Task<Genre> CreateAsync(Genre genre)
    {
        context.Genres.Add(genre);

        await context.SaveChangesAsync();

        return genre;
    }

    public async Task<List<Genre>> GetAllAsync()
    {
        return await context.Genres
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Genre?> GetByIdAsync(int id)
    {
        return await context.Genres
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<bool> UpdateAsync(int id, Genre genre)
    {
        var existingGenre =
            await context.Genres.FindAsync(id);

        if (existingGenre == null)
        {
            return false;
        }

        existingGenre.Name = genre.Name;

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var genre =
            await context.Genres.FindAsync(id);

        if (genre == null)
        {
            return false;
        }

        context.Genres.Remove(genre);

        await context.SaveChangesAsync();

        return true;
    }
}