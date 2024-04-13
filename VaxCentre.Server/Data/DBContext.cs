
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VaxCentre.Server.Models;

namespace VaxCentre.Server.Data
{
    public class DBContext : IdentityDbContext
    {
        public DBContext(DbContextOptions<DBContext> options):base(options)
        {
            
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<VaccineCentre> vaccineCentres { get; set; }
        public DbSet<Vaccine> vaccines { get; set; }

    }
}
