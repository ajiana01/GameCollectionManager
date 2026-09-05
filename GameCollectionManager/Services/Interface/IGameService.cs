using GameCollectionManager.DTOs;

namespace GameCollectionManager.Services.Interface;

public interface IGameService
{
    //Create
    Task<ApiResponseDto<GameDto>> CreateAsync(CreateGameDto createGameDto, string userId);
    
    //Read
    Task<ApiResponseDto<List<GameDto>>> GetAllAsync(string userId);
    Task<ApiResponseDto<GameDto>?> GetByIdAsync(int id, string userId);
    
    //Update
    Task<ApiResponseDto<GameDto>> UpdateAsync(int id, UpdateGameDto updateGameDto, string userId);
    
    //Delete
    Task<ApiResponseDto<object>> DeleteAsync(int id, string userId);
}
