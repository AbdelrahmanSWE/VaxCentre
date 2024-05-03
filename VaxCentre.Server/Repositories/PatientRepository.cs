using Microsoft.EntityFrameworkCore;
using VaxCentre.Server.Data;
using VaxCentre.Server.Interfaces;
using VaxCentre.Server.Models;

namespace VaxCentre.Server.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        DBContext _context;

        public PatientRepository(DBContext context) 
        {
            _context = context;
        }

        public Task<Patient?> CreateAsync(Patient entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            try
            {
                var Result = await _context.Patients.ToListAsync();
                return Result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all vaccines.", ex);
            }
        }

        public Task<Patient?> GetByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Patient>> GetByNameAsync(string name)
        {
            try
            {
                var Result = await _context.Patients
                    .Where(x => (x.FirstName+" "+x.LastName).Contains(name))
                    .ToListAsync();

                return Result;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving vaccines with name containing {name}.", ex);
            }
        }

        public async Task<List<Patient>> GetByState(int state)
        {
            try
            {
                var Result = await _context.Patients
                    .Where(x => x.AcceptState == state)
                    .ToListAsync();

                return Result;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving vaccines with name containing {name}.", ex);
            }
        }

        public async Task<bool> IsExist(string Id)
        {
            try
            {
                var Result = await _context.Patients.FirstOrDefaultAsync(x => x.Id == Id);
                if (Result != null) return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while checking the existence of the vaccine with Id {Id}.", ex);
            }
        }

        public Task<Patient?> UpdateAsync(Patient entity, string id)
        {
            throw new NotImplementedException();
        }
    }
}
