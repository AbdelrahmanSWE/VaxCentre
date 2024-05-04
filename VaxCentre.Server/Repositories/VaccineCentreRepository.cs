using Microsoft.EntityFrameworkCore;
using VaxCentre.Server.Data;
using VaxCentre.Server.Interfaces;
using VaxCentre.Server.Models;

namespace VaxCentre.Server.Repositories
{
    public class VaccineCentreRepository : IVaccineCentreRepository
    {
        DBContext _context;
        public VaccineCentreRepository(DBContext context) 
        {
            _context = context;
        }
        public Task<VaccineCentre?> CreateAsync(VaccineCentre entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<VaccineCentre>> GetAllAsync()
        {
            try
            {
                var Result = await _context.VaccineCentres.ToListAsync();
                return Result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all centres.", ex);
            }
        }

        public Task<VaccineCentre?> GetByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<VaccineCentre>> GetByNameAsync(string name)
        {
            try
            {
                var Result = await _context.VaccineCentres
                    .Where(x => x.DisplayName.Contains(name))
                    .ToListAsync();

                return Result;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving centres with name containing {name}.", ex);
            }
        }

        public async Task<bool> IsExist(string Id)
        {
            try
            {
                var Result = await _context.VaccineCentres.FirstOrDefaultAsync(x => x.Id == Id);
                if (Result != null) return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while checking the existence of the vaccine centre with Id {Id}.", ex);
            }
        }

        public Task<VaccineCentre?> UpdateAsync(VaccineCentre entity, string Id)
        {
            throw new NotImplementedException();
        }
    }
}
