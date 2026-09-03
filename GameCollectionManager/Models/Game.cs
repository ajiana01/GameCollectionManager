using System.ComponentModel.DataAnnotations;

namespace GameCollectionManager.Models;

public class Game
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = String.Empty;
    public string? Description { get; set; }
    public int ReleaseYear { get; set; }
    
    public int DeveloperId { get; set; }
    public virtual Developer Developer { get; set; } = null!;
    
    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
    
    public virtual ICollection<Platform> Platforms { get; set; } = new List<Platform>();
}