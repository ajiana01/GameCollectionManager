using GameCollectionManager.Data;
using GameCollectionManager.DTOs;
using GameCollectionManager.Models;
using GameCollectionManager.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace GameCollectionManager.Services;

public class PlatformService(AppDbContext context) : IPlatformService
{
    public async Task<ApiResponseDto<PlatformDto>> CreateAsync(CreatePlatformDto createPlatformDto)
    {
        try
        {
            var platform = new Platform { Name = createPlatformDto.Name };
            context.Platforms.Add(platform);
            await context.SaveChangesAsync();

            return ApiResponseDto<PlatformDto>.SuccessResult(MapToDto(platform), "Platform created successfully");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<PlatformDto>.ErrorResult($"Error creating platform: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<List<PlatformDto>>> GetAllAsync()
    {
        try
        {
            var platforms = await context.Platforms.AsNoTracking().ToListAsync();
            return ApiResponseDto<List<PlatformDto>>.SuccessResult(platforms.Select(MapToDto).ToList());
        }
        catch (Exception ex)
        {
            return ApiResponseDto<List<PlatformDto>>.ErrorResult($"Error retrieving platforms: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<PlatformDto>?> GetByIdAsync(int id)
    {
        try
        {
            var platform = await context.Platforms.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            return platform is null
                ? ApiResponseDto<PlatformDto>.ErrorResult("Platform not found")
                : ApiResponseDto<PlatformDto>.SuccessResult(MapToDto(platform));
        }
        catch (Exception ex)
        {
            return ApiResponseDto<PlatformDto>.ErrorResult($"Error retrieving platform: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<PlatformDto>> UpdateAsync(int id, UpdatePlatformDto updatePlatformDto)
    {
        try
        {
            var platform = await context.Platforms.FindAsync(id);
            if (platform is null)
            {
                return ApiResponseDto<PlatformDto>.ErrorResult("Platform not found");
            }

            platform.Name = updatePlatformDto.Name;
            await context.SaveChangesAsync();

            return ApiResponseDto<PlatformDto>.SuccessResult(MapToDto(platform), "Platform updated successfully");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<PlatformDto>.ErrorResult($"Error updating platform: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<object>> DeleteAsync(int id)
    {
        try
        {
            var platform = await context.Platforms.FindAsync(id);
            if (platform is null)
            {
                return ApiResponseDto<object>.ErrorResult("Platform not found");
            }

            context.Platforms.Remove(platform);
            await context.SaveChangesAsync();

            return ApiResponseDto<object>.SuccessResult(new object(), "Platform deleted successfully");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<object>.ErrorResult($"Error deleting platform: {ex.Message}");
        }
    }

    private static PlatformDto MapToDto(Platform platform)
    {
        return new PlatformDto { Id = platform.Id, Name = platform.Name };
    }
}
