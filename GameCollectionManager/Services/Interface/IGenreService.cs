using GameCollectionManager.Models;

namespace GameCollectionManager.Services.Interface;

public interface IGenreService
{
    //Create
    Task<Genre> CreateAsync(Genre genre);
    
    //Read
    Task<List<Genre>> GetAllAsync();
    Task<Genre?> GetByIdAsync(int id);
    
    //Update
    Task<bool> UpdateAsync(int id, Genre genre);
    
    //Delete
    Task<bool> DeleteAsync(int id);
}