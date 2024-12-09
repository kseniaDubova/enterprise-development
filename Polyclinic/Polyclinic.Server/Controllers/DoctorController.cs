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
    public async Task<ActionResult<IEnumerable<Doctor>>> Get() => Ok(await repository.GetAll());

    /// <summary>
    /// Вернуть доктора по идентификатору
    /// </summary>
    /// <param name="id">идентификатор доктора</param>
    /// <returns><see cref="Doctor"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<Doctor>> Get(int id)
    {
        var doctor = await repository.Get(id);

        return doctor != null ? Ok(doctor) : NotFound();
    }

    /// <summary>
    /// Добавить доктора
    /// </summary>
    /// <param name="value">объект Dto Доктора</param>
    /// <returns></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] DoctorDto value)
    {
        var doctor = mapper.Map<Doctor>(value);

        if (Enum.TryParse<SpecializationTypes>(value.Specialization, out var specialization))
            doctor.Specialization = specialization;

        var minDate = new DateTime(1900, 1, 1);
        var maxDate = DateTime.Today;
        if (value.Birth < minDate || value.Birth > maxDate)
            return BadRequest("Uncorrect date");

        await repository.Post(doctor);

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
    public async Task<IActionResult> Put(int id, [FromBody] DoctorDto value)
    {
        var doctor = mapper.Map<Doctor>(value);

        if (Enum.TryParse<SpecializationTypes>(value.Specialization, out var specialization))
            doctor.Specialization = specialization;

        var minDate = new DateTime(1900, 1, 1);
        var maxDate = DateTime.Today;
        if (value.Birth < minDate || value.Birth > maxDate)
            return BadRequest("Uncorrect date");

        return await repository.Put(doctor, id) ? Ok() : NotFound();
    }

    /// <summary>
    /// Удалить доктора по идентификатору
    /// </summary>
    /// <param name="id">идентификатор доктора</param>
    /// <returns></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => await repository.Delete(id) ? Ok() : NotFound();
}
