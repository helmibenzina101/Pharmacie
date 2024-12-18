using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Pharmacie.Models
{
    public class Medecin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        [MaxLength(100)]
        public string FullName { get; set; }

       
        public string Email { get; set; }

       
        

        [MaxLength(100)]
        public string Specialization { get; set; }

        // Navigation virtuelle pour les prescriptions (non exposée dans les DTO ou Swagger)
        [JsonIgnore]
        public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}
