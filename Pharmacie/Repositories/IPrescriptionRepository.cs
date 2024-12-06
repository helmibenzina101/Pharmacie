using Pharmacie.Models;
using Pharmacie.Repositories;
using Pharmacie.Models;

namespace Pharmacie.Repositories
{
    public interface IPrescriptionRepository : IRepository<Prescription>
    {
        Task<IEnumerable<Prescription>> GetByPatientIdAsync(int patientId);
        Task<IEnumerable<Prescription>> GetByPharmacistIdAsync(int pharmacistId);
    }
}
