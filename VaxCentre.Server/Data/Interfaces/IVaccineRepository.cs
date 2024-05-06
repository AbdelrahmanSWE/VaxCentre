using VaxCentre.Server.Models;
using VaxCentre.Server.Dtos.Vaccine;

namespace VaxCentre.Server.Data.Interfaces
{
    public interface IVaccineRepository : IGenericRepository<Vaccine>
    {
        Task<List<Vaccine>> GetByNameAsync(string name);
        Task<List<Vaccine>> GetByCentre(int centreId);
    }
}
