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

        public async Task<VaccineCentre> UpdateAsync(VaccineCentre updatedCentre)
        {
            // Assuming _context is your DbContext
            _context.Entry(updatedCentre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return updatedCentre;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await IsExist(updatedCentre.Id))
                {
                    throw new Exception("Vaccine Centre not found");
                }
                else
                {
                    throw new Exception("unkown error");
                }
            }
        }
    }
}
