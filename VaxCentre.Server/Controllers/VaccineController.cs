using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VaxCentre.Server.Dtos.Vaccine;
using VaxCentre.Server.Interfaces;
using VaxCentre.Server.Models;

namespace VaxCentre.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVaccineRepository _repository;
        public VaccineController(IMapper mapper, IVaccineRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVaccines()
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
        public async Task<IActionResult> GetById([FromRoute] int Id) {
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

        [HttpGet("search/{query}")]
        public async Task<IActionResult> GetByName([FromRoute] string query)
        {
            try
            {
                if (!string.IsNullOrEmpty(query))
                {
                    var result = await _repository.GetByNameAsync(query);
                    return Ok(result);
                }
                return BadRequest("Please provide a valid query.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the data in the controller method. \n{ex.Message}");
            }
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateVaccine(CreateVaccineDto input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Vaccine vaccine = _mapper.Map<Vaccine>(input);
                    var result = await _repository.CreateAsync(vaccine);
                    if (result != null) return Ok(result);
                }
                return BadRequest("Failed to save vaccine");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the data in the controller method. \n{ex.Message}");
            }
        }


        [HttpPost("Update/{Id}")]
        public async Task<IActionResult> UpdateVaccine(UpdateVaccineDto input, [FromRoute] int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest("Invalid Id");
                }
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Vaccine vaccine = _mapper.Map<Vaccine>(input);
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


        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> DeleteVaccine([FromRoute] int Id)
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
                    return NotFound($"Vaccine with Id {Id} not found");
                }

                return Ok("Vaccine deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the data in the controller method. \n{ex.Message}");
            }
        }

    }
}
