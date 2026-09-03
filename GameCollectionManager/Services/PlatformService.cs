using GameCollectionManager.Data;
using GameCollectionManager.DTOs;
using GameCollectionManager.Models;
using GameCollectionManager.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace GameCollectionManager.Services;

public class PlatformService(AppDbContext context): IPlatformService
{
    public async Task<PlatformResponse> CreateAsync(CreatePlatformRequest request)
    {
        var platform = new Platform
        {
            Name = request.Name
        };

        context.Platforms.Add(platform);
        await context.SaveChangesAsync();

        return MapToResponse(platform);
    }

    public async Task<List<PlatformResponse>> GetAllAsync()
    {
        var platforms = await context.Platforms
            .AsNoTracking()
            .ToListAsync();

        return platforms
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<PlatformResponse?> GetByIdAsync(int id)
    {
        var platform = await context.Platforms
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (platform is null)
        {
            return null;
        }

        return MapToResponse(platform);
    }

    public async Task<bool> UpdateAsync(int id, UpdatePlatformRequest request)
    {
        var existingPlatform = await context.Platforms.FindAsync(id);

        if (existingPlatform is null)
        {
            return false;
        }

        existingPlatform.Name = request.Name;

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var platform = await context.Platforms.FindAsync(id);

        if (platform is null)
        {
            return false;
        }

        context.Platforms.Remove(platform);

        await context.SaveChangesAsync();

        return true;
    }
    
    private static PlatformResponse MapToResponse(Platform platform)
    {
        return new PlatformResponse
        {
            Id = platform.Id,
            Name = platform.Name
        };
    }
}