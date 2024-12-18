using Pharmacie.Models;
using System.Threading.Tasks;

namespace Pharmacie.Repositories
{
    /// <summary>
    /// Interface spécifique pour le repository des patients.
    /// Hérite de l'interface générique IRepository<Patient>.
    /// </summary>
    public interface IPatientRepository : IRepository<Patient>
    {
        /// <summary>
        /// Récupère un patient par son nom complet.
        /// </summary>
        /// <param name="fullName">Nom complet du patient.</param>
        /// <returns>Instance du patient si trouvée, sinon null.</returns>
        Task<Patient> GetByFullNameAsync(string fullName);

        /// <summary>
        /// Met à jour un patient existant de manière robuste pour éviter les conflits de suivi.
        /// </summary>
        /// <param name="patient">Instance du patient avec les modifications.</param>
        /// <returns>Tâche asynchrone.</returns>
        Task UpdatePatientAsync(Patient patient);
    }
}
