using System.ComponentModel.DataAnnotations;

namespace GameCollectionManager.Models;

public class Platform
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}