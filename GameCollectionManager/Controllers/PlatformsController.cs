using GameCollectionManager.DTOs;
using GameCollectionManager.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GameCollectionManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlatformsController(IPlatformService platformService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<PlatformResponse>>> GetAll()
    {
        return Ok(await platformService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PlatformResponse>> GetById(int id)
    {
        var platform = await platformService.GetByIdAsync(id);

        if (platform is null)
        {
            return NotFound();
        }

        return Ok(platform);
    }

    [HttpPost]
    public async Task<ActionResult<PlatformResponse>> Create(
        CreatePlatformRequest request)
    {
        var platform = await platformService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = platform.Id },
            platform
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdatePlatformRequest request)
    {
        var success = await platformService.UpdateAsync(id, request);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await platformService.DeleteAsync(id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}