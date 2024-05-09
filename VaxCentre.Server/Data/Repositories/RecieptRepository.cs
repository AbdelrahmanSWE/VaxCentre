using Microsoft.EntityFrameworkCore;
using VaxCentre.Server.Data.Interfaces;
using VaxCentre.Server.Models;

namespace VaxCentre.Server.Data.Repositories
{
    public class RecieptRepository : GenericRepository<VaccinationReciept>,IRecieptRepository
    {
        DBContext _context;
        private object x;

        public RecieptRepository(DBContext context) : base(context)
        {
            _context = context;
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
            var reciept = await _context.VaccinationReciepts.Include(r => r.Vaccine).FirstOrDefaultAsync(x => x.Id == Id); ;
            if (reciept == null )
            {
                return false;
            }
            TimeSpan timeSpan = reciept.VaccineDose1Date - reciept.VaccineDose2Date;
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

        public async Task<List<VaccinationReciept>> GetByCentre(int CentreId)
        {
            if (CentreId <= 0)
            {
                throw new ArgumentException("Invalid CentreId", nameof(CentreId));
            }

            try
            {
                var result = await _context.VaccinationReciepts
                                   .Include(r => r.VaccineCentre)
                                   .Where(x => x.VaccineCentre != null && x.VaccineCentre.Id == CentreId)
                                   .ToListAsync();

                if (!result.Any())
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
