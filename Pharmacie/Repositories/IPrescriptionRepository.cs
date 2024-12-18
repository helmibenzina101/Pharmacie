using Pharmacie.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacie.Repositories
{
    public interface IPrescriptionRepository : IRepository<Prescription>
    {
        Task<Prescription> GetPrescriptionWithDetailsByIdAsync(int id);
        Task<IEnumerable<Prescription>> GetPrescriptionsWithDetailsAsync();
        Task AddPrescriptionWithDetailsAsync(Prescription prescription);
    }
}
