using GameCollectionManager.Models;

namespace GameCollectionManager.Repositories.Interface;

public interface IPlatfromRepository
{
    //Create
    Task<Platform> CreateAsync(Platform platform);
    
    //Read
    Task<List<Platform>> GetAllAsync();
    Task<Platform?> GetByIdAsync(int id);
    
    //Update
    Task<Platform> UpdateAsync(Platform platform);
    
    //Delete
    Task DeleteAsync(int id);
}