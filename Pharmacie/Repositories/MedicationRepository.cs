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
    }
}
