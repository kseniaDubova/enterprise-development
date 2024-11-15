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
        var doctors = await repositoryDoctor.GetAll();

        return Ok(doctors.Where(d => d.Experience > 10).ToList());
    }

    /// <summary>
    /// Вывод всех пациентов указанного доктора, сортировка по имени
    /// </summary>
    /// <param name="id">идентификатор доктора</param>
    /// <returns><see cref="Patient"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet("patients-of-doctor/{id}")]
    public async Task<ActionResult<IEnumerable<Patient>>> GetPatientsOfDoctor(int id)
    {
        var appointments = await repositoryAppointment.GetAll();

        return Ok(appointments.Where(a => a.Doctor.Id == id)
            .Select(p => p.Patient)
            .Distinct()
            .OrderBy(p => p.FullName)
            .ToList());
    }

    /// <summary>
    /// Вывод здоровых на данный момент пациентов
    /// </summary>
    /// <returns><see cref="Patient"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet("healthy-patients")]
    public async Task<ActionResult<IEnumerable<Patient>>> GetHealthyPatients()
    {
        var appointments = await repositoryAppointment.GetAll();

        return Ok(appointments.Where(a => a.Conclusion == ConclusionTypes.Healthy)
            .Select(p => p.Patient)
            .Distinct()
            .ToList());
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

        var appointments = await repositoryAppointment.GetAll();

        return Ok(appointments.Where(a => a.Date >= month)
            .GroupBy(p => p.Doctor.Id)
            .Select(g => new
            {
                Doctor = g.Key,
                Count = g.Count(),
            })
            .OrderBy(g => g.Doctor)
            .ToList());
    }

    /// <summary>
    /// Вывод топа заболеваний
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet("disease-top")]
    public async Task<IActionResult> GetSpetializationTop()
    {
        var appointments = await repositoryAppointment.GetAll();

        return Ok(appointments.GroupBy(p => p.Doctor.Specialization)
            .Select(g => new
            {
                Specialization = g.Key,
                Count = g.Count(),
            })
            .OrderByDescending(g => g.Count)
            .Take(5)
            .ToList());
    }

    /// <summary>
    /// Вывод пациентов, записаных к нескольким врачам, сортировка по дате рождения (старше 30 лет)
    /// </summary>
    /// <returns><see cref="Patient"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet("patients-with-several-appointment")]
    public async Task<ActionResult<IEnumerable<Patient>>> GetPatientsWithSeveralAppointment()
    {
        var today = DateTime.Now;

        var appointments = await repositoryAppointment.GetAll();

        return Ok(appointments.Where(a => (today.Year - a.Patient.Birth.Year) > 30)
            .GroupBy(p => p.Patient)
            .Where(g => g.Select(a => a.Doctor.Id).Distinct().Count() > 1)
            .Select(g => g.Key)
            .OrderBy(p => p.Birth)
            .ToList()); 
    }

}
