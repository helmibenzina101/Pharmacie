using Microsoft.EntityFrameworkCore;
using Pharmacie.Data;
using Pharmacie.Models;

namespace Pharmacie.Repositories
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        // Méthode pour récupérer un patient par son nom complet
        public async Task<Patient> GetByFullNameAsync(string fullName)
        {
            return await _context.Patients
                .AsNoTracking() // Empêche le suivi des entités pour éviter les conflits
                .FirstOrDefaultAsync(p => p.FullName == fullName);
        }

        // Méthode de mise à jour pour gérer les conflits de suivi
        public async Task UpdatePatientAsync(Patient updatedPatient)
        {
            // Vérifie si une instance du patient est déjà suivie par le contexte
            var trackedEntity = _context.ChangeTracker.Entries<Patient>()
                .FirstOrDefault(e => e.Entity.Id == updatedPatient.Id);

            if (trackedEntity != null)
            {
                // Détache l'entité suivie pour éviter les conflits
                _context.Entry(trackedEntity.Entity).State = EntityState.Detached;
            }

            // Attache et marque l'entité comme modifiée
            _context.Entry(updatedPatient).State = EntityState.Modified;

            // Sauvegarde les modifications
            await _context.SaveChangesAsync();
        }
    }
}
