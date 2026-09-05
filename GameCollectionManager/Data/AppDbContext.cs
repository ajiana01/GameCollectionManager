using GameCollectionManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameCollectionManager.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
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

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            //Property
            entity.Property(user => user.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(user => user.LastName).HasMaxLength(100);
            entity.Property(user => user.CreatedAt).HasDefaultValueSql("datetime('now')");
        });
        
        modelBuilder.Entity<Developer>(entity =>
        {
            //Key
            entity.HasKey(developer => developer.Id);
            
            //Property
            entity.Property(developer => developer.Name).IsRequired().HasMaxLength(200);
            entity.Property(developer => developer.Location).IsRequired().HasMaxLength(200);
            entity.Property(developer => developer.CreatedAt).HasDefaultValueSql("datetime('now')"); 
            
            //Indexing
            entity.HasIndex(developer => developer.Name)
                .IsUnique();
            entity.HasIndex(developer => developer.UserId);
            entity.HasIndex(developer => developer.CreatedAt);
            
            //Relation
            entity.HasMany(developer => developer.Games)
                .WithOne(game => game.Developer)
                .HasForeignKey(game => game.DeveloperId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(developer => developer.User)
                .WithMany(user => user.Developers)
                .HasForeignKey(developer => developer.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Game>(entity =>
        {
            //Key
            entity.HasKey(game => game.Id);

            entity.ToTable(table => table.HasCheckConstraint(
                "CK_Games_ReleaseYear",
                "\"ReleaseYear\" BETWEEN 1950 AND 2100"));
            
            //Property
            entity.Property(game => game.Title).IsRequired().HasMaxLength(200);
            entity.Property(game => game.Description).IsRequired().HasMaxLength(2000);
            entity.Property(game => game.ReleaseYear).IsRequired();
            entity.Property(game => game.DeveloperId).IsRequired();
            entity.Property(game => game.CreatedAt).HasDefaultValueSql("datetime('now')");
            
            //Indexing
            entity.HasIndex(game => game.Title);
            entity.HasIndex(game => game.CreatedAt);

            //Relation
            entity.HasMany(game => game.Genres)
                .WithMany(genre => genre.Games)
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
            //Key
            entity.HasKey(genre => genre.Id);
            
            //Property
            entity.Property(genre => genre.Name).IsRequired().HasMaxLength(100);
            
            //Indexing
            entity.HasIndex(g => g.Name)
                .IsUnique();
        });

        modelBuilder.Entity<Platform>(entity =>
        {
            //Key
            entity.HasKey(platform => platform.Id);
            
            //Property
            entity.Property(platform => platform.Name).IsRequired().HasMaxLength(100);
            
            //Indexing
            entity.HasIndex(p => p.Name)
                .IsUnique();
        });
        
    }
}
