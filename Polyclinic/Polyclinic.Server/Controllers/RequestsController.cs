using Microsoft.AspNetCore.Mvc;
using Polyclinic.Domain;
using Polyclinic.Domain.Repositories;

namespace Polyclinic.Server.Controllers;
/// <summary>
/// Класс для работы с запросами
/// </summary>
/// <param name="repositoryDoctor">репозиторий докторов</param>
/// <param name="repositoryAppointment">репозиторий посещений</param>
[Route("api/[controller]")]
[ApiController]
public class RequestsController(IRepository<Doctor, int> repositoryDoctor, IRepository<Appointment, int> repositoryAppointment) : ControllerBase
{
    /// <summary>
    /// Вывод всех докторов, опыт которых больше 10 лет
    /// </summary>
    /// <returns><see cref="Doctor"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet("experience-doctors")]
    public async Task<ActionResult<IEnumerable<Doctor>>> GetExperienceDoctors()
    {
        var _doctors = await repositoryDoctor.GetAll();

        var doctors = _doctors
            .Where(d => d.Experience > 10).ToList();

        return Ok(doctors);
    }

    /// <summary>
    /// Вывод всех пациентов указанного доктора, сортировка по имени
    /// </summary>
    /// <param name="id">идентификатор пациента</param>
    /// <returns><see cref="Patient"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet("patients-of-doctor/{id}")]
    public async Task<ActionResult<IEnumerable<Patient>>> GetPatientsOfDoctor(int id)
    {
        var _patients = await repositoryAppointment.GetAll();

        var patients = _patients
            .Where(a => a.Doctor.Id == id)
            .Select(p => p.Patient)
            .Distinct()
            .OrderBy(p => p.FullName)
            .ToList();

        return Ok(patients);
    }

    /// <summary>
    /// Вывод здоровых на данный момент пациентов
    /// </summary>
    /// <returns><see cref="Patient"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet("healthy-patients")]
    public async Task<ActionResult<IEnumerable<Patient>>> GetHealthyPatients()
    {
        var _patients = await repositoryAppointment.GetAll();

        var patients = _patients
            .Where(a => a.Conclusion == ConclusionTypes.Healthy)
            .Select(p => p.Patient)
            .Distinct()
            .ToList();

        return Ok(patients);
    }

    /// <summary>
    /// Вывод количества приемов пациентов по врачам за месяц
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet("count-appointment")]
    public async Task<IActionResult> GetCountAppointment()
    {
        var month = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        var _appointments = await repositoryAppointment.GetAll();

        var appointments = _appointments
            .Where(a => a.Date >= month)
            .GroupBy(p => p.Doctor.Passport)
            .Select(g => new
            {
                Doctor = g.Key,
                Count = g.Count(),
            })
            .OrderBy(g => g.Doctor)
            .ToList();

        return Ok(appointments);
    }

    /// <summary>
    /// Вывод топа заболеваний
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet("disease-top")]
    public async Task<IActionResult> GetSpetialisationTop()
    {
        var _spetialisations = await repositoryAppointment.GetAll();

        var spetialisations = _spetialisations
            .GroupBy(p => p.Doctor.Specialization)
            .Select(g => new
            {
                Specialization = g.Key,
                Count = g.Count(),
            })
            .OrderByDescending(g => g.Count)
            .Take(5)
            .ToList();

        return Ok(spetialisations);
    }

    /// <summary>
    /// Вывод пациентов, записаных к нескольким врачам, сортировка по дате рождения
    /// </summary>
    /// <returns><see cref="Patient"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet("patients-with-several-appointment")]
    public async Task<ActionResult<IEnumerable<Patient>>> GetPatientsWithSeveralAppointment()
    {
        var today = DateTime.Now;

        var _patients = await repositoryAppointment.GetAll();

        var patients = _patients
            .Where(a => (today.Year - a.Patient.Birth.Year) > 30)
            .GroupBy(p => p.Patient)
            .Where(g => g.Select(a => a.Doctor.Passport).Distinct().Count() > 1)
            .Select(g => g.Key)
            .OrderBy(p => p.Birth)
            .ToList();

        return Ok(patients); 
    }

}
