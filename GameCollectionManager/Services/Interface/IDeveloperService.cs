using GameCollectionManager.DTOs;

namespace GameCollectionManager.Services.Interface;

public interface IDeveloperService
{
    //Create
    Task<DeveloperResponse> CreateAsync(CreateDeveloperRequest request);
    
    //Read
    Task<List<DeveloperResponse>> GetAllAsync();
    Task<DeveloperResponse?> GetByIdAsync(int id);
    
    //Update
    Task<bool> UpdateAsync(int id, UpdateDeveloperRequest request);
    
    //Delete
    Task<bool> DeleteAsync(int id);
}