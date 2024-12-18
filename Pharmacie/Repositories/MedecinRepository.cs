using Microsoft.EntityFrameworkCore;
using Pharmacie.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pharmacie.Data;
namespace Pharmacie.Repositories
{
    public class MedecinRepository : IMedecinRepository
    {
        private readonly AppDbContext _context;

        public MedecinRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Medecin>> GetAllAsync()
        {
            return await _context.Medecins
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Medecin> GetByIdAsync(int id)
        {
            return await _context.Medecins
                .Include(m => m.Prescriptions) // Inclut les prescriptions associées
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Medecin medecin)
        {
            await _context.Medecins.AddAsync(medecin);
            await SaveChangesAsync();
        }

        public void Update(Medecin medecin)
        {
            _context.Medecins.Update(medecin);
        }

        public void Delete(Medecin medecin)
        {
            _context.Medecins.Remove(medecin);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
