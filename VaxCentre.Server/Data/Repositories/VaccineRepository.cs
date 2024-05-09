using Microsoft.EntityFrameworkCore;
using VaxCentre.Server.Data;
using VaxCentre.Server.Data.Interfaces;
using VaxCentre.Server.Models;


namespace VaxCentre.Server.Data.Repositories
{
    public class VaccineRepository :GenericRepository<Vaccine>, IVaccineRepository
    {
        private readonly DBContext _context;
        public VaccineRepository(DBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Vaccine>> GetByNameAsync(string name)
        {
            try
            {
                var Result = await _context.Vaccines
                    .Where(x => x.Name!=null && x.Name.Contains(name))
                    .ToListAsync();

                return Result;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving vaccines with name containing {name}.", ex);
            }
        }
        public async Task<List<Vaccine>> GetByCentre(int centreId)
        {
            try
            {
                var Result = await _context.VaccineCentres
                    .Include(vc => vc.Vaccines)
                    .FirstOrDefaultAsync(x => x.Id == centreId);
                return Result?.Vaccines ?? [];
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving vaccines for the centre with Id {centreId}.", ex);
            }
        }

        public async Task<Vaccine> UpdateAsync(Vaccine updatedVaccine, int Id)
        {
            var vaccine = await _context.Vaccines.FindAsync(Id);
            if (vaccine == null)
            {
                throw new Exception("Vaccine not found");
            }

            if (updatedVaccine.Name != null)
            {
                vaccine.Name = updatedVaccine.Name;
            }
            if (updatedVaccine.Description != null)
            {
                vaccine.Description = updatedVaccine.Description;
            }
            if (updatedVaccine.Precaution != null)
            {
                vaccine.Precaution = updatedVaccine.Precaution;
            }
            if (updatedVaccine.GapTime.HasValue)
            {
                vaccine.GapTime = updatedVaccine.GapTime;
            }

            try
            {
                await _context.SaveChangesAsync();
                return vaccine;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Unknown error");
            }
        }

    }
}
