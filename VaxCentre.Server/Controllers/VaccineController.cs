using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VaxCentre.Server.Data.Interfaces;
using VaxCentre.Server.Dtos.Vaccine;
using VaxCentre.Server.Models;
using VaxCentre.Server.Services;

namespace VaxCentre.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVaccineRepository _repository;
        private readonly AuthService _authService;
        public VaccineController(AuthService authService,IMapper mapper, IVaccineRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> DisplayVaccines()
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
        public async Task<IActionResult> GetVaccine([FromRoute] int Id) {
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

        [HttpGet("/SearchByCentre/{Id}")]
        public async Task<IActionResult> GetVaccinesByCentre([FromRoute] int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return BadRequest("Invalid Id");
                }
                var vaccines = await _repository.GetByCentre(Id);

                var vaccineDtos = _mapper.Map<List<VaccineDisplayDto>>(vaccines);

                return Ok(vaccineDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the data in the controller method. \n{ex.Message}");
            }
        }


        [HttpGet("search/{query}")]
        public async Task<IActionResult> SearchVaccine([FromRoute] string query)
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
        public async Task<IActionResult> CreateVaccine( Dictionary<string, string> data)
        {
            try
            {
                string token = data["token"];
                // Remove the token from the data dictionary
                data.Remove("token");
                CreateVaccineDto input = new CreateVaccineDto();
                input.Name = data["Name"];
                input.Description = data["Description"];
                input.Precaution = data["Precaution"];
                input.GapTime = int.Parse(data["GapTime"]);
                //authorize access bye role
                if (!_authService.AuthorizeRole(token, "Admin")) return Unauthorized("Invalid Role authorization");
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
        public async Task<IActionResult> UpdateVaccine(Dictionary<string, string> data, [FromRoute] int Id)
        {
            try
            {
                string token = data["token"];
                UpdateVaccineDto input = new UpdateVaccineDto {
                    Name = data["Name"],
                    Description = data["Description"],
                    Precaution = data["Precaution"],
                    GapTime = int.Parse(data["GapTime"])
                };
                //authorize access bye role
                if (!_authService.AuthorizeRole(token, "Admin")) return Unauthorized("Invalid Role authorization");
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


        [HttpPost("Delete/{Id}")]
        public async Task<IActionResult> RemoveVaccine([FromRoute] int Id, Dictionary<string, string> data)
        {
            try
            {
                string token=data["token"];
                //authorize access bye role
                if (!_authService.AuthorizeRole(token, "Admin")) return Unauthorized("Invalid Role authorization");
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
