using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaxCentre.Server.Dtos.Account;
using VaxCentre.Server.Models;

namespace VaxCentre.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signinManager;
        public AccountController(UserManager<Account> userManager, SignInManager<Account> signInManager)
        {
            _userManager = userManager;
            _signinManager = signInManager;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> RegisterPatient(PatientRegisterDto PatientRegisterDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var Patient = new Patient
                {
                    UserName = PatientRegisterDto.UserName,
                    Email = PatientRegisterDto.Email,
                    FirstName = PatientRegisterDto.FirstName,
                    LastName = PatientRegisterDto.LastName,
                    SSID = PatientRegisterDto.SSID,
                    PhoneNumber = PatientRegisterDto.PhoneNumber,
                    Address = PatientRegisterDto.Address,
                    AcceptState = 0,
                };
                var createdUser = await _userManager.CreateAsync(Patient, PatientRegisterDto.Password);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(Patient, "Patient");
                    if (roleResult.Succeeded)
                    {
                        return Ok("Successful register Mr./Mrs. "+ Patient.FirstName);
                    }
                    return StatusCode(500, roleResult.Errors);
                }
                return StatusCode(500, createdUser.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, stackTrace = ex.StackTrace });
            }
        }


        [HttpPost("VaccineCentreRegister")]
        public async Task<IActionResult> RegisterVaccineCentre(CentreRegisterDto RegisterDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var Centre = new VaccineCentre
                {
                    UserName = RegisterDto.UserName,
                    Email = RegisterDto.Email,
                    PhoneNumber = RegisterDto.PhoneNumber,
                    Address = RegisterDto.Address,
                    DisplayName = RegisterDto.DisplayName,
                };
                var createdUser = await _userManager.CreateAsync(Centre, RegisterDto.Password);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(Centre, "VaccineCentre");
                    if (roleResult.Succeeded)
                    {
                        return Ok("The vaccine Centre was added to the database with the name: " + Centre.DisplayName);
                    }
                    return StatusCode(500, roleResult.Errors);
                }
                return StatusCode(500, createdUser.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, stackTrace = ex.StackTrace });
            }
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());

            if (user == null) return Unauthorized("Invalid username!");

            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Admin")) return Ok("Admin redirect Mr. " + user.UserName);
            if (roles.Contains("VaccineCentre")) return Ok("Centre redirect Mr. " + user.UserName);
            if (roles.Contains("Patient")) return Ok("Patient redirect Mr. " + user.UserName);
            return BadRequest();
        }
    }
}
