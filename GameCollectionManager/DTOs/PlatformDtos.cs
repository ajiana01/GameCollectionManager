using System.ComponentModel.DataAnnotations;

namespace GameCollectionManager.DTOs;

public class PlatformResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}

public class CreatePlatformRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}

public class UpdatePlatformRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}