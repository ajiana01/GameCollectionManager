using GameCollectionManager.Data;
using GameCollectionManager.Models;
using GameCollectionManager.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace GameCollectionManager.Services;

public class DeveloperService(AppDbContext context) : IDeveloperService
{
    public async Task<Developer> CreateAsync(Developer developer)
    {
        context.Developers.Add(developer);
        await context.SaveChangesAsync();
        return developer;
    }

    public async Task<List<Developer>> GetAllAsync()
    {
        return await context.Developers
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Developer?> GetByIdAsync(int id)
    {
        return await context.Developers
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<bool> UpdateAsync(int id, Developer developer)
    {
        var existingDeveloper =
            await context.Developers.FindAsync(id);

        if (existingDeveloper == null)
        {
            return false;
        }

        existingDeveloper.Name = developer.Name;
        existingDeveloper.Location = developer.Location;
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var developer =
            await context.Developers.FindAsync(id);

        if (developer == null)
        {
            return false;
        }

        context.Developers.Remove(developer);
        await context.SaveChangesAsync();

        return true;
    }
}