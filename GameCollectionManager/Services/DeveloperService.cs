using AutoMapper;
using GameCollectionManager.Data;
using GameCollectionManager.DTOs;
using GameCollectionManager.Models;
using GameCollectionManager.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace GameCollectionManager.Services;

public class DeveloperService(AppDbContext context, IMapper mapper) : IDeveloperService
{
    public async Task<ApiResponseDto<DeveloperDto>> CreateAsync(CreateDeveloperDto createDeveloperDto, string userId)
    {
        try
        {
            var developerExists = await context.Developers
                .AnyAsync(developer => developer.Name == createDeveloperDto.Name);

            if (developerExists)
            {
                return ApiResponseDto<DeveloperDto>.ErrorResult("A developer with this name already exists");
            }

            var developer = mapper.Map<Developer>(createDeveloperDto);
            developer.UserId = userId;

            context.Developers.Add(developer);
            await context.SaveChangesAsync();

            return ApiResponseDto<DeveloperDto>.SuccessResult(
                mapper.Map<DeveloperDto>(developer),
                "Developer created successfully");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<DeveloperDto>.ErrorResult($"Error creating developer: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<List<DeveloperDto>>> GetAllAsync(string userId)
    {
        try
        {
            var developers = await context.Developers
                .AsNoTracking()
                .Where(developer => developer.UserId == userId)
                .OrderBy(developer => developer.Name)
                .ToListAsync();

            return ApiResponseDto<List<DeveloperDto>>.SuccessResult(
                mapper.Map<List<DeveloperDto>>(developers));
        }
        catch (Exception ex)
        {
            return ApiResponseDto<List<DeveloperDto>>.ErrorResult($"Error retrieving developers: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<DeveloperDto>?> GetByIdAsync(int id, string userId)
    {
        try
        {
            var developer = await context.Developers
                .AsNoTracking()
                .FirstOrDefaultAsync(item => item.Id == id && item.UserId == userId);

            return developer is null
                ? ApiResponseDto<DeveloperDto>.ErrorResult("Developer not found")
                : ApiResponseDto<DeveloperDto>.SuccessResult(mapper.Map<DeveloperDto>(developer));
        }
        catch (Exception ex)
        {
            return ApiResponseDto<DeveloperDto>.ErrorResult($"Error retrieving developer: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<DeveloperDto>> UpdateAsync(
        int id,
        UpdateDeveloperDto updateDeveloperDto,
        string userId)
    {
        try
        {
            var developer = await context.Developers
                .FirstOrDefaultAsync(item => item.Id == id && item.UserId == userId);

            if (developer is null)
            {
                return ApiResponseDto<DeveloperDto>.ErrorResult("Developer not found");
            }

            var duplicateNameExists = await context.Developers
                .AnyAsync(item => item.Id != id && item.Name == updateDeveloperDto.Name);

            if (duplicateNameExists)
            {
                return ApiResponseDto<DeveloperDto>.ErrorResult("A developer with this name already exists");
            }

            mapper.Map(updateDeveloperDto, developer);
            await context.SaveChangesAsync();

            return ApiResponseDto<DeveloperDto>.SuccessResult(
                mapper.Map<DeveloperDto>(developer),
                "Developer updated successfully");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<DeveloperDto>.ErrorResult($"Error updating developer: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto<object>> DeleteAsync(int id, string userId)
    {
        try
        {
            var developer = await context.Developers
                .FirstOrDefaultAsync(item => item.Id == id && item.UserId == userId);

            if (developer is null)
            {
                return ApiResponseDto<object>.ErrorResult("Developer not found");
            }

            context.Developers.Remove(developer);
            await context.SaveChangesAsync();

            return ApiResponseDto<object>.SuccessResult(new object(), "Developer deleted successfully");
        }
        catch (Exception ex)
        {
            return ApiResponseDto<object>.ErrorResult($"Error deleting developer: {ex.Message}");
        }
    }
}
