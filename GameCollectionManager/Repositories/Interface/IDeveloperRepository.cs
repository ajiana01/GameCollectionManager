using GameCollectionManager.DTOs;
using GameCollectionManager.Models;

namespace GameCollectionManager.Repositories.Interface;

public interface IDeveloperRepository
{
    //Create
    Task<Developer> CreateAsync(Developer developer);
    
    //Read
    Task<List<Developer>> GetAllAsync(string userId);
    Task<Developer?> GetByIdAsync(int id, string userId);
    
    //Update
    Task<Developer> UpdateAsync(Developer developer);
    
    //Delete
    Task DeleteAsync(int id, string userId);
}