using Microsoft.EntityFrameworkCore;
using VaxCentre.Server.Data.Interfaces;
using VaxCentre.Server.Models;

namespace VaxCentre.Server.Data.Repositories
{
    public class RecieptRepository : GenericRepository<VaccinationReciept>,IRecieptRepository
    {
        DBContext _context;

        public RecieptRepository(DBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<VaccinationReciept>> GetAllDetailed()
        {
            try
            {
                var Result = await _context.VaccinationReciepts.Include(r => r.Vaccine).Include(r => r.Patient).Include(r => r.VaccineCentre).ToListAsync();
                return Result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all entities.", ex);
            }
        }
        public async Task<bool> ReserveDose2 (int Id, DateTime date)
        {
            var result = await _context.VaccinationReciepts.Include(r=>r.Vaccine).FirstOrDefaultAsync(x=> x.Id==Id);
            if (result == null) return false;
            TimeSpan timeSpan = date - result.VaccineDose1Date;
            double days = Math.Abs(timeSpan.TotalDays);
            if (result.Vaccine != null && result.Vaccine.GapTime <= days)
            {
                result.VaccineDose2Date = date;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> CheckVaccinePatientExist(int VaccineId, int PatientId)
        {
            var result = await _context.VaccinationReciepts
                                   .Include(r => r.Vaccine).Include(r => r.Patient)
                                   .FirstOrDefaultAsync(x => x.Vaccine != null && x.Patient !=null && x.Vaccine.Id == VaccineId && x.Patient.Id == PatientId);
                                   
            if (result == null) { return false; }
            return true;
        }
        public async Task<bool> ApproveDose1(int Id)
        {
            var reciept = await _context.VaccinationReciepts.FindAsync(Id);
            if (reciept == null)
            {
                return false;
            }
            reciept.Dose1State = 1;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }catch (Exception ex)
            {
                throw new Exception("error in saving",ex);
            }

        }

        public async Task<bool> ApproveDose2(int Id)
        {
            var reciept = await _context.VaccinationReciepts.Include(r => r.Vaccine).FirstOrDefaultAsync(x => x.Id == Id); 
            if (reciept == null )
            {
                return false;
            }
            TimeSpan timeSpan = reciept.VaccineDose2Date - reciept.VaccineDose1Date;
            double days = Math.Abs(timeSpan.TotalDays);
            if (reciept.Dose1State == 1 && reciept.Vaccine.GapTime <= days) reciept.Dose2State = 1;
            else return false;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("error in saving",ex);
            }

        }

        public async Task<List<Patient>> GetByCentre(int CentreId)
        {
            if (CentreId <= 0)
            {
                throw new ArgumentException("Invalid CentreId", nameof(CentreId));
            }

            try
            {
                var query = await _context.VaccinationReciepts
                    .Include(r => r.VaccineCentre)
                    .Include(r => r.Patient)
                    .Where(x => x.VaccineCentre != null && x.VaccineCentre.Id == CentreId)
                    .ToListAsync();

                var result = query.Select(r => r.Patient).ToList();


                if (!result.Any() && result!=null)
                {
                    return [];
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving vaccination receipts for the centre with Id {CentreId}.", ex);
            }
        }

    }
}
