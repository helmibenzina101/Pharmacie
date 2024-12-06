using Pharmacie.Models;

namespace Pharmacie.Repositories
{
    public interface IPharmacistRepository : IRepository<Pharmacist>
    {
        Task<Pharmacist> GetByEmailAsync(string email);
    }
}