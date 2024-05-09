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
        public VaccineCentreController(IMapper mapper, IVaccineCentreRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
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

    }
}
