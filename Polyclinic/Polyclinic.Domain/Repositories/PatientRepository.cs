using Microsoft.EntityFrameworkCore;

namespace Polyclinic.Domain.Repositories;
/// <summary>
/// Репозиторий пациентов
/// </summary>
public class PatientRepository(PolyclinicDbContext context) : IRepository<Patient, int>
{
    /// <summary>
    /// Вернуть всех пациентов
    /// </summary>
    /// <returns><see cref="Patient"/></returns>
    public async Task<List<Patient>> GetAll() => await context.Patients.ToListAsync();

    /// <summary>
    /// Вернуть пациента по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns><see cref="Patient"/></returns>
    public async Task<Patient?> Get(int id) => await context.Patients.FindAsync(id);

    /// <summary>
    /// Удалить пациента по идентификатору
    /// </summary>
    /// <param name="id">идентификатор пациента</param>
    /// <returns>false при ошибке,true иначе </returns>
    public async Task<bool> Delete(int id)
    {
        var deletedPatient = await Get(id);

        if (deletedPatient == null) return false; 

        context.Patients.Remove(deletedPatient);
        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Добавить пациента
    /// </summary>
    /// <param name="newObj">объект класса пациента</param>
    public async Task Post(Patient newObj) 
    {
        await context.Patients.AddAsync(newObj);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Изменить пациента по идентификатору
    /// </summary>
    /// <param name="newObj">объект класса пациента</param>
    /// <param name="id">идентификатор пациента</param>
    /// <returns>false при ошибке,true иначе</returns>
    public async Task<bool> Put(Patient newObj, int id)
    {
        var oldPatient = await Get(id);

        if (oldPatient == null) return false;

        context.Patients.Update(newObj);
        await context.SaveChangesAsync();

        return true;
    }
}
