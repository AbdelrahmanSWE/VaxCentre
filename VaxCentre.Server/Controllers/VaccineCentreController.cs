using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaxCentre.Server.Data;
using VaxCentre.Server.Data.Interfaces;
using VaxCentre.Server.Dtos.Account;
using VaxCentre.Server.Dtos.Patient;
using VaxCentre.Server.Dtos.Vaccine;
using VaxCentre.Server.Dtos.VaccineCentre;
using VaxCentre.Server.Models;
using VaxCentre.Server.Services;


namespace VaxCentre.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineCentreController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVaccineCentreRepository _repository;
        private readonly IRecieptRepository _recieptRepository;
        private readonly AuthService _authService;
        public VaccineCentreController(AuthService authService,IRecieptRepository recieptRepository,IMapper mapper, IVaccineCentreRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
            _recieptRepository = recieptRepository;
            _authService = authService;
        }


        [HttpGet]
        public async Task<IActionResult> DisplayVaccineCentres()
        {
            try
            {

                var result = await _repository.GetAllAsync();
                var resultDto = _mapper.Map<List<VaccineCentreHeaderDto>>(result);
                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the data in the controller method. \n{ex.Message}");
            }
        }
        [HttpGet("Reciept")]
        public async Task<IActionResult> DisplayAllReciepts()
        {
            try
            {
                var result = await _recieptRepository.GetAllDetailed();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the data in the controller method. \n{ex.Message}");
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCentre([FromRoute] int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest("Invalid Id");
                }
                var result = await _repository.GetByIdAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the data in the controller method. \n{ex.Message}");
            }
        }

        [HttpPost("Update/{Id}")]
        public async Task<IActionResult> UpdateVaccineCentre(UpdateVaccineCentreDto input, [FromRoute] int Id, string token)
        {
            try
            {
                //authorize access bye role
                if (!_authService.AuthorizeRole(token, "VaccineCentre")) return Unauthorized("Invalid Role authorization");
                if (Id <= 0)
                {
                    return BadRequest("Invalid Id");
                }
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                VaccineCentre vaccine = _mapper.Map<VaccineCentre>(input);
                var result = await _repository.UpdateAsync(vaccine, Id);

                if (result != null)
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the data in the controller method. \n{ex.Message}");
            }
        }

        [HttpGet("Assign/{Id}")]
        public async Task<IActionResult> AssignVaccineToCentre([FromRoute] int Id, int VaccineId, string token)
        {
            try
            {
                //authorize access bye role
                if (!_authService.AuthorizeRole(token, "VaccineCentre")) return Unauthorized("Invalid Role authorization");
                var vaccineCentre = await _repository.AssignVaccineToCentre(Id, VaccineId);


                return Ok(vaccineCentre.Vaccines.Select(v => v.Id).ToList());

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> RemoveVaccineCentre([FromRoute] int Id,string token)
        {
            try
            {
                //authorize access bye role
                if (!_authService.AuthorizeRole(token, "Admin")) return Unauthorized("Invalid Role authorization");
                if (Id <= 0)
                {
                    return BadRequest("Invalid Id");
                }

                var result = await _repository.DeleteAsync(Id);

                if (!result)
                {
                    return NotFound($"Vaccine centre with Id {Id} not found");
                }

                return Ok("Vaccine centre deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the data in the controller method. \n{ex.Message}");
            }
        }

        [HttpGet("Registered/{VaccineCentreId}")]
        public async Task<IActionResult> GetRegisteredPatients([FromRoute]int VaccineCentreId, string token)
        {
            //authorize access bye role
            if (!_authService.AuthorizeRole(token, "Admin")) return Unauthorized("Invalid Role authorization");
            var result = await _recieptRepository.GetByCentre(VaccineCentreId);
            return Ok(result);
        }

        [HttpGet("ApproveDos1/{RecieptId}")]
        public async Task<IActionResult> ApproveDose1([FromRoute]int RecieptId, string token)
        {
            //authorize access bye role
            if (!_authService.AuthorizeRole(token, "VaccineCentre")) return Unauthorized("Invalid Role authorization");
            if ( await _recieptRepository.ApproveDose1(RecieptId))return Ok(RecieptId);
            return BadRequest();
        }

        [HttpGet("ApproveDos2/{RecieptId}")]
        public async Task<IActionResult> ApproveDose2([FromRoute] int RecieptId, string token)
        {
            //authorize access bye role
            if (!_authService.AuthorizeRole(token, "VaccineCentre")) return Unauthorized("Invalid Role authorization");
            if (await _recieptRepository.ApproveDose2(RecieptId)) return Ok(RecieptId);
            return BadRequest();
        }


        [HttpPost("Upload/{RecieptId}")]
        public async Task<string> UploadFile(IFormFile _IFormFile, [FromRoute]int RecieptId, string token)
        {
            //authorize access bye role
            if (!_authService.AuthorizeRole(token, "VaccineCentre")) return "Invalid Role authorization";
            var result = await _recieptRepository.GetByIdAsync(RecieptId);
            if (result.Dose2State != 1) return "not available";
            string FileName = "";
            try
            {
                FileInfo _FileInfo = new FileInfo(_IFormFile.FileName);
                FileName = RecieptId + _FileInfo.Extension;
                var _GetFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");
                if (!Directory.Exists(_GetFilePath)) Directory.CreateDirectory(_GetFilePath);
                var FileDirectory = Path.Combine(Directory.GetCurrentDirectory(),_GetFilePath, FileName);
                using (var _FileStream = new FileStream(FileDirectory, FileMode.Create))
                {
                    await _IFormFile.CopyToAsync(_FileStream);
                }
                return FileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
