namespace GameCollectionManager.DTOs;

public class CreateGameDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int ReleaseYear { get; set; }
    public int DeveloperId { get; set; }
    public List<int> GenreIds { get; set; } = new();
    public List<int> PlatformIds { get; set; } = new();
}

public class UpdateGameDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int ReleaseYear { get; set; }
    public int DeveloperId { get; set; }
    public List<int> GenreIds { get; set; } = new();
    public List<int> PlatformIds { get; set; } = new();
}

public class GameDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int ReleaseYear { get; set; }
    public DeveloperDto Developer { get; set; } = null!;
    public List<GenreDto> Genres { get; set; } = new();
    public List<PlatformDto> Platforms { get; set; } = new();
}
