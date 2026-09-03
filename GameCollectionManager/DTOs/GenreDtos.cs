using System.ComponentModel.DataAnnotations;

namespace GameCollectionManager.DTOs;

public class GenreResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}


public class CreateGenreRequest
{
    [Required] [MaxLength(100)] public string Name { get; set; } = string.Empty;
}

public class UpdateGenreRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}