using GameCollectionManager.DTOs;
namespace GameCollectionManager.Services.Interface;

public interface IGenreService
{
    //Create
    Task<ApiResponseDto<GenreDto>> CreateAsync(CreateGenreDto createGenreDto);
    
    //Read
    Task<ApiResponseDto<List<GenreDto>>> GetAllAsync();
    Task<ApiResponseDto<GenreDto>?> GetByIdAsync(int id);
    
    //Update
    Task<ApiResponseDto<GenreDto>> UpdateAsync(int id, UpdateGenreDto updateGenreDto);
    
    //Delete
    Task<ApiResponseDto<object>> DeleteAsync(int id);
}
