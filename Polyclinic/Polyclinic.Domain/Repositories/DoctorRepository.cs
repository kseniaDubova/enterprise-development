using Microsoft.EntityFrameworkCore;

namespace Polyclinic.Domain.Repositories;
/// <summary>
/// Репозиторий докторов
/// </summary>
public class DoctorRepository(PolyclinicDbContext context) : IRepository<Doctor, int>
{
    /// <summary>
    /// Вернуть всех докторов
    /// </summary>
    /// <returns><see cref="Doctor"/></returns>
    public async Task<List<Doctor>> GetAll() => await context.Doctors.ToListAsync();

    /// <summary>
    /// Вернуть доктора по идентификатору
    /// </summary>
    /// <param name="id">идентификатор доктора</param>
    /// <returns><see cref="Doctor"/></returns>
    public async Task<Doctor?> Get(int id) => await context.Doctors.FindAsync(id);

    /// <summary>
    /// Удалить доктора по идентификатору
    /// </summary>
    /// <param name="id">идентификатор доктора</param>
    /// <returns>false при ошибке,true иначе</returns>
    public async Task<bool> Delete(int id)
    {
        var deletedDoctor = await Get(id);

        if (deletedDoctor == null) return false;

        context.Doctors.Remove(deletedDoctor);
        await context.SaveChangesAsync();   

        return true;
    }

    /// <summary>
    /// Добавить доктора
    /// </summary>
    /// <param name="newObj">объект класса доктора</param>
    public async Task Post(Doctor newObj) 
    {
        await context.Doctors.AddAsync(newObj);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Изменить доктора по идентификатору
    /// </summary>
    /// <param name="newObj">объект класса доктора</param>
    /// <param name="id">идентификатор доктора</param>
    /// <returns>false при ошибке,true иначе</returns>
    public async Task<bool> Put(Doctor newObj, int id)
    {
        var oldDoctor = await Get(id);

        if (oldDoctor == null) return false;

        context.Doctors.Update(newObj);
        await context.SaveChangesAsync();

        return true;
    }
}
