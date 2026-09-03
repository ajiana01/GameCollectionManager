using GameCollectionManager.Data;
using GameCollectionManager.DTOs;
using GameCollectionManager.Models;
using GameCollectionManager.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace GameCollectionManager.Services;

public class DeveloperService(AppDbContext context) : IDeveloperService
{
    public async Task<DeveloperResponse> CreateAsync(CreateDeveloperRequest request)
    {
        var developer = new Developer
        {
            Name = request.Name,
            Location = request.Location
        };

        context.Developers.Add(developer);
        await context.SaveChangesAsync();

        return MapToResponse(developer);
    }

    public async Task<List<DeveloperResponse>> GetAllAsync()
    {
        return await context.Developers
            .AsNoTracking()
            .Select(d => new DeveloperResponse
            {
                Id = d.Id,
                Name = d.Name,
                Location = d.Location
            })
            .ToListAsync();
    }

    public async Task<DeveloperResponse?> GetByIdAsync(int id)
    {
        return await context.Developers
            .AsNoTracking()
            .Where(d => d.Id == id)
            .Select(d => new DeveloperResponse
            {
                Id = d.Id,
                Name = d.Name,
                Location = d.Location
            })
            .FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateAsync(int id, UpdateDeveloperRequest request)
    {
        var existingDeveloper =
            await context.Developers.FindAsync(id);

        if (existingDeveloper is null)
        {
            return false;
        }

        existingDeveloper.Name = request.Name;
        existingDeveloper.Location = request.Location;

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var developer =
            await context.Developers.FindAsync(id);

        if (developer is null)
        {
            return false;
        }

        context.Developers.Remove(developer);

        await context.SaveChangesAsync();

        return true;
    }
    
    private static DeveloperResponse MapToResponse(Developer developer)
    {
        return new DeveloperResponse
        {
            Id = developer.Id,
            Name = developer.Name,
            Location = developer.Location
        };
    }
}