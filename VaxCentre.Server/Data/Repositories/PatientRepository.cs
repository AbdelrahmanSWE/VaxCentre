using Microsoft.EntityFrameworkCore;
using VaxCentre.Server.Data;
using VaxCentre.Server.Data.Interfaces;
using VaxCentre.Server.Models;

namespace VaxCentre.Server.Data.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        private readonly DBContext _context;

        public PatientRepository(DBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Patient>> GetByNameAsync(string name)
        {
            try
            {
                var Result = await _context.Patients
                    .Where(x => (x.FirstName + " " + x.LastName).Contains(name))
                    .ToListAsync();

                return Result;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving patients with name containing {name}.", ex);
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
                throw new Exception($"An error occurred while retrieving patients with state: {state}.", ex);
            }
        }

        public async Task<Patient> ChangeAcceptState(int Id)
        {
            var patient = await GetByIdAsync(Id);
            if (patient == null) {
                throw new Exception("No user found");
            }
            patient.AcceptState = 1;
            await _context.SaveChangesAsync();
            return patient;
        }
    }
}
