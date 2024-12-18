using System.ComponentModel.DataAnnotations;

namespace Pharmacie.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Nom d'utilisateur obligatoire")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
