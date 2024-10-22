using AutoMapper;
using Polyclinic.Domain;
using Polyclinic.Domain.Repositories;
using Polyclinic.Services.Dto;

namespace Polyclinic.Services;

/// <summary>
/// Класс для корректного преобразования данных из Dto в объекты
/// </summary>
/// <param name="mapper"></param>
public class ComponentsMapper(IMapper mapper)
{
    /// <summary>
    /// Преобразование Dto в объект класса Appointment
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
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
        catch (Exception ex)
        {
            throw new ApplicationException("Doctor's error" + ex.Message, ex);
        }

        return appointment;
    }
    /// <summary>
    ///  Преобразование Dto в объект класса Doctors
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public Doctor GetDoctor(DoctorDto value)
    {
        var newDoctor = mapper.Map<Doctor>(value);
        try
        {
            newDoctor.Specialization = (SpecializationTypes)Enum.Parse(typeof(SpecializationTypes), value.Specialization);
        }
        catch(Exception ex) 
        {
            throw new ApplicationException("Doctor's error" + ex.Message, ex);
        };

        return newDoctor;
    }
}
