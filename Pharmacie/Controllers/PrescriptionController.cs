using Microsoft.AspNetCore.Mvc;
using Pharmacie.Models;
using Pharmacie.Repositories;


namespace Pharmacie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IMedicationRepository _medicationRepository;

        public PrescriptionController(IPrescriptionRepository prescriptionRepository, IMedicationRepository medicationRepository)
        {
            _prescriptionRepository = prescriptionRepository;
            _medicationRepository = medicationRepository;
        }

        // Récupérer une ordonnance par ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrescription(int id)
        {
            var prescription = await _prescriptionRepository.GetByIdAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }
            return Ok(prescription);
        }

        // Ajouter une ordonnance
        [HttpPost]
        public async Task<IActionResult> CreatePrescription([FromBody] Prescription prescription)
        {
            if (prescription == null)
            {
                return BadRequest("L'ordonnance est null");
            }

            // Assurer que les médicaments sont valides avant de les ajouter à l'ordonnance
            if (prescription.Medications != null && prescription.Medications.Count > 0)
            {
                foreach (var medication in prescription.Medications)
                {
                    var med = await _medicationRepository.GetByIdAsync(medication.Id);
                    if (med == null)
                    {
                        return BadRequest($"Le médicament {medication.Name} n'existe pas dans la base de données.");
                    }
                }
            }

            await _prescriptionRepository.AddAsync(prescription);
            return CreatedAtAction(nameof(GetPrescription), new { id = prescription.Id }, prescription);
        }

        // Mettre à jour une ordonnance
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrescription(int id, [FromBody] Prescription prescription)
        {
            if (prescription == null || prescription.Id != id)
            {
                return BadRequest("Les données de l'ordonnance sont incorrectes");
            }

            var existingPrescription = await _prescriptionRepository.GetByIdAsync(id);
            if (existingPrescription == null)
            {
                return NotFound();
            }

            // Mettre à jour les médicaments de l'ordonnance
            if (prescription.Medications != null && prescription.Medications.Count > 0)
            {
                foreach (var medication in prescription.Medications)
                {
                    var med = await _medicationRepository.GetByIdAsync(medication.Id);
                    if (med == null)
                    {
                        return BadRequest($"Le médicament {medication.Name} n'existe pas dans la base de données.");
                    }
                }
            }

            _prescriptionRepository.Update(prescription);
            return NoContent();
        }

        // Supprimer une ordonnance
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            var prescription = await _prescriptionRepository.GetByIdAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }

            _prescriptionRepository.Delete(prescription);
            return NoContent();
        }

        // Récupérer toutes les ordonnances
        [HttpGet]
        public async Task<IActionResult> GetAllPrescriptions()
        {
            var prescriptions = await _prescriptionRepository.GetAllAsync();
            return Ok(prescriptions);
        }

        // Ajouter un médicament à une ordonnance
        [HttpPost("{prescriptionId}/add-medication")]
        public async Task<IActionResult> AddMedicationToPrescription(int prescriptionId, [FromBody] Medication medication)
        {
            var prescription = await _prescriptionRepository.GetByIdAsync(prescriptionId);
            if (prescription == null)
            {
                return NotFound($"L'ordonnance avec l'ID {prescriptionId} n'a pas été trouvée.");
            }

            var existingMedication = await _medicationRepository.GetByIdAsync(medication.Id);
            if (existingMedication == null)
            {
                return NotFound($"Le médicament avec l'ID {medication.Id} n'existe pas.");
            }

            if (prescription.Medications == null)
            {
                prescription.Medications = new List<Medication>();
            }

            prescription.Medications.Add(existingMedication);
            _prescriptionRepository.Update(prescription);

            return Ok(prescription);
        }

        // Supprimer un médicament d'une ordonnance
        [HttpDelete("{prescriptionId}/remove-medication/{medicationId}")]
        public async Task<IActionResult> RemoveMedicationFromPrescription(int prescriptionId, int medicationId)
        {
            var prescription = await _prescriptionRepository.GetByIdAsync(prescriptionId);
            if (prescription == null)
            {
                return NotFound($"L'ordonnance avec l'ID {prescriptionId} n'a pas été trouvée.");
            }

            var medication = prescription.Medications?.FirstOrDefault(m => m.Id == medicationId);
            if (medication == null)
            {
                return NotFound($"Le médicament avec l'ID {medicationId} n'est pas associé à cette ordonnance.");
            }

            prescription.Medications.Remove(medication);
            _prescriptionRepository.Update(prescription);

            return Ok(prescription);
        }
    }
}