using GameCollectionManager.DTOs;
using GameCollectionManager.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GameCollectionManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController(IGameService gameService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<GameResponse>>> GetAll()
    {
        return Ok(await gameService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GameResponse>> GetById(int id)
    {
        var game = await gameService.GetByIdAsync(id);

        if (game is null)
        {
            return NotFound();
        }

        return Ok(game);
    }

    [HttpPost]
    public async Task<ActionResult<GameResponse>> Create(
        CreateGameRequest request)
    {
        var game = await gameService.CreateAsync(request);

        if (game is null)
        {
            return BadRequest("Developer not found.");
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = game.Id },
            game
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateGameRequest request)
    {
        var success = await gameService.UpdateAsync(id, request);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await gameService.DeleteAsync(id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}