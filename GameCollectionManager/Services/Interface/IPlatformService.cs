using GameCollectionManager.DTOs;

namespace GameCollectionManager.Services.Interface;

public interface IPlatformService
{
    //Create
    Task<ApiResponseDto<PlatformDto>> CreateAsync(CreatePlatformDto createPlatformDto);
    
    //Read
    Task<ApiResponseDto<List<PlatformDto>>> GetAllAsync();
    Task<ApiResponseDto<PlatformDto>?> GetByIdAsync(int id);
    
    //Update
    Task<ApiResponseDto<PlatformDto>> UpdateAsync(int id, UpdatePlatformDto updatePlatformDto);
    
    //Delete
    Task<ApiResponseDto<object>> DeleteAsync(int id);
}
