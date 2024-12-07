using System.ComponentModel.DataAnnotations;

namespace Pharmacie.Models
{
    public class Pharmacist
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}