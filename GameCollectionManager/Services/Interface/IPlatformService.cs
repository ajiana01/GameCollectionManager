using GameCollectionManager.DTOs;

namespace GameCollectionManager.Services.Interface;

public interface IPlatformService
{
    //Create
    Task<PlatformResponse> CreateAsync(CreatePlatformRequest request);
    
    //Read
    Task<List<PlatformResponse>> GetAllAsync();
    Task<PlatformResponse?> GetByIdAsync(int id);
    
    //Update
    Task<bool> UpdateAsync(int id, UpdatePlatformRequest request);
    
    //Delete
    Task<bool> DeleteAsync(int id);
}