using Microsoft.EntityFrameworkCore;
using Pharmacie.Data;
using Pharmacie.Models;
using Pharmacie.Repositories;
using Pharmacie.Models;

namespace Pharmacie.Repositories
{
    public class PrescriptionRepository : Repository<Prescription>, IPrescriptionRepository
    {
        public PrescriptionRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Prescription>> GetByPatientIdAsync(int patientId)
        {
            return await _context.Prescriptions
                .Where(p => p.PatientId == patientId)
                .Include(p => p.Medications)
                .ToListAsync();
        }

        public async Task<IEnumerable<Prescription>> GetByPharmacistIdAsync(int pharmacistId)
        {
            return await _context.Prescriptions
                .Where(p => p.PharmacistId == pharmacistId)
                .Include(p => p.Medications)
                .ToListAsync();
        }
    }
}
