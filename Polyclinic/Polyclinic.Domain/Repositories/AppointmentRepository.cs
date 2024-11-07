using Microsoft.EntityFrameworkCore;

namespace Polyclinic.Domain.Repositories;
/// <summary>
/// Репозиторий посещений
/// </summary>
public class AppointmentRepository(PolyclinicDbContext context) : IRepository<Appointment, int>
{
    /// <summary>
    /// Вернуть все посещения
    /// </summary>
    /// <returns><see cref="Appointment"/></returns>
    public async Task<List<Appointment>> GetAll() => await context.Appointments.ToListAsync();

    /// <summary>
    /// Вернуть посещение по идентификатору
    /// </summary>
    /// <param name="id">идентификатор посещений</param>
    /// <returns><see cref="Appointment"/></returns>
    public async Task<Appointment?> Get(int id) => await context.Appointments.FindAsync(id);

    /// <summary>
    /// Удалить почещение по идентификатору
    /// </summary>
    /// <param name="id">идентификатор посещений</param>
    /// <returns>false при ошибке,true иначе</returns>
    public async Task<bool> Delete(int id)
    {
        var deletedAppounment = await Get(id);

        if (deletedAppounment == null) return false;

        context.Appointments.Remove(deletedAppounment);
        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Добавить посещение
    /// </summary>
    /// <param name="newObj">объект класса посещений</param>
    public async Task Post(Appointment newObj) 
    {
        await context.Appointments.AddAsync(newObj);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Изменить посещение по идентификатора
    /// </summary>
    /// <param name="newObj">объект класса посещений</param>
    /// <param name="id">идентификатор посещений</param>
    /// <returns>false при ошибке,true иначе</returns>
    public async Task<bool> Put(Appointment newObj, int id)
    {
        var oldAppointment = await Get(id);

        if (oldAppointment == null) return false;

        oldAppointment.Patient = newObj.Patient;
        oldAppointment.Conclusion = newObj.Conclusion;  
        oldAppointment.Date = newObj.Date;
        oldAppointment.Doctor = newObj.Doctor;

        context.Appointments.Update(oldAppointment);
        await context.SaveChangesAsync();

        return true;
    }
}
