using AutoMapper;
using VaxCentre.Server.Dtos.Vaccine;
using VaxCentre.Server.Models;

namespace VaxCentre.Server.Mapper
{
    public class AutoMapperProfile : Profile
    {      
        public AutoMapperProfile()
        {
            CreateMap<CreateVaccineDto, Vaccine>();
            CreateMap<UpdateVaccineDto, Vaccine>();
        }

    }
}
