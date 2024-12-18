using Microsoft.EntityFrameworkCore;
using Pharmacie.Data;
using Pharmacie.Models;
using Pharmacie.Repositories;
using Pharmacie.Models;

namespace Pharmacie.Repositories
{
    public class MedicationRepository : Repository<Medication>, IMedicationRepository
    {
        public MedicationRepository(AppDbContext context) : base(context) { }

        public async Task<Medication> GetByNameAsync(string name)
        {
            return await _context.Medications.FirstOrDefaultAsync(m => m.Name == name);
        }
        public void Update(Medication medication)
        {
            var existingEntity = _context.ChangeTracker.Entries<Medication>()
                .FirstOrDefault(e => e.Entity.Id == medication.Id);

            if (existingEntity != null)
            {
                _context.Entry(existingEntity.Entity).State = EntityState.Detached;
            }

            _context.Medications.Update(medication);
            _context.SaveChanges();
        }


    }
}
