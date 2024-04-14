using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using VaxCentre.Server.Data;
using VaxCentre.Server.Models;

namespace VaxCentre.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly DBContext _DBContext;
        public PatientController(DBContext dBContext)
        {
            _DBContext = dBContext;
        }

       
    }
}
