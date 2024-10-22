using Microsoft.AspNetCore.Mvc;
using Polyclinic.Domain.Repositories;
using Polyclinic.Domain;
using AutoMapper;
using Polyclinic.Services.Dto;
using Polyclinic.Services;

namespace Polyclinic.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentController(IRepository<Appointment, int> repository, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Вернуть все посещения
    /// </summary>
    /// <returns><see cref="Appointment"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet]
    public ActionResult<IEnumerable<Appointment>> Get() => Ok(repository.GetAll());

    /// <summary>
    /// Вернуть посещение по идентификатору
    /// </summary>
    /// <param name="id">идентификатор посещений</param>
    /// <returns><see cref="Appointment"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet("{id}")]
    public ActionResult<Appointment> Get(int id)
    {
        var appointment = repository.Get(id);

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
        ComponentsMapper servise = new(mapper);
        var appointment = servise.GetAppointment(value);
        repository.Post(appointment);

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
        ComponentsMapper servise = new(mapper);
        var appointment = servise.GetAppointment(value);
        appointment.Id = id;

        return !repository.Put(appointment, id) ? NotFound() : Ok(); 
    }

    /// <summary>
    /// Удалить посещение по идентификатору
    /// </summary>
    /// <param name="id">идннтификатор посещений</param>
    /// <returns></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id) => !repository.Delete(id) ? NotFound() : Ok();
}
