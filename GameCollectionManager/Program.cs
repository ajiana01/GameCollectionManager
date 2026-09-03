using GameCollectionManager.Data;
using GameCollectionManager.DTOs;
using GameCollectionManager.Models;
using GameCollectionManager.Services;
using GameCollectionManager.Services.Interface;
using Microsoft.EntityFrameworkCore;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(
                builder.Configuration.GetConnectionString("DefaultConnection")
            ));

        builder.Services.AddScoped<IDeveloperService, DeveloperService>();
        builder.Services.AddScoped<IGameService, GameService>();
        builder.Services.AddScoped<IGenreService, GenreService>();
        builder.Services.AddScoped<IPlatformService, PlatformService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider
                .GetRequiredService<AppDbContext>();

            await context.Database.MigrateAsync();

            await SeedDatabaseAsync(context);
        }

        await app.RunAsync();
    }
    
    static async Task SeedDatabaseAsync(AppDbContext context)
    {
        if (await context.Developers.AnyAsync())
            return;

        var developers = new[]
        {
            new Developer
            {
                Name = "CD Projekt Red",
                Location = "Poland"
            },
            new Developer
            {
                Name = "Rockstar Games",
                Location = "United States"
            },
            new Developer
            {
                Name = "FromSoftware",
                Location = "Japan"
            }
        };

        var genres = new[]
        {
            new Genre { Name = "RPG" },
            new Genre { Name = "Action" },
            new Genre { Name = "Adventure" },
            new Genre { Name = "Open World" }
        };

        var platforms = new[]
        {
            new Platform { Name = "PC" },
            new Platform { Name = "PlayStation 5" },
            new Platform { Name = "Xbox Series X" }
        };

        context.Developers.AddRange(developers);
        context.Genres.AddRange(genres);
        context.Platforms.AddRange(platforms);

        await context.SaveChangesAsync();

        var games = new[]
        {
            new Game
            {
                Title = "The Witcher 3",
                Description = "Open world action RPG.",
                ReleaseYear = 2015,
                Developer = developers[0],
                Genres = new List<Genre>
                {
                    genres[0],
                    genres[1],
                    genres[3]
                },
                Platforms = new List<Platform>
                {
                    platforms[0],
                    platforms[1],
                    platforms[2]
                }
            },

            new Game
            {
                Title = "Red Dead Redemption 2",
                Description = "Open world action adventure game.",
                ReleaseYear = 2018,
                Developer = developers[1],
                Genres = new List<Genre>
                {
                    genres[1],
                    genres[2],
                    genres[3]
                },
                Platforms = new List<Platform>
                {
                    platforms[0],
                    platforms[1],
                    platforms[2]
                }
            },

            new Game
            {
                Title = "Elden Ring",
                Description = "Action RPG developed by FromSoftware.",
                ReleaseYear = 2022,
                Developer = developers[2],
                Genres = new List<Genre>
                {
                    genres[0],
                    genres[1],
                    genres[3]
                },
                Platforms = new List<Platform>
                {
                    platforms[0],
                    platforms[1],
                    platforms[2]
                }
            }
        };

        context.Games.AddRange(games);

        await context.SaveChangesAsync();
    }
}