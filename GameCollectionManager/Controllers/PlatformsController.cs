using FluentValidation;
using GameCollectionManager.DTOs;
using GameCollectionManager.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameCollectionManager.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PlatformsController(
    IPlatformService platformService,
    IValidator<CreatePlatformDto> createValidator,
    IValidator<UpdatePlatformDto> updateValidator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await platformService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await platformService.GetByIdAsync(id);
        return result is { Success: true } ? Ok(result) : NotFound(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlatformDto createPlatformDto)
    {
        var validationResult = await createValidator.ValidateAsync(createPlatformDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            return BadRequest(ApiResponseDto<PlatformDto>.ErrorResult("Validation failed", errors));
        }

        var result = await platformService.CreateAsync(createPlatformDto);
        return result.Success
            ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result)
            : BadRequest(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePlatformDto updatePlatformDto)
    {
        var validationResult = await updateValidator.ValidateAsync(updatePlatformDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            return BadRequest(ApiResponseDto<PlatformDto>.ErrorResult("Validation failed", errors));
        }

        var result = await platformService.UpdateAsync(id, updatePlatformDto);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await platformService.DeleteAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }
}
