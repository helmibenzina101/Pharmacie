using Pharmacie.Models;


namespace Pharmacie.Repositories
{
    public interface IMedicationRepository : IRepository<Medication>
    {
        Task<Medication> GetByNameAsync(string name);
    }
}