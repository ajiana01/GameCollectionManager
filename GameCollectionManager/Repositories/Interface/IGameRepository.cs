using GameCollectionManager.Models;

namespace GameCollectionManager.Repositories.Interface;

public interface IGameRepository
{
    //Create
    Task<Game> CreateAsync(Game game);
    
    //Read
    Task<List<Game>> GetAllAsync(int developerId);
    Task<Game?> GetByIdAsync(int id, int developerId);
    
    //Update
    Task<Game> UpdateAsync(Game game);
    
    //Delete
    Task DeleteAsync(int id, int developerId);
}
