namespace Polyclinic.Domain.Repositories;
/// <summary>
/// Репозиторий посещений
/// </summary>
public class AppointmentRepository : IRepository<Appointment, int>
{
    private int _id = 0;
    private static readonly List<Appointment> _appointments = [];

    /// <summary>
    /// Вернуть все посещения
    /// </summary>
    /// <returns><see cref="Appointment"/></returns>
    public List<Appointment> GetAll() => _appointments;

    /// <summary>
    /// Вернуть посещение по идентификатору
    /// </summary>
    /// <param name="id">идентификатор посещений</param>
    /// <returns><see cref="Appointment"/></returns>
    public Appointment? Get(int id) => _appointments.Find(a => a.Id == id);

    /// <summary>
    /// Удалить почещение по идентификатору
    /// </summary>
    /// <param name="id">идентификатор посещений</param>
    /// <returns>false при ошибке,true иначе</returns>
    public bool Delete(int id)
    {
        var deletedAppounment = Get(id);

        if (deletedAppounment == null) return false;

        return _appointments.Remove(deletedAppounment);
    }

    /// <summary>
    /// Добавить посещение
    /// </summary>
    /// <param name="newObj">объект класса посещений</param>
    public void Post(Appointment newObj) 
    {
        newObj.Id = _id;
        _id++;
        _appointments.Add(newObj);
    }

    /// <summary>
    /// Изменить посещение по идентификатора
    /// </summary>
    /// <param name="newObj">объект класса посещений</param>
    /// <param name="id">идентификатор посещений</param>
    /// <returns>false при ошибке,true иначе</returns>
    public bool Put(Appointment newObj, int id)
    {
        var oldAppointment = Get(id);

        if (oldAppointment == null) return false;

        oldAppointment.Id = newObj.Id;
        oldAppointment.Patient = newObj.Patient;
        oldAppointment.Doctor = newObj.Doctor;
        oldAppointment.Date = newObj.Date;
        oldAppointment.Conclusion = newObj.Conclusion;

        return true;
    }
}
