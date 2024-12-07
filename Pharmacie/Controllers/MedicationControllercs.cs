using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacie.Models;
using Pharmacie.Repositories;




namespace Pharmacie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MedicationController : ControllerBase
    {
        private readonly IMedicationRepository _medicationRepository;

        public MedicationController(IMedicationRepository medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }

        // Récupérer un médicament par ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedication(int id)
        {
            var medication = await _medicationRepository.GetByIdAsync(id);
            if (medication == null)
            {
                return NotFound();
            }
            return Ok(medication);
        }

        // Ajouter un médicament
        [HttpPost]
        public async Task<IActionResult> CreateMedication([FromBody] Medication medication)
        {
            if (medication == null)
            {
                return BadRequest("Le médicament est null");
            }

            await _medicationRepository.AddAsync(medication);
            return CreatedAtAction(nameof(GetMedication), new { id = medication.Id }, medication);
        }

        // Mettre à jour un médicament
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedication(int id, [FromBody] Medication medication)
        {
            if (medication == null || medication.Id != id)
            {
                return BadRequest("Les données du médicament sont incorrectes");
            }

            var existingMedication = await _medicationRepository.GetByIdAsync(id);
            if (existingMedication == null)
            {
                return NotFound();
            }

            _medicationRepository.Update(medication);
            return NoContent();
        }

        // Supprimer un médicament
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedication(int id)
        {
            var medication = await _medicationRepository.GetByIdAsync(id);
            if (medication == null)
            {
                return NotFound();
            }

            _medicationRepository.Delete(medication);
            return NoContent();
        }

        // Récupérer tous les médicaments
        [HttpGet]
        public async Task<IActionResult> GetAllMedications()
        {
            var medications = await _medicationRepository.GetAllAsync();
            return Ok(medications);
        }
    }
}