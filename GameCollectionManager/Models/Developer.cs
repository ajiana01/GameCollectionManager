using System.ComponentModel.DataAnnotations;

namespace GameCollectionManager.Models;

public class Developer
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }
    
    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}