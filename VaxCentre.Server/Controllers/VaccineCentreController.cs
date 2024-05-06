using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaxCentre.Server.Data;
using VaxCentre.Server.Data.Interfaces;
using VaxCentre.Server.Dtos.Account;
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


    }
}
