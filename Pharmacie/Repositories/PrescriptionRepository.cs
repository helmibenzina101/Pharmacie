using Microsoft.EntityFrameworkCore;
using Pharmacie.Data;
using Pharmacie.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacie.Repositories
{
    public class PrescriptionRepository : Repository<Prescription>, IPrescriptionRepository
    {
        private readonly AppDbContext _context;

        public PrescriptionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Prescription> GetPrescriptionWithDetailsByIdAsync(int id)
        {
            return await _context.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Medecin)
                .Include(p => p.Pharmacist)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Prescription>> GetPrescriptionsWithDetailsAsync()
        {
            return await _context.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Medecin)
                .Include(p => p.Pharmacist)
                .ToListAsync();
        }

        public async Task AddPrescriptionWithDetailsAsync(Prescription prescription)
        {
            // Charger les entités liées pour éviter les doublons
            prescription.Patient = await _context.Patients.FindAsync(prescription.PatientId);
            prescription.Medecin = await _context.Medecins.FindAsync(prescription.MedecinId);
            prescription.Pharmacist = await _context.Pharmacists.FindAsync(prescription.PharmacistId);

            if (prescription.Patient == null || prescription.Medecin == null || prescription.Pharmacist == null)
            {
                throw new KeyNotFoundException("Patient, Medecin ou Pharmacist introuvable.");
            }

            // Ajouter la prescription
            await _context.Prescriptions.AddAsync(prescription);
            await _context.SaveChangesAsync();
        }
    }
}
