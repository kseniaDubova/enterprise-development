using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Polyclinic.Domain;
using Polyclinic.Domain.Repositories;
using Polyclinic.Services.Dto;
namespace Polyclinic.Server.Controllers;

/// <summary>
/// Класс для работы с данными пациентов
/// </summary>
/// <param name="repository">репозиторий пациентов</param>
/// <param name="mapper"></param>
[Route("api/[controller]")]
[ApiController]
public class PatientController(IRepository<Patient, int> repository, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Вернуть всех пациентов
    /// </summary>
    /// <returns><see cref="Patient"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Patient>>> Get() => Ok(await repository.GetAll());

    /// <summary>
    /// Вернуть пациента по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор пациента</param>
    /// <returns><see cref="Patient"/></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<Patient>> Get(int id)
    {
        var patient = await repository.Get(id);

        return patient != null ? Ok(patient) : NotFound();
    }

    /// <summary>
    /// Добавить пациента
    /// </summary>
    /// <param name="value">объект Dto Пациент</param>
    /// <returns></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PatientDto value)
    {
        var patient = mapper.Map<Patient>(value);
        await repository.Post(patient);

        var minDate = new DateTime(1900, 1, 1);
        var maxDate = DateTime.Today;
        if (value.Birth < minDate || value.Birth > maxDate)
            return BadRequest("Uncorrect date");

        return Ok();
    }

    /// <summary>
    /// Изменить пациента по идентификатору
    /// </summary>
    /// <param name="id">идентификатор пациента</param>
    /// <param name="value">объект Dto Пациент</param>
    /// <returns></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] PatientDto value)
    {
        var minDate = new DateTime(1900, 1, 1);
        var maxDate = DateTime.Today;
        if (value.Birth < minDate || value.Birth > maxDate)
            return BadRequest("Uncorrect date");

        return await repository.Put(mapper.Map<Patient>(value), id) ? Ok() : NotFound(); 
    }

    /// <summary>
    /// Удалить пациента по идентификатору
    /// </summary>
    /// <param name="id">идентификатор пациента</param>
    /// <returns></returns>
    /// <response code="200">Запрос выполнен успешно</response>
    [HttpDelete("{id}")] 
    public async Task<IActionResult> Delete(int id) => await repository.Delete(id) ? Ok() : NotFound();
}
