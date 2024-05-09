using Microsoft.EntityFrameworkCore;
using VaxCentre.Server.Data;
using VaxCentre.Server.Data.Interfaces;
using VaxCentre.Server.Models;

namespace VaxCentre.Server.Data.Repositories
{
    public class VaccineCentreRepository : GenericRepository<VaccineCentre>,IVaccineCentreRepository
    {
        private readonly DBContext _context;
        public VaccineCentreRepository(DBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<VaccineCentre>> GetByNameAsync(string name)
        {
            try
            {
                
                var Result = await _context.VaccineCentres
                    .Where(x => x.DisplayName!= null && x.DisplayName.Contains(name))
                    .ToListAsync();

                return Result;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving centres with name containing {name}.", ex);
            }
        }

        public async Task<VaccineCentre> UpdateAsync(VaccineCentre updatedCentre, int Id)
        {
            // Assuming _context is your DbContext
            var centre = await _context.VaccineCentres.FindAsync(Id);
            if (centre == null)
            {
                throw new Exception("Vaccine Centre not found");
            }

            // Update the properties of the centre object here
            if (updatedCentre.DisplayName != null)
            {
                centre.DisplayName = updatedCentre.DisplayName;
            }
            if (updatedCentre.Address != null)
            {
                centre.Address = updatedCentre.Address;
            }

            try
            {
                await _context.SaveChangesAsync();
                return centre;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Unknown error");
            }
        }

        public async Task<VaccineCentre> AssignVaccineToCentre(int VaccineCentreId, int VaccineId)
        {
            var vaccineCentre = await _context.VaccineCentres.FindAsync(VaccineCentreId);
            if (vaccineCentre == null)
            {
                throw new Exception("Vaccine Centre not found");
            }

            var vaccine = await _context.Vaccines.FindAsync(VaccineId);
            if (vaccine == null)
            {
                throw new Exception("Vaccine not found");
            }

            var existingVaccine = await _context.VaccineCentres
                .Where(vc => vc.Id == VaccineCentreId)
                .SelectMany(vc => vc.Vaccines)
                .FirstOrDefaultAsync(v => v.Id == VaccineId);

            if (existingVaccine != null)
            {
                throw new Exception("This Vaccine is already assigned to the Vaccine Centre");
            }

            if (vaccineCentre.Vaccines == null)
            {
                vaccineCentre.Vaccines = new List<Vaccine>();
            }
            vaccineCentre.Vaccines.Add(vaccine);

            await _context.SaveChangesAsync();

            return vaccineCentre;
        }

    }
}
