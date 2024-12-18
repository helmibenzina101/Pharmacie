using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pharmacie.Models
{
    public class Prescription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; } // Lien avec le patient

        [Required]
        public int MedecinId { get; set; } // Lien avec le médecin

        [Required]
        public int PharmacistId { get; set; } // Lien avec le pharmacien

        [Required]
        public DateTime DateIssued { get; set; }

        [Required]
        public List<int> Medications { get; set; } = new List<int>(); // Liste des IDs des médicaments

        // Propriétés de navigation (facultatives dans JSON envoyé par React)
        public virtual Patient Patient { get; set; }
        public virtual Medecin Medecin { get; set; }
        public virtual Pharmacist Pharmacist { get; set; }
    }
}
