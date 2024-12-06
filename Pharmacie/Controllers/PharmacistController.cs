using Microsoft.AspNetCore.Mvc;
using Pharmacie.Models;
using Pharmacie.Repositories;



namespace Pharmacie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacistController : ControllerBase
    {
        private readonly IPharmacistRepository _pharmacistRepository;

        public PharmacistController(IPharmacistRepository pharmacistRepository)
        {
            _pharmacistRepository = pharmacistRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPharmacist(int id)
        {
            var pharmacist = await _pharmacistRepository.GetByIdAsync(id);
            if (pharmacist == null)
            {
                return NotFound();
            }
            return Ok(pharmacist);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePharmacist(Pharmacist pharmacist)
        {
            await _pharmacistRepository.AddAsync(pharmacist);
            return CreatedAtAction(nameof(GetPharmacist), new { id = pharmacist.Id }, pharmacist);
        }
    }
}