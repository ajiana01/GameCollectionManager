using GameCollectionManager.Data;
using GameCollectionManager.Models;
using GameCollectionManager.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace GameCollectionManager.Services;

public class GameService(AppDbContext context): IGameService
{
    public async Task<Game> CreateAsync(Game game)
    {
        context.Games.Add(game);

        await context.SaveChangesAsync();

        return game;
    }

    public async Task<List<Game>> GetAllAsync()
    {
        return await context.Games
            .AsNoTracking()
            .Include(g => g.Developer)
            .Include(g => g.Genres)
            .Include(g => g.Platforms)
            .ToListAsync();
    }

    public async Task<Game?> GetByIdAsync(int id)
    {
        return await context.Games
            .AsNoTracking()
            .Include(g => g.Developer)
            .Include(g => g.Genres)
            .Include(g => g.Platforms)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<bool> UpdateAsync(int id, Game game)
    {
        var existingGame =
            await context.Games.FindAsync(id);

        if (existingGame == null)
        {
            return false;
        }

        existingGame.Title = game.Title;
        existingGame.Description = game.Description;
        existingGame.ReleaseYear = game.ReleaseYear;
        existingGame.DeveloperId = game.DeveloperId;

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var game =
            await context.Games.FindAsync(id);

        if (game == null)
        {
            return false;
        }

        context.Games.Remove(game);

        await context.SaveChangesAsync();

        return true;
    }
}