using VaxCentre.Server.Models;

namespace VaxCentre.Server.Data.Interfaces
{
    public interface IRecieptRepository : IGenericRepository<VaccinationReciept>
    {
        public Task<bool> ApproveDose1(int Id);
        public Task<List<VaccinationReciept>> GetByCentre(int CentreId);
        public Task<bool> ApproveDose2(int Id);
    }
}
