using GameCollectionManager.Models;

namespace GameCollectionManager.Repositories.Interface;

public interface IGenreRepository
{
    //Create
    Task<Genre> CreateAsync(Genre genre);
    
    //Read
    Task<List<Genre>> GetAllAsync();
    Task<Genre?> GetByIdAsync(int id);
    
    //Update
    Task<Genre> UpdateAsync(Genre genre);
    
    //Delete
    Task DeleteAsync(int id);
}