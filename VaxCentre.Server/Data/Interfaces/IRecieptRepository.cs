using VaxCentre.Server.Models;

namespace VaxCentre.Server.Data.Interfaces
{
    public interface IRecieptRepository : IGenericRepository<VaccinationReciept>
    {
        public Task<bool> ApproveDose1(int Id);
        public Task<bool> ApproveDose2(int Id);
        public Task<bool> CheckVaccinePatientExist(int VaccineId, int PatientId);
        public Task<bool> ReserveDose2(int Id, DateTime date);
        public Task<List<Patient>> GetByCentre(int CentreId);
    }
}
