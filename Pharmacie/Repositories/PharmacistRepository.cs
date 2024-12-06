using Microsoft.EntityFrameworkCore;
using Pharmacie.Data;
using Pharmacie.Models;
using Pharmacie.Repositories;
using Pharmacie.Models;

namespace Pharmacie.Repositories
{
    public class PharmacistRepository : Repository<Pharmacist>, IPharmacistRepository
    {
        public PharmacistRepository(AppDbContext context) : base(context) { }

        public async Task<Pharmacist> GetByEmailAsync(string email)
        {
                  
        
            return await _context.Pharmacists.FirstOrDefaultAsync(p => p.Email == email);
        
    }
    }
}