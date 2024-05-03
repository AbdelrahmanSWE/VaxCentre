using Microsoft.EntityFrameworkCore;
using VaxCentre.Server.Data;
using VaxCentre.Server.Interfaces;
using VaxCentre.Server.Models;


namespace VaxCentre.Server.Repositories
{
    public class VaccineRepository : IVaccineRepository
    {
        private readonly DBContext _context;
        public VaccineRepository(DBContext context) { 
            _context= context;
        }

        public async Task<Vaccine?> CreateAsync(Vaccine vaccine)
        {
            try
            {
                await _context.Vaccines.AddAsync(vaccine);
                var saveResult = await _context.SaveChangesAsync();
                if (saveResult > 0) return vaccine;
                return null;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"An error occurred while saving changes to the database. vaccine in query: {vaccine}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"An unexpected error occurred while creating the vaccine in query: {vaccine}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                var Deleted = await _context.Vaccines.FirstOrDefaultAsync(x => x.Id == Id);
                if (Deleted == null) return false;
                _context.Vaccines.Remove(Deleted);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while deleting the vaccine from the database.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the vaccine.", ex);
            }
        }

        public async Task<List<Vaccine>> GetAllAsync()
        {
            try
            {
                var Result = await _context.Vaccines.ToListAsync();
                return Result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all vaccines.", ex);
            }
        }

        public async Task<Vaccine?> GetByIdAsync(int Id)
        {
            try
            {
                var Result = await _context.Vaccines.FirstOrDefaultAsync(x => x.Id == Id);
                if (Result != null) return Result;
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the vaccine with Id {Id}.", ex);
            }
        }

        public async Task<List<Vaccine>> GetByNameAsync(string name)
        {
            try
            {
                var Result = await _context.Vaccines
                    .Where(x => x.Name.Contains(name))
                    .ToListAsync();

                return Result;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving vaccines with name containing {name}.", ex);
            }
        }

        public async Task<List<Vaccine>> GetByCentre(string centreId)
        {
            try
            {
                var Result = await _context.VaccineCentres.FirstOrDefaultAsync(x => x.Id == centreId);
                return Result?.Vaccines ?? new List<Vaccine>();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving vaccines for the centre with Id {centreId}.", ex);
            }
        }

        public async Task<bool> IsExist(int Id)
        {
            try
            {
                var Result = await _context.Vaccines.FirstOrDefaultAsync(x => x.Id == Id);
                if (Result != null) return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while checking the existence of the vaccine with Id {Id}.", ex);
            }
        }


        public async Task<Vaccine?> UpdateAsync(Vaccine Updated, int Id)
        {
            try
            {
                var vaccine = await _context.Vaccines.FirstOrDefaultAsync(x => x.Id == Id);

                if (vaccine == null)
                    return null;

                if (Updated.Name != null)
                    vaccine.Name = Updated.Name;

                if (Updated.Description != null)
                    vaccine.Description = Updated.Description;

                if (Updated.Precaution != null)
                    vaccine.Precaution = Updated.Precaution;

                if (Updated.GapTime != null)
                    vaccine.GapTime = Updated.GapTime;

                await _context.SaveChangesAsync();

                return vaccine;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while updating the vaccine.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
    }
}
