using Microsoft.AspNetCore.Identity;

namespace GameCollectionManager.Models;

public class ApplicationUser: IdentityUser
{
    public string?  FirstName { get; set; }
    public string?  LastName { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<Developer> Developers { get; set; } = new List<Developer>();
}