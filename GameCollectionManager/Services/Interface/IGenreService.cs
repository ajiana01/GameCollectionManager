using GameCollectionManager.DTOs;
namespace GameCollectionManager.Services.Interface;

public interface IGenreService
{
    //Create
    Task<GenreResponse> CreateAsync(CreateGenreRequest request);
    
    //Read
    Task<List<GenreResponse>> GetAllAsync();
    Task<GenreResponse?> GetByIdAsync(int id);
    
    //Update
    Task<bool> UpdateAsync(int id, UpdateGenreRequest request);
    
    //Delete
    Task<bool> DeleteAsync(int id);
}