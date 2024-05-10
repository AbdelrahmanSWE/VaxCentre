using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
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
        public AccountController(IAccountRepository accountRepository,
            AuthService authenticationService,
            IMapper mapper,
            IPatientRepository patient,
            IVaccineCentreRepository vaccineCentre)
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
            string token = _authService.GenerateJwtToken(account);
            if (account.Role=="Patient")
            {
                var patient = await PatientRepository.GetByIdAsync(account.Id);
                if (patient!=null&&patient.AcceptState == 0) return Unauthorized("User not accepted by admin");
            }
            return account.Role switch
            {
                "Admin" => Ok(new { token }),
                "VaccineCentre" => Ok(new { token }),
                "Patient" => Ok(new { token }),
                _ => Unauthorized("Unauth"),
            };
        }

        [HttpGet("Activate/{Id}")]
        public async Task<IActionResult> ActivateAccount([FromRoute] int Id, string token)
        {
            //authorize access bye role
            if (!_authService.AuthorizeRole(token, "Admin")) return Unauthorized("Invalid Role authorization");

            if (Id <= 0)
            {
                return BadRequest("Invalid Id");
            }

            try
            {
                var patient = await PatientRepository.ChangeAcceptState(Id);

                if (patient == null)
                {
                    return NotFound($"Patient with Id {Id} not found");
                }
                return Ok(patient);
            }
            catch (Exception ex)
            { 
                return StatusCode(500, $"An error occurred while activating the account {ex}");
            }
        }

    }
}
