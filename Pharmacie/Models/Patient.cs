using System.ComponentModel.DataAnnotations;

namespace Pharmacie.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
    }
}
