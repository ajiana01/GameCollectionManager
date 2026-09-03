using GameCollectionManager.Models;
using Microsoft.EntityFrameworkCore;

namespace GameCollectionManager.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Game> Games {get; set;}
    public DbSet<Developer> Developers {get; set;}
    public DbSet<Genre> Genres {get; set;}
    public DbSet<Platform> Platforms {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=GameCollectionManager.db");
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Developer>(entity =>
        {
            entity.HasIndex(d => d.Name)
                .IsUnique();

            entity.HasMany(d => d.Games)
                .WithOne(g => g.Developer)
                .HasForeignKey(g => g.DeveloperId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasIndex(g => g.Title);

            entity.HasMany(g => g.Genres)
                .WithMany(g => g.Games)
                .UsingEntity<Dictionary<string, object>>(
                    "GameGenre",
                    j => j
                        .HasOne<Genre>()
                        .WithMany()
                        .HasForeignKey("GenreId"),
                    j => j
                        .HasOne<Game>()
                        .WithMany()
                        .HasForeignKey("GameId"),
                    j =>
                    {
                        j.HasKey("GameId", "GenreId");
                        j.ToTable("GameGenres");
                    });

            entity.HasMany(g => g.Platforms)
                .WithMany(p => p.Games)
                .UsingEntity<Dictionary<string, object>>(
                    "GamePlatform",
                    j => j
                        .HasOne<Platform>()
                        .WithMany()
                        .HasForeignKey("PlatformId"),
                    j => j
                        .HasOne<Game>()
                        .WithMany()
                        .HasForeignKey("GameId"),
                    j =>
                    {
                        j.HasKey("GameId", "PlatformId");
                        j.ToTable("GamePlatforms");
                    });
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasIndex(g => g.Name)
                .IsUnique();
        });

        modelBuilder.Entity<Platform>(entity =>
        {
            entity.HasIndex(p => p.Name)
                .IsUnique();
        });
        
    }
}