using AutoMapper;
using VaxCentre.Server.Dtos.Account;
using VaxCentre.Server.Dtos.Patient;
using VaxCentre.Server.Dtos.Vaccine;
using VaxCentre.Server.Dtos.VaccineCentre;
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
            CreateMap<Vaccine, VaccineDisplayDto>();

            //Patient
            CreateMap<PatientRegisterDto, Patient>();
            CreateMap<Patient, DisplayPatientNameDto>();

            //VaccineCentre
            CreateMap<CentreRegisterDto, VaccineCentre>();
            CreateMap<UpdateVaccineCentreDto, VaccineCentre>();
            CreateMap<VaccineCentre, VaccineCentreHeaderDto>();
        }

    }
}
