using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaxCentre.Server.Data.Interfaces;
using VaxCentre.Server.Dtos.Account;
using VaxCentre.Server.Models;      

namespace VaxCentre.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IPatientRepository PatientRepository;
        IVaccineCentreRepository VaccineCentreRepository;
        IGenericRepository<Admin> AdminRepository;
        IMapper _mapper;
        public AccountController(IMapper mapper,IPatientRepository patient, IVaccineCentreRepository vaccineCentre, IGenericRepository<Admin> admin)
        {
            PatientRepository = patient;
            VaccineCentreRepository = vaccineCentre;
            AdminRepository = admin;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterPatient(PatientRegisterDto PatientRegisterDto)
        {
            throw new NotImplementedException();
        }


        [HttpPost("VaccineCentreRegister")]
        public async Task<IActionResult> RegisterVaccineCentre(CentreRegisterDto RegisterDto)
        {
            throw new NotImplementedException();
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            throw new NotImplementedException();   
        }
    }
}
