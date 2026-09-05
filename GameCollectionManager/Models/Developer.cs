using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameCollectionManager.Models;

public class Developer
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(200)]
    public string? Location { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
    
    //FK
    [Required]
    public string UserId { get; set; } = string.Empty;

    //Navigation Property
    [ForeignKey("UserId")] public ApplicationUser User { get; set; } = null!;
}