using GameCollectionManager.Data;
using GameCollectionManager.Models;
using GameCollectionManager.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace GameCollectionManager.Services;

public class PlatformService(AppDbContext context): IPlatformService
{
    public async Task<Platform> CreateAsync(Platform platform)
    {
        context.Platforms.Add(platform);

        await context.SaveChangesAsync();

        return platform;
    }

    public async Task<List<Platform>> GetAllAsync()
    {
        return await context.Platforms
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Platform?> GetByIdAsync(int id)
    {
        return await context.Platforms
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<bool> UpdateAsync(int id, Platform platform)
    {
        var existingPlatform =
            await context.Platforms.FindAsync(id);

        if (existingPlatform == null)
        {
            return false;
        }

        existingPlatform.Name = platform.Name;

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var platform =
            await context.Platforms.FindAsync(id);

        if (platform == null)
        {
            return false;
        }

        context.Platforms.Remove(platform);

        await context.SaveChangesAsync();

        return true;
    }
}