using Microsoft.EntityFrameworkCore;
using Pharmacie.Models;

namespace Pharmacie.Data
{


public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        public DbSet<Pharmacist> Pharmacists { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
    }
}

