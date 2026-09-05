using GameCollectionManager.Data;
using GameCollectionManager.Models;
using GameCollectionManager.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace GameCollectionManager.Repositories;

public class GenreRepository(AppDbContext context) : IGenreRepository
{
    public async Task<Genre> CreateAsync(Genre genre)
    {
        context.Genres.Add(genre);
        await context.SaveChangesAsync();
        return genre;
    }

    public async Task<List<Genre>> GetAllAsync()
    {
        return await context.Genres.OrderBy(genre => genre.Name).ToListAsync();
    }

    public async Task<Genre?> GetByIdAsync(int id)
    {
        return await context.Genres.FirstOrDefaultAsync(genre => genre.Id == id);
    }

    public async Task<Genre> UpdateAsync(Genre genre)
    {
        context.Genres.Update(genre);
        await context.SaveChangesAsync();
        return genre;
    }

    public async Task DeleteAsync(int id)
    {
        var genre = await context.Genres.FindAsync(id);
        if (genre != null)
        {
            context.Genres.Remove(genre);
            await context.SaveChangesAsync();
        }
    }
}