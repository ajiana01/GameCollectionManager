using FluentValidation;
using GameCollectionManager.DTOs;
using GameCollectionManager.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameCollectionManager.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GenresController(
    IGenreService genreService,
    IValidator<CreateGenreDto> createValidator,
    IValidator<UpdateGenreDto> updateValidator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await genreService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await genreService.GetByIdAsync(id);
        return result is { Success: true } ? Ok(result) : NotFound(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGenreDto createGenreDto)
    {
        var validationResult = await createValidator.ValidateAsync(createGenreDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            return BadRequest(ApiResponseDto<GenreDto>.ErrorResult("Validation failed", errors));
        }

        var result = await genreService.CreateAsync(createGenreDto);
        return result.Success
            ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result)
            : BadRequest(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateGenreDto updateGenreDto)
    {
        var validationResult = await updateValidator.ValidateAsync(updateGenreDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            return BadRequest(ApiResponseDto<GenreDto>.ErrorResult("Validation failed", errors));
        }

        var result = await genreService.UpdateAsync(id, updateGenreDto);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await genreService.DeleteAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }
}
