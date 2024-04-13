using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using VaxCentre.Server.Data;
using VaxCentre.Server.Models;

namespace VaxCentre.Server.Controllers
{
    [Route("api/patient/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly DBContext _DBContext;
        public PatientController(DBContext dBContext)
        {
            _DBContext = dBContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Patient patient)
        {
            if (patient == null)
            {
                Console.WriteLine("Null patient");
                return BadRequest();
            }
            PasswordHasher<Patient> passwordHasher = new PasswordHasher<Patient>();
            patient.password = passwordHasher.HashPassword(patient, patient.password);
            _DBContext.Patients.Add(patient);
            await _DBContext.SaveChangesAsync();
            return Ok(patient);
        }
        [HttpPost]
        public async Task<IActionResult> Authenticate(Patient patient)
        {
            if (patient != null)
            {
                Patient found = await _DBContext.FindAsync<Patient>(patient.SSID);
                if (found != null)
                {
                    if (found.password == patient.password) 
                    {
                        return Ok(patient);
                    }
                }
            }
            return BadRequest();
        }
    }
}
