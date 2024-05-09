using VaxCentre.Server.Models;

namespace VaxCentre.Server.Data.Interfaces
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<List<Patient>> GetByNameAsync(string name);
        Task<List<Patient>> GetByState(int state);
        Task<Patient> ChangeAcceptState(int Id);
    }
}
