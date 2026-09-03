using GameCollectionManager.DTOs;
using GameCollectionManager.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GameCollectionManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevelopersController(IDeveloperService developerService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<DeveloperResponse>>> GetAll()
    {
        var developers = await developerService.GetAllAsync();

        return Ok(developers);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DeveloperResponse>> GetById(int id)
    {
        var developer = await developerService.GetByIdAsync(id);

        if (developer is null)
        {
            return NotFound();
        }

        return Ok(developer);
    }

    [HttpPost]
    public async Task<ActionResult<DeveloperResponse>> Create(
        CreateDeveloperRequest request)
    {
        var developer = await developerService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = developer.Id },
            developer
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateDeveloperRequest request)
    {
        var success = await developerService.UpdateAsync(id, request);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await developerService.DeleteAsync(id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}