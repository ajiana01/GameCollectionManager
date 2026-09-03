using GameCollectionManager.Models;

namespace GameCollectionManager.Services.Interface;

public interface IPlatformService
{
    //Create
    Task<Platform> CreateAsync(Platform platform);
    
    //Read
    Task<List<Platform>> GetAllAsync();
    Task<Platform?> GetByIdAsync(int id);
    
    //Update
    Task<bool> UpdateAsync(int id, Platform platform);
    
    //Delete
    Task<bool> DeleteAsync(int id);
}