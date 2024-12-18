using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacie.Models;
using Pharmacie.Repositories;
using System;
using System.Threading.Tasks;

namespace Pharmacie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "user,master")]  // Rôle "master" nécessaire pour accéder à ce contrôleur
    public class PharmacistController : ControllerBase
    {
        private readonly IPharmacistRepository _pharmacistRepository;

        public PharmacistController(IPharmacistRepository pharmacistRepository)
        {
            _pharmacistRepository = pharmacistRepository;
        }

        /// <summary>
        /// Récupérer un pharmacien par ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPharmacistById(int id)
        {
            var pharmacist = await _pharmacistRepository.GetByIdAsync(id);
            if (pharmacist == null)
            {
                return NotFound("Pharmacist not found.");
            }
            return Ok(pharmacist);
        }

        /// <summary>
        /// Récupérer un pharmacien par nom complet
        /// </summary>
        [HttpGet("searchByName/{fullName}")]
        public async Task<IActionResult> GetPharmacistByName(string fullName)
        {
            var pharmacist = await _pharmacistRepository.GetByFullNameAsync(fullName);
            if (pharmacist == null)
            {
                return NotFound("Pharmacist not found.");
            }
            return Ok(pharmacist);
        }

        /// <summary>
        /// Ajouter un nouveau pharmacien
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreatePharmacist([FromBody] Pharmacist pharmacist)
        {
            if (pharmacist == null)
            {
                return BadRequest("Pharmacist data is null.");
            }

            await _pharmacistRepository.AddAsync(pharmacist);
            return CreatedAtAction(nameof(GetPharmacistById), new { id = pharmacist.Id }, pharmacist);
        }

        /// <summary>
        /// Mettre à jour un pharmacien existant
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePharmacist(int id, [FromBody] Pharmacist pharmacist)
        {
            if (pharmacist == null || pharmacist.Id != id)
            {
                return BadRequest("Pharmacist data is invalid or ID mismatch.");
            }

            var existingPharmacist = await _pharmacistRepository.GetByIdAsync(id);
            if (existingPharmacist == null)
            {
                return NotFound("Pharmacist not found.");
            }

            // Mise à jour des champs nécessaires
            existingPharmacist.FullName = pharmacist.FullName;
            existingPharmacist.PharmacyName = pharmacist.PharmacyName;
            existingPharmacist.Email = pharmacist.Email;

            await _pharmacistRepository.UpdatePharmacistAsync(existingPharmacist);
            return NoContent();
        }

        /// <summary>
        /// Supprimer un pharmacien par ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePharmacist(int id)
        {
            var pharmacist = await _pharmacistRepository.GetByIdAsync(id);
            if (pharmacist == null)
            {
                return NotFound("Pharmacist not found.");
            }

            await _pharmacistRepository.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Récupérer tous les pharmaciens
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllPharmacists()
        {
            var pharmacists = await _pharmacistRepository.GetAllAsync();
            return Ok(pharmacists);
        }
    }
}
