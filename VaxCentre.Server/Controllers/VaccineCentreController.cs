using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaxCentre.Server.Data;
using VaxCentre.Server.Dtos.Account;
using VaxCentre.Server.Models;


namespace VaxCentre.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineCentreController: ControllerBase
    {
        private readonly DBContext _DBContext;
        public VaccineCentreController(DBContext dBContext)
        {
            _DBContext = dBContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Result = await _DBContext.VaccineCentres.ToListAsync();
            if (Result != null)
                return Ok(Result);
            return Ok("No vaccines available");

        }
        [HttpGet("{Id}")]
        public IActionResult GetById([FromRoute] string Id)
        {
            var Result = _DBContext.VaccineCentres.FirstOrDefault(x => x.Id == Id);
            if (Result != null)
                return Ok(Result);
            return NotFound();
        }
    }
}
