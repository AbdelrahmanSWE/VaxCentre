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

        public Task<bool> DeleteAsync(int id)
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

        public Task<Patient?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Patient>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<Patient>> GetByState(int state)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExist(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Patient?> UpdateAsync(Patient entity, int id)
        {
            throw new NotImplementedException();
        }
    }
}
