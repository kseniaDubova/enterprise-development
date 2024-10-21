using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Polyclinic.Domain;
using Polyclinic.Domain.Repositories;
using Polyclinic.Services.Dto;
namespace Polyclinic.Server.Controllers;
/// <summary>
/// Класс для работы с данными пациентов
/// </summary>
/// <param name="repository"></param>
[Route("api/[controller]")]
[ApiController]
public class PatientController(IRepository<Patient, int> repository, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Вернуть всех пациентов
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<IEnumerable<Patient>> Get() => Ok(repository.GetAll());

    /// <summary>
    /// Вернуть пациента по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public ActionResult<Patient> Get(int id)
    {
        var patient = repository.Get(id);

        return patient != null ? Ok(patient) : NotFound();
    } 

    /// <summary>
    /// Добавить пациента
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Post([FromBody] PatientDto value)
    {
        var patient = mapper.Map<Patient>(value);
        repository.Post(patient);

        return Ok();
    }

    /// <summary>
    /// Изменить пациента по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] PatientDto value) => !repository.Put(mapper.Map<Patient>(value), id) ? NotFound() : Ok();

    /// <summary>
    /// Удалить пациента по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")] 
    public IActionResult Delete(int id) => !repository.Delete(id) ? NotFound() : Ok();
}
