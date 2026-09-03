using GameCollectionManager.DTOs;

namespace GameCollectionManager.Services.Interface;

public interface IGameService
{
    //Create
    Task<GameResponse?> CreateAsync(CreateGameRequest request);
    
    //Read
    Task<List<GameResponse>> GetAllAsync();
    Task<GameResponse?> GetByIdAsync(int id);
    
    //Update
    Task<bool> UpdateAsync(int id, UpdateGameRequest request);
    
    //Delete
    Task<bool> DeleteAsync(int id);
}