using Microsoft.AspNetCore.Identity;

namespace Pharmacie.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Role { get; set; } // Médecin ou Master
    }
}
