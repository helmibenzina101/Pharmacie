using System.ComponentModel.DataAnnotations;

namespace Pharmacie.DTO
{
    public class NewUserDTO
    {
        [Required]
        [EmailAddress]


        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string Role { get; set; } // "medecin" ou "master"
       
    }
}
