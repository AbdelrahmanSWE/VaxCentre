using VaxCentre.Server.Models;

namespace VaxCentre.Server.Interfaces
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<Patient?> GetByIdAsync(string Id);
        Task<List<Patient>> GetByNameAsync(string name);
        Task<List<Patient>> GetByState(int state);
    }
}
