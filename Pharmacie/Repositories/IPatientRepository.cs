using Pharmacie.Models;

using Pharmacie.Repositories;

namespace Pharmacie.Repositories
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<Patient> GetByFullNameAsync(string fullName);
    }
}
