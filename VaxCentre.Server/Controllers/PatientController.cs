using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography;
using VaxCentre.Server.Data;
using VaxCentre.Server.Data.Interfaces;
using VaxCentre.Server.Data.Repositories;
using VaxCentre.Server.Models;
using VaxCentre.Server.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace VaxCentre.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPatientRepository _repository;
        private readonly IRecieptRepository _recieptRepository;
        private readonly IVaccineCentreRepository _vaccineCentreRepository;
        private readonly IVaccineRepository _vaccineRepository;
        private readonly AuthService _authService;
        public PatientController(IVaccineRepository vaccineRepository, IVaccineCentreRepository vaccineCentreRepository, AuthService authService, IRecieptRepository recieptRepository, IMapper mapper, IPatientRepository repository)
        {
            _mapper = mapper;
            _recieptRepository = recieptRepository;
            _repository = repository;
            _authService = authService;
            _vaccineCentreRepository = vaccineCentreRepository;
            _vaccineRepository = vaccineRepository;
        }

        [HttpGet]
        public async Task<IActionResult> DisplayApprovedPatients()
        {
            try
            {
                var result = await _repository.GetByState(1);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the data in the controller method. \n{ex.Message}");
            }
        }
        [HttpGet("Unapproved")]
        public async Task<IActionResult> DisplayUnapprovedPatients(string token)
        {
            //authorize access bye role
            if (!_authService.AuthorizeRole(token, "Admin")) return Unauthorized("Invalid Role authorization");
            try
            {
                var result = await _repository.GetByState(0);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the data in the controller method. \n{ex.Message}");
            }
        }

        [HttpGet("Dose1/{VaccineId}")]
        public async Task<IActionResult> ReserveVaccine(string token,int CentreId,[FromRoute]int VaccineId, DateTime dose1date) 
        {
            var principal = _authService.ValidateToken(token);
            var userName = principal.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            int userId = int.Parse(principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var role = principal.Claims.First(c => c.Type == ClaimTypes.Role).Value;

            if (await _recieptRepository.CheckVaccinePatientExist(VaccineId,userId))
            {
                return BadRequest("Already reserved this vaccine");
            }

            VaccinationReciept reciept = new VaccinationReciept();
            reciept.Patient = await _repository.GetByIdAsync(userId);
            reciept.Vaccine = await _vaccineRepository.GetByIdAsync(VaccineId);
            reciept.VaccineCentre = await _vaccineCentreRepository.GetByIdAsync(CentreId);
            reciept.VaccineDose1Date = dose1date;

            var result = await _recieptRepository.CreateAsync(reciept);
            if (result!=null) return Ok(result);
            return BadRequest();
        }

        [HttpGet("Dose2/{RecieptId}")]
        public async Task<IActionResult> ReserveDose2([FromRoute]int RecieptId, DateTime date,string token) 
        {
            //authorize access bye role
            if (!_authService.AuthorizeRole(token, "Patient")) return Unauthorized("Invalid Role authorization");
            var reciept = await _recieptRepository.GetByIdAsync(RecieptId);
            if (reciept==null) { return BadRequest("Invalid Reciept Id"); }
            if (reciept.Dose1State != 1) return BadRequest("dose 1 was not accepted");
            if (await _recieptRepository.ReserveDose2(RecieptId, date)) return Ok("Success");
            return BadRequest();
        }

        [HttpGet("Download/{RecieptId}")]
        public async Task<IActionResult> DownloadCertificate([FromRoute]int RecieptId, string token)
        {
            try
            {
                //authorize access bye role
                if (!_authService.AuthorizeRole(token, "Patient")) return Unauthorized("Invalid Role authorization");
                var _GetFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files",RecieptId+".png");
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(_GetFilePath, out var _ContentType))
                {
                    _ContentType = "application/octet-stream";
                }
                var _ReadAllBytesAsync = await System.IO.File.ReadAllBytesAsync(_GetFilePath);
                return File(_ReadAllBytesAsync, _ContentType, Path.GetFileName(_GetFilePath));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
