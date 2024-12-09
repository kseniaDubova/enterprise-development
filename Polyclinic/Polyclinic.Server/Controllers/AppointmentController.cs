using Microsoft.AspNetCore.Mvc;
using Polyclinic.Domain.Repositories;
using Polyclinic.Domain;
using AutoMapper;
using Polyclinic.Services.Dto;

namespace Polyclinic.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentController(IRepository<Appointment, int> repositoryAppointment, IRepository<Doctor, int> repositoryDoctor, IRepository<Patient, int> repositoryPatient, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Вернуть все посещения
    /// </summary>
    /// <returns><see cref="Appointment"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Appointment>>> Get() => Ok(await repositoryAppointment.GetAll());

    /// <summary>
    /// Вернуть посещение по идентификатору
    /// </summary>
    /// <param name="id">идентификатор посещений</param>
    /// <returns><see cref="Appointment"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<Appointment>> Get(int id)
    {
        var appointment = await repositoryAppointment.Get(id);

        return appointment != null ? Ok(appointment) : NotFound();
    }

    /// <summary>
    /// Добавить посещение
    /// </summary>
    /// <param name="value">объект Dto Посещений</param>
    /// <returns></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AppointmentDto value)
    {
        var patient = await repositoryPatient.Get(value.IdPatient);

        if (patient == null)
            return NotFound("patient not found");

        var doctor = await repositoryDoctor.Get(value.IdDoctor);

        if (doctor == null)
            return NotFound("doctor not found");

        var appointment = mapper.Map<Appointment>(value);
        appointment.Doctor = doctor;
        appointment.Patient = patient;

        if (Enum.TryParse<ConclusionTypes>(value.Conclusion, out var conclusion))
            appointment.Conclusion = conclusion;

        var minDate = new DateTime(1900, 1, 1);
        var maxDate = DateTime.Today;
        if (value.Date < minDate || value.Date > maxDate)
            return BadRequest("Uncorrect date");

        await repositoryAppointment.Post(appointment);

        return Ok();
    }

    /// <summary>
    /// Изменить посещение по идентификатору
    /// </summary>
    /// <param name="id">идентификатор посещений</param>
    /// <param name="value">объект Dto Посещений</param>
    /// <returns></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] AppointmentDto value) 
    {
        var patient = await repositoryPatient.Get(value.IdPatient);

        if (patient == null)
            return NotFound("patient not found");

        var doctor = await repositoryDoctor.Get(value.IdDoctor);

        if (doctor == null)
            return NotFound("doctor not found");

        var appointment = mapper.Map<Appointment>(value);
        appointment.Doctor = doctor;
        appointment.Patient = patient;

        if (Enum.TryParse<ConclusionTypes>(value.Conclusion, out var conclusion))
            appointment.Conclusion = conclusion;

        appointment.Id = id;

        var minDate = new DateTime(1900, 1, 1);
        var maxDate = DateTime.Today;
        if (value.Date < minDate || value.Date > maxDate)
            return BadRequest("Uncorrect date");

        return await repositoryAppointment.Put(appointment, id) ? Ok() : NotFound(); 
    }

    /// <summary>
    /// Удалить посещение по идентификатору
    /// </summary>
    /// <param name="id">идннтификатор посещений</param>
    /// <returns></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => await repositoryAppointment.Delete(id) ? Ok() : NotFound();
}
