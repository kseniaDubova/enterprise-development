using AutoMapper;
using Polyclinic.Domain;
using Polyclinic.Services.Dto;

namespace Polyclinic.Services;

public class Mapping: Profile
{
    public Mapping()
    {
        CreateMap<Patient, PatientDto>().ReverseMap();
        CreateMap<Doctor, DoctorDto>().ReverseMap();
        CreateMap<Appointment, AppointmentDto>().ReverseMap();
    }
}
