using GameCollectionManager.Models;

namespace GameCollectionManager.Services.Interface;

public interface IDeveloperService
{
    //Create
    Task<Developer> CreateAsync(Developer developer);
    
    //Read
    Task<List<Developer>> GetAllAsync();
    Task<Developer?> GetByIdAsync(int id);
    
    //Update
    Task<bool> UpdateAsync(int id, Developer developer);
    
    //Delete
    Task<bool> DeleteAsync(int id);
}