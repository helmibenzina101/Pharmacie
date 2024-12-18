using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Pharmacie.Models;

namespace Pharmacie.Data
{
    // Ajout de l'héritage de IdentityDbContext avec ApplicationUser
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Ajout des DbSet pour les entités
        public DbSet<Pharmacist> Pharmacists { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Medecin> Medecins { get; set; } // Ajout de la table pour les médecins



    }
}
