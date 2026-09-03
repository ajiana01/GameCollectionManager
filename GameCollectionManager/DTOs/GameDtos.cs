using System.ComponentModel.DataAnnotations;

namespace GameCollectionManager.DTOs;

public class CreateGameRequest
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Range(1950, 2100)]
    public int ReleaseYear { get; set; }

    [Required]
    public int DeveloperId { get; set; }

    public List<int> GenreIds { get; set; } = new();

    public List<int> PlatformIds { get; set; } = new();
}


public class UpdateGameRequest
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Range(1950, 2100)]
    public int ReleaseYear { get; set; }

    [Required]
    public int DeveloperId { get; set; }

    public List<int> GenreIds { get; set; } = new();

    public List<int> PlatformIds { get; set; } = new();
}


public class GameResponse
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Range(1950, 2100)]
    public int ReleaseYear { get; set; }

    public DeveloperResponse Developer { get; set; } = null!;

    public List<GenreResponse> Genres { get; set; } = new();

    public List<PlatformResponse> Platforms { get; set; } = new();
}
