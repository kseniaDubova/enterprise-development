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
    public ActionResult<IEnumerable<Appointment>> Get() => Ok(repositoryAppointment.GetAll());

    /// <summary>
    /// Вернуть посещение по идентификатору
    /// </summary>
    /// <param name="id">идентификатор посещений</param>
    /// <returns><see cref="Appointment"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet("{id}")]
    public ActionResult<Appointment> Get(int id)
    {
        var appointment = repositoryAppointment.Get(id);

        return appointment != null ? Ok(appointment) : NotFound();
    }

    /// <summary>
    /// Добавить посещение
    /// </summary>
    /// <param name="value">объект Dto Посещений</param>
    /// <returns></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpPost]
    public IActionResult Post([FromBody] AppointmentDto value)
    {
        var patient = repositoryPatient.Get(value.IdPatient);

        if (patient == null)
            return NotFound("patient not found");

        var doctor = repositoryDoctor.Get(value.IdDoctor);

        if (doctor == null)
            return NotFound("doctor not found");

        var appointment = mapper.Map<Appointment>(value);

        if (Enum.TryParse<ConclusionTypes>(value.Conclusion, out var conclusion))
            appointment.Conclusion = conclusion;
        else
            return NotFound("specialization not found");

        repositoryAppointment.Post(appointment);

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
    public IActionResult Put(int id, [FromBody] AppointmentDto value) 
    {
        var patient = repositoryPatient.Get(value.IdPatient);

        if (patient == null)
            return NotFound("patient not found");

        var doctor = repositoryDoctor.Get(value.IdDoctor);

        if (doctor == null)
            return NotFound("doctor not found");

        var appointment = mapper.Map<Appointment>(value);

        if (Enum.TryParse<ConclusionTypes>(value.Conclusion, out var conclusion))
            appointment.Conclusion = conclusion;
        else
            return NotFound("conclision not found");

        appointment.Id = id;

        return !repositoryAppointment.Put(appointment, id) ? NotFound() : Ok(); 
    }

    /// <summary>
    /// Удалить посещение по идентификатору
    /// </summary>
    /// <param name="id">идннтификатор посещений</param>
    /// <returns></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id) => !repositoryAppointment.Delete(id) ? NotFound() : Ok();
}
