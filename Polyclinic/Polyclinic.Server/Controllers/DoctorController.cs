using Microsoft.AspNetCore.Mvc;
using Polyclinic.Domain.Repositories;
using Polyclinic.Domain;
using AutoMapper;
using Polyclinic.Services.Dto;
using Polyclinic.Services;

namespace Polyclinic.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorController(IRepository<Doctor, int> repository, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Вернуть всех докторов
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<IEnumerable<Doctor>> Get() => Ok(repository.GetAll());

    /// <summary>
    /// Вернуть доктора по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public ActionResult<Doctor> Get(int id)
    {
        var doctor = repository.Get(id);

        return doctor != null ? Ok(doctor) : NotFound();
    }

    /// <summary>
    /// Добавить доктора
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Post([FromBody] DoctorDto value)
    {
        ComponentsMapper servise = new(mapper);
        var doctor = servise.GetDoctor(value);
        repository.Post(doctor);

        return Ok();
    }

    /// <summary>
    /// Изменить доктора по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] DoctorDto value)
    {
        ComponentsMapper servise = new(mapper);
        var doctor = servise.GetDoctor(value);

        return !repository.Put(doctor, id) ? NotFound() : Ok();
    }

    /// <summary>
    /// Удалить доктора по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id) => !repository.Delete(id) ? NotFound() : Ok();
}
