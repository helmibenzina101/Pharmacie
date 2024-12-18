using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacie.Models;
using Pharmacie.Repositories;
using System.Threading.Tasks;

namespace Pharmacie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "user,master")]
    public class MedecinController : ControllerBase
    {
        private readonly IMedecinRepository _medecinRepository;

        public MedecinController(IMedecinRepository medecinRepository)
        {
            _medecinRepository = medecinRepository;
        }

        // Récupère tous les médecins
        [HttpGet]
        public async Task<IActionResult> GetAllMedecins()
        {
            var medecins = await _medecinRepository.GetAllAsync();
            return Ok(medecins);
        }

        // Récupère un médecin par son ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedecinById(int id)
        {
            var medecin = await _medecinRepository.GetByIdAsync(id);
            if (medecin == null)
                return NotFound();

            return Ok(medecin);
        }

        // Ajoute un médecin
        [HttpPost]
        public async Task<IActionResult> AddMedecin([FromBody] Medecin medecin)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _medecinRepository.AddAsync(medecin);
            return CreatedAtAction(nameof(GetMedecinById), new { id = medecin.Id }, medecin);
        }

        // Met à jour un médecin
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedecin(int id, [FromBody] Medecin medecin)
        {
            if (id != medecin.Id)
                return BadRequest("L'ID fourni ne correspond pas à l'entité.");

            var existingMedecin = await _medecinRepository.GetByIdAsync(id);
            if (existingMedecin == null)
                return NotFound();

            existingMedecin.FullName = medecin.FullName;
            existingMedecin.Email = medecin.Email;
           
            existingMedecin.Specialization = medecin.Specialization;

            _medecinRepository.Update(existingMedecin);
            await _medecinRepository.SaveChangesAsync();

            return NoContent();
        }

        // Supprime un médecin
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedecin(int id)
        {
            var medecin = await _medecinRepository.GetByIdAsync(id);
            if (medecin == null)
                return NotFound();

            _medecinRepository.Delete(medecin);
            await _medecinRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
