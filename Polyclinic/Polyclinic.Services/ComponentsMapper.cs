using AutoMapper;
using Polyclinic.Domain;
using Polyclinic.Domain.Repositories;
using Polyclinic.Services.Dto;

namespace Polyclinic.Services;

public class ComponentsMapper(IMapper mapper)
{
    public Appointment GetAppointment(AppointmentDto value)
    {
        var appointment = mapper.Map<Appointment>(value);

        PatientRepository patientRepository = new();
        var patient = patientRepository.Get(value.IdPatient);
        if (patient == null)
        {
            appointment.Patient = null;
        }
        else
        {
            appointment.Patient = patient;
        };

        DoctorRepository doctorRepository = new();
        var doctor = doctorRepository.Get(value.IdDoctor);
        if (doctor == null)
        {
            appointment.Doctor = null;
        }
        else
        {
            appointment.Doctor = doctor;
        };

        try
        {
            appointment.Conclusion = (ConclusionTypes)Enum.Parse(typeof(ConclusionTypes), value.Conclusion);

        }
        catch (Exception)
        {

            throw;
        }

        return appointment;
    }

    public Doctor GetDoctor(DoctorDto value)
    {
        var newDoctor = mapper.Map<Doctor>(value);
        try
        {
            newDoctor.Specialization = (SpecializationTypes)Enum.Parse(typeof(SpecializationTypes), value.Specialization);
        }
        catch(Exception) 
        {
            throw;
        };

        return newDoctor;
    }
}
