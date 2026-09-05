using GameCollectionManager.Data;
using GameCollectionManager.Models;
using GameCollectionManager.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace GameCollectionManager.Repositories;

public class PlatformRepository(AppDbContext  context) : IPlatfromRepository
{
    public async Task<Platform> CreateAsync(Platform platform)
    {
        context.Platforms.Add(platform);
        await context.SaveChangesAsync();
        return platform;
    }

    public async Task<List<Platform>> GetAllAsync()
    {
        return await context.Platforms.OrderBy(p => p.Name).ToListAsync();
    }

    public Task<Platform?> GetByIdAsync(int id)
    {
        return context.Platforms.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Platform> UpdateAsync(Platform platform)
    {
        context.Platforms.Update(platform);
        await context.SaveChangesAsync();
        return platform;
    }

    public async Task DeleteAsync(int id)
    {
        var platform = await GetByIdAsync(id);
        if (platform != null)
        {
            context.Platforms.Remove(platform);
            await context.SaveChangesAsync();
        }
    }
}