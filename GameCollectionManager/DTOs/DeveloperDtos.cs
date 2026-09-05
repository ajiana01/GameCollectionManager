namespace GameCollectionManager.DTOs;

public class CreateDeveloperDto
{
    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }
}


public class UpdateDeveloperDto
{
    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }
}

public class DeveloperDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }
    public DateTime CreatedAt { get; set; }
}