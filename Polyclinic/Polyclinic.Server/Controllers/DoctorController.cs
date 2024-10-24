using Microsoft.AspNetCore.Mvc;
using Polyclinic.Domain.Repositories;
using Polyclinic.Domain;
using AutoMapper;
using Polyclinic.Services.Dto;

namespace Polyclinic.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorController(IRepository<Doctor, int> repository, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Вернуть всех докторов
    /// </summary>
    /// <returns><see cref="Doctor"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet]
    public ActionResult<IEnumerable<Doctor>> Get() => Ok(repository.GetAll());

    /// <summary>
    /// Вернуть доктора по идентификатору
    /// </summary>
    /// <param name="id">идентификатор доктора</param>
    /// <returns><see cref="Doctor"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet("{id}")]
    public ActionResult<Doctor> Get(int id)
    {
        var doctor = repository.Get(id);

        return doctor != null ? Ok(doctor) : NotFound();
    }

    /// <summary>
    /// Добавить доктора
    /// </summary>
    /// <param name="value">объект Dto Доктора</param>
    /// <returns></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpPost]
    public IActionResult Post([FromBody] DoctorDto value)
    {
        var doctor = mapper.Map<Doctor>(value);

        if (Enum.TryParse<SpecializationTypes>(value.Specialization, out var specialization))
            doctor.Specialization = specialization;
        else 
            return NotFound("specialization not found");

        repository.Post(doctor);

        return Ok();
    }

    /// <summary>
    /// Изменить доктора по идентификатору
    /// </summary>
    /// <param name="id">идентификатор доктора</param>
    /// <param name="value">объект Dto Доктора</param>
    /// <returns></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] DoctorDto value)
    {
        var doctor = mapper.Map<Doctor>(value);

        if (Enum.TryParse<SpecializationTypes>(value.Specialization, out var specialization))
            doctor.Specialization = specialization;
        else
            return NotFound("specialization not found");

        return !repository.Put(doctor, id) ? NotFound() : Ok();
    }

    /// <summary>
    /// Удалить доктора по идентификатору
    /// </summary>
    /// <param name="id">идентификатор доктора</param>
    /// <returns></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id) => !repository.Delete(id) ? NotFound() : Ok();
}
