using GameCollectionManager.DTOs;
using GameCollectionManager.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GameCollectionManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController(IGenreService genreService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<GenreResponse>>> GetAll()
    {
        return Ok(await genreService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GenreResponse>> GetById(int id)
    {
        var genre = await genreService.GetByIdAsync(id);

        if (genre is null)
        {
            return NotFound();
        }

        return Ok(genre);
    }

    [HttpPost]
    public async Task<ActionResult<GenreResponse>> Create(
        CreateGenreRequest request)
    {
        var genre = await genreService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = genre.Id },
            genre
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateGenreRequest request)
    {
        var success = await genreService.UpdateAsync(id, request);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await genreService.DeleteAsync(id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}