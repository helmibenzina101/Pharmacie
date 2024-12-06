using Pharmacie.Models;
using Pharmacie.Repositories;
using Pharmacie.Services;

public class PharmacistService : IPharmacistService
{
    private readonly IRepository<Pharmacist> _repository;

    public PharmacistService(IRepository<Pharmacist> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Pharmacist>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Pharmacist> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
}
