using GameCollectionManager.Data;
using GameCollectionManager.Models;
using GameCollectionManager.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace GameCollectionManager.Repositories;

public class GameRepository(AppDbContext context) : IGameRepository
{
    public async Task<Game> CreateAsync(Game game)
    {
        context.Games.Add(game);
        await context.SaveChangesAsync();
        return game;
    }

    public async Task<List<Game>> GetAllAsync(int developerId)
    {
        return await context.Games
            .Include(game => game.Developer)
            .Where(game => game.DeveloperId == developerId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<Game?> GetByIdAsync(int id, int developerId)
    {
        return await context.Games
            .Include(game => game.Developer)
            .FirstOrDefaultAsync(game => game.Id == id && game.DeveloperId == developerId);
    }

    public async Task<Game> UpdateAsync(Game game)
    {
        context.Games.Update(game);
        await context.SaveChangesAsync();
        return game;
    }

    public async Task DeleteAsync(int id, int developerId)
    {
        var game = await GetByIdAsync(id, developerId);
        if (game != null)
        {
            context.Games.Remove(game);
            await context.SaveChangesAsync();
        }
    }
}
