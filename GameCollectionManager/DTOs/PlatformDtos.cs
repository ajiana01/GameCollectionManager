namespace GameCollectionManager.DTOs;

public class PlatformDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CreatePlatformDto
{
    public string Name { get; set; } = string.Empty;
}

public class UpdatePlatformDto
{
    public string Name { get; set; } = string.Empty;
}