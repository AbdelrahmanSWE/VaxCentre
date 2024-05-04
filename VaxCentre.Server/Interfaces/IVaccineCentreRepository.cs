using VaxCentre.Server.Models;

namespace VaxCentre.Server.Interfaces
{
    public interface IVaccineCentreRepository : IGenericRepository<VaccineCentre, string>
    {
        Task<List<VaccineCentre>> GetByNameAsync(string name);
    }
}
