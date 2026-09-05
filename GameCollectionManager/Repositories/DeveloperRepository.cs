using GameCollectionManager.Data;
using GameCollectionManager.Models;
using GameCollectionManager.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace GameCollectionManager.Repositories;

public class DeveloperRepository(AppDbContext context) : IDeveloperRepository
{
    public async Task<Developer> CreateAsync(Developer developer)
    {
        context.Developers.Add(developer);
        await context.SaveChangesAsync();
        return developer;
    }

    public async Task<List<Developer>> GetAllAsync(string userId)
    {
        return await context.Developers
            .Include(developer => developer.User)
            .Where(developer => developer.UserId == userId )
            .ToListAsync();
    }

    public async Task<Developer?> GetByIdAsync(int id, string userId)
    {
        return await context.Developers
            .Include(developer => developer.User)
            .FirstOrDefaultAsync(developer => developer.Id == id && developer.UserId == userId);
    }

    public async Task<Developer> UpdateAsync(Developer developer)
    {
        context.Developers.Update(developer);
        await context.SaveChangesAsync();
        return developer;
    }

    public async Task DeleteAsync(int id, string userId)
    {
        var developer = await GetByIdAsync(id, userId);
        if (developer != null)
        {
            context.Developers.Remove(developer);
            await context.SaveChangesAsync();
        }
    }
}