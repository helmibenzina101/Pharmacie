using Microsoft.EntityFrameworkCore;
using Pharmacie.Data;
using Pharmacie.Models;
using Pharmacie.Repositories;


namespace Pharmacie.Repositories
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        public PatientRepository(AppDbContext context) : base(context) { }

        public async Task<Patient> GetByFullNameAsync(string fullName)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.FullName == fullName);
        }
    }
}
