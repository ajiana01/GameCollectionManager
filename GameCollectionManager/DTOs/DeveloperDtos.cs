using System.ComponentModel.DataAnnotations;

namespace GameCollectionManager.DTOs;

public class CreateDeveloperRequest
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    public string? Location { get; set; }
}


public class UpdateDeveloperRequest
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    public string? Location { get; set; }
}

public class DeveloperResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Location { get; set; }
}