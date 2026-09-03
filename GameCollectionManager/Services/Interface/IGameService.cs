using GameCollectionManager.Models;

namespace GameCollectionManager.Services.Interface;

public interface IGameService
{
    //Create
    Task<Game> CreateAsync(Game game);
    
    //Read
    Task<List<Game>> GetAllAsync();
    Task<Game?> GetByIdAsync(int id);
    
    //Update
    Task<bool> UpdateAsync(int id, Game game);
    
    //Delete
    Task<bool> DeleteAsync(int id);
}