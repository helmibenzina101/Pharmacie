using Pharmacie.Models;

namespace Pharmacie.Services
{
    public interface IPharmacistService
    {
      
            Task<IEnumerable<Pharmacist>> GetAllAsync();
            Task<Pharmacist> GetByIdAsync(int id);
       

    }
}
