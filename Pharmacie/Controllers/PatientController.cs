using Microsoft.AspNetCore.Mvc;
using Pharmacie.Models;
using Pharmacie.Repositories;


namespace Pharmacie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        // Récupérer un patient par ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        // Ajouter un nouveau patient
        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] Patient patient)
        {
            if (patient == null)
            {
                return BadRequest("Le patient est null");
            }

            await _patientRepository.AddAsync(patient);
            return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, patient);
        }

        // Mettre à jour un patient existant
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] Patient patient)
        {
            if (patient == null || patient.Id != id)
            {
                return BadRequest("Les données du patient sont incorrectes");
            }

            var existingPatient = await _patientRepository.GetByIdAsync(id);
            if (existingPatient == null)
            {
                return NotFound();
            }

            _patientRepository.Update(patient);
            return NoContent();
        }

        // Supprimer un patient
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _patientRepository.Delete(patient);
            return NoContent();
        }

        // Récupérer tous les patients (optionnel)
        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _patientRepository.GetAllAsync();
            return Ok(patients);
        }
    }
}