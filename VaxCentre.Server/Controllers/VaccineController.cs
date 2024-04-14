using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaxCentre.Server.Data;
using VaxCentre.Server.Dtos.Account;
using VaxCentre.Server.Dtos.Vaccine;
using VaxCentre.Server.Models;

namespace VaxCentre.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        private readonly DBContext _DBContext;
        public VaccineController(DBContext dBContext)
        {
            _DBContext = dBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Result = await _DBContext.Vaccines.ToListAsync();
            if (Result != null)
                return Ok(Result);
            return Ok("No vaccines available");
        }


        [HttpGet("{Id}")]
        public IActionResult GetById([FromRoute] int Id) {
            var Result = _DBContext.Vaccines.FirstOrDefault(x => x.Id == Id);
            if (Result != null)
                return Ok(Result);
            return NotFound();
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(InputDto InputDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var Vaccine = new Vaccine
            {
                Name = InputDto.Name,
                Description = InputDto.Description,
                Precaution = InputDto.Precaution,
                GapTime = InputDto.GapTime,
            };
            await _DBContext.Vaccines.AddAsync(Vaccine);
            var saveResult = await _DBContext.SaveChangesAsync();

            if (saveResult > 0) return Ok(Vaccine);
            return BadRequest("Failed to add vaccine to DataBase");
        }


        [HttpPost("Update/{Id}")]
        public async Task<IActionResult> Update(InputDto InputDto, [FromRoute] int Id) {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Updated = _DBContext.Vaccines.FirstOrDefault(x => x.Id == Id);

            if (Updated == null)
                return NotFound();

            Updated.Name = InputDto.Name;

            Updated.Description = InputDto.Description;

            if (InputDto.Precaution != null)
                Updated.Precaution = InputDto.Precaution;

            if (InputDto.GapTime != null)
                Updated.GapTime = InputDto.GapTime;

            _DBContext.Vaccines.Update(Updated);
            await _DBContext.SaveChangesAsync();

            return Ok(Updated);
        }


        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute]int Id)
        {
            var Deleted = _DBContext.Vaccines.FirstOrDefault(x => x.Id == Id);

            if (Deleted == null) return NotFound();

            _DBContext.Vaccines.Remove(Deleted);

            await _DBContext.SaveChangesAsync();

            return Ok(Deleted);
        }

    }
}
