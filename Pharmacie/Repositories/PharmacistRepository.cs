using Microsoft.EntityFrameworkCore;
using Pharmacie.Data;
using Pharmacie.Models;
using System.Threading.Tasks;

namespace Pharmacie.Repositories
{
    public class PharmacistRepository : Repository<Pharmacist>, IPharmacistRepository
    {
        private readonly AppDbContext _context;

        public PharmacistRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Pharmacist> GetByFullNameAsync(string fullName)
        {
            return await _context.Pharmacists
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.FullName == fullName);
        }

        public async Task<Pharmacist> GetByEmailAsync(string email)
        {
            return await _context.Pharmacists
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task UpdatePharmacistAsync(Pharmacist pharmacist)
        {
            // Vérifie si une autre instance est déjà suivie
            var existingEntity = _context.ChangeTracker.Entries<Pharmacist>()
                .FirstOrDefault(e => e.Entity.Id == pharmacist.Id);

            if (existingEntity != null)
            {
                _context.Entry(existingEntity.Entity).State = EntityState.Detached;
            }

            // Charge le pharmacien existant depuis la base de données
            var pharmacistInDb = await _context.Pharmacists.FindAsync(pharmacist.Id);
            if (pharmacistInDb != null)
            {
                // Mise à jour des champs nécessaires
                pharmacistInDb.FullName = pharmacist.FullName;
                pharmacistInDb.PharmacyName = pharmacist.PharmacyName;
                pharmacistInDb.Email = pharmacist.Email;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Pharmacist not found.");
            }
        }
        // Implémentation de DeleteAsync
        public async Task DeleteAsync(int id)
        {
            var pharmacist = await _context.Pharmacists.FindAsync(id);
            if (pharmacist != null)
            {
                _context.Pharmacists.Remove(pharmacist);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Pharmacist not found.");
            }
        }
    }
}
