using Pharmacie.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacie.Repositories
{
    public interface IMedecinRepository
    {
        Task<IEnumerable<Medecin>> GetAllAsync();
        Task<Medecin> GetByIdAsync(int id);
        Task AddAsync(Medecin medecin);
        void Update(Medecin medecin);
        void Delete(Medecin medecin);
        Task SaveChangesAsync();
    }
}
