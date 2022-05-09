using Microsoft.AspNetCore.Identity;

namespace DVDRental.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? ShopName { get; set; }
        
        public string? Password { get; set; }
    }
}
 