using GameCollectionManager.DTOs;

namespace GameCollectionManager.Services.Interface;

public interface IDeveloperService
{
    //Create
    Task<ApiResponseDto<DeveloperDto>> CreateAsync(CreateDeveloperDto createDeveloperDto, string userId);
    
    //Read
    Task<ApiResponseDto<List<DeveloperDto>>> GetAllAsync(string userId);
    Task<ApiResponseDto<DeveloperDto>?> GetByIdAsync(int id, string userId);
    
    //Update
    Task<ApiResponseDto<DeveloperDto>> UpdateAsync(int id, UpdateDeveloperDto updateDeveloperDto, string userId);
    
    //Delete
    Task<ApiResponseDto<object>> DeleteAsync(int id, string userId);
}