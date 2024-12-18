using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacie.Models;
using Pharmacie.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "master")]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionRepository _prescriptionRepository;

        public PrescriptionController(IPrescriptionRepository prescriptionRepository)
        {
            _prescriptionRepository = prescriptionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPrescriptions()
        {
            var prescriptions = await _prescriptionRepository.GetPrescriptionsWithDetailsAsync();
            return Ok(prescriptions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrescriptionById(int id)
        {
            var prescription = await _prescriptionRepository.GetPrescriptionWithDetailsByIdAsync(id);
            if (prescription == null)
                return NotFound(new { message = "Prescription introuvable." });

            return Ok(prescription);
        }

        [HttpPost]
        public async Task<IActionResult> AddPrescription([FromBody] Prescription prescription)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _prescriptionRepository.AddPrescriptionWithDetailsAsync(prescription);
                return CreatedAtAction(nameof(GetPrescriptionById), new { id = prescription.Id }, prescription);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrescription(int id, [FromBody] Prescription prescription)
        {
            if (id != prescription.Id)
                return BadRequest(new { message = "Les IDs ne correspondent pas." });

            try
            {
                var existingPrescription = await _prescriptionRepository.GetByIdAsync(id);
                if (existingPrescription == null)
                    return NotFound(new { message = "Prescription introuvable." });

                // Mise à jour
                existingPrescription.PatientId = prescription.PatientId;
                existingPrescription.MedecinId = prescription.MedecinId;
                existingPrescription.PharmacistId = prescription.PharmacistId;
                existingPrescription.DateIssued = prescription.DateIssued;
                existingPrescription.Medications = prescription.Medications;

                _prescriptionRepository.Update(existingPrescription);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            var prescription = await _prescriptionRepository.GetByIdAsync(id);
            if (prescription == null)
                return NotFound(new { message = "Prescription introuvable." });

            _prescriptionRepository.Delete(prescription);
            return NoContent();
        }
    }
}