using VaxCentre.Server.Models;
using VaxCentre.Server.Dtos.Vaccine;

namespace VaxCentre.Server.Interfaces
{
    public interface IVaccineRepository
    {
        Task<List<Vaccine>> GetAllAsync();
        Task<Vaccine?> GetByIdAsync(int Id);
        Task<List<Vaccine>> GetByNameAsync(string name);
        Task<List<Vaccine>> GetByCentre(string centreId);
        Task<Vaccine?> CreateAsync(Vaccine vaccine);
        Task<Vaccine?> UpdateAsync(Vaccine vaccine, int Id);
        Task<bool> DeleteAsync(int Id);
        Task<bool> IsExist(int Id);
    }
}
