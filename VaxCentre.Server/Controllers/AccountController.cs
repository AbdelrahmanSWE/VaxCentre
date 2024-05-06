using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaxCentre.Server.Data.Interfaces;
using VaxCentre.Server.Dtos.Account;
using VaxCentre.Server.Models;
using VaxCentre.Server.Services;

namespace VaxCentre.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IPatientRepository PatientRepository;
        IVaccineCentreRepository VaccineCentreRepository;
        IAccountRepository AccountRepository;
        IMapper _mapper;
        AuthService _authService;
        public AccountController(IAccountRepository accountRepository,AuthService authenticationService,IMapper mapper,IPatientRepository patient, IVaccineCentreRepository vaccineCentre)
        {
            PatientRepository = patient;
            VaccineCentreRepository = vaccineCentre;
            AccountRepository = accountRepository;
            _mapper = mapper;
            _authService = authenticationService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterPatient(PatientRegisterDto RegisterDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (await AccountRepository.GetByUserNameAsync(RegisterDto.UserName)!=null) return BadRequest("Username is already taken");
            // Map the DTO to the domain model
            var patient = _mapper.Map<Patient>(RegisterDto);
            if (patient.Password == null) return BadRequest(ModelState);
            // Hash and salt the password
            patient.Password = _authService.HashPassword(patient.Password);
            patient.Role = "Patient";
            // Add the patient to the repository
            await PatientRepository.CreateAsync(patient);

            // Return a success response
            return Ok();
        }

        [HttpPost("VaccineCentreRegister")]
        public async Task<IActionResult> RegisterVaccineCentre(CentreRegisterDto RegisterDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            if (await AccountRepository.GetByUserNameAsync(RegisterDto.UserName)!=null) return BadRequest("Username is already taken");
            // Map the DTO to the domain model
            var vaccineCentre = _mapper.Map<VaccineCentre>(RegisterDto);
            if (vaccineCentre.Password == null) return BadRequest(ModelState);
            // Hash and salt the password
            vaccineCentre.Password = _authService.HashPassword(vaccineCentre.Password);
            vaccineCentre.Role = "VaccineCentre";

            // Add the vaccine centre to the repository
            await VaccineCentreRepository.CreateAsync(vaccineCentre);

            // Return a success response
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            // Check if the user exists in any of the repositories
            var account = await AccountRepository.GetByUserNameAsync(loginDto.UserName);

            if (account == null)
            {
                return Unauthorized("error in account");
            }
            if (account.Password==null)
            {
                return Unauthorized("no password");
            }

            // Verify the password
            bool isPasswordValid = _authService.VerifyPassword(loginDto.Password, account.Password);
            if (!isPasswordValid)
            {
                return Unauthorized("Wrong username or password");
            }
            return account.Role switch
            {
                "Admin" => Ok("Admin"),
                "VaccineCentre" => Ok("VaccineCentre"),
                "Patient" => Ok("Patient"),
                _ => Unauthorized("No role assigned"),
            };
        }

    }
}
