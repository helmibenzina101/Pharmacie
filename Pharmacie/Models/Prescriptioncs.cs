using System.ComponentModel.DataAnnotations;

namespace Pharmacie.Models
{
    public class Prescription
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PharmacistId { get; set; }
        public DateTime DateIssued { get; set; }
        public List<Medication> Medications { get; set; }
    }
}
