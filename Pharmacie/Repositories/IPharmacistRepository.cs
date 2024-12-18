using Pharmacie.Models;
using System.Threading.Tasks;

namespace Pharmacie.Repositories
{
    /// <summary>
    /// Interface spécifique pour le repository des pharmaciens.
    /// </summary>
    public interface IPharmacistRepository : IRepository<Pharmacist>
    {
        Task<Pharmacist> GetByFullNameAsync(string fullName);
        Task<Pharmacist> GetByEmailAsync(string email);

        /// <summary>
        /// Met à jour un pharmacien existant.
        /// </summary>
        /// <param name="pharmacist">Instance du pharmacien avec les modifications.</param>
        /// <returns>Tâche asynchrone.</returns>
        Task UpdatePharmacistAsync(Pharmacist pharmacist);
        Task DeleteAsync(int id);

    }
}
