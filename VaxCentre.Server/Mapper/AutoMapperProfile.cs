using AutoMapper;
using VaxCentre.Server.Dtos.Account;
using VaxCentre.Server.Dtos.Vaccine;
using VaxCentre.Server.Models;

namespace VaxCentre.Server.Mapper
{
    public class AutoMapperProfile : Profile
    {      
        public AutoMapperProfile()
        {
            //Vaccine
            CreateMap<CreateVaccineDto, Vaccine>();
            CreateMap<UpdateVaccineDto, Vaccine>();

            //Patient
            CreateMap<PatientRegisterDto, Patient>();
        }

    }
}
