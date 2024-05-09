using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaxCentre.Server.Data;
using VaxCentre.Server.Data.Interfaces;
using VaxCentre.Server.Dtos.Account;
using VaxCentre.Server.Dtos.Vaccine;
using VaxCentre.Server.Dtos.VaccineCentre;
using VaxCentre.Server.Models;


namespace VaxCentre.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineCentreController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVaccineCentreRepository _repository;
        private readonly IRecieptRepository _recieptRepository;
        public VaccineCentreController(IRecieptRepository recieptRepository,IMapper mapper, IVaccineCentreRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
            _recieptRepository = recieptRepository;
        }

        [HttpGet]
        public async Task<IActionResult> DisplayVaccineCentres()
        {
            try
            {
                var result = await _repository.GetAllAsync();
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
        public async Task<IActionResult> UpdateVaccineCentre(UpdateVaccineCentreDto input, [FromRoute] int Id)
        {
            try
            {
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
        public async Task<IActionResult> AssignVaccineToCentre([FromRoute] int Id, int VaccineId)
        {
            try
            {
                var vaccineCentre = await _repository.AssignVaccineToCentre(Id, VaccineId);


                return Ok(vaccineCentre.Vaccines.Select(v => v.Id).ToList());

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> RemoveVaccineCentre([FromRoute] int Id)
        {
            try
            {
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
        public async Task<IActionResult> GetRegisteredPatients([FromRoute]int VaccineCentreId)
        {
            var result = await _recieptRepository.GetByCentre(VaccineCentreId);
            return Ok(result);
        }
        [HttpGet("ApproveDos1/{RecieptId}")]
        public async Task<IActionResult> ApproveDose1([FromRoute]int RecieptId)
        {
            if( await _recieptRepository.ApproveDose1(RecieptId))return Ok(RecieptId);
            return BadRequest();
        }

        [HttpGet("ApproveDos2/{RecieptId}")]
        public async Task<IActionResult> ApproveDose2([FromRoute] int RecieptId)
        {
            if (await _recieptRepository.ApproveDose2(RecieptId)) return Ok(RecieptId);
            return BadRequest();
        }
        [HttpPost("Upload/{RecieptId}")]
        public async Task<string> UploadFile(IFormFile _IFormFile, [FromRoute]int RecieptId)
        {
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
