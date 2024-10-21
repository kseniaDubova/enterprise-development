namespace Polyclinic.Domain.Repositories;
/// <summary>
/// Репозиторий посещений
/// </summary>
public class AppointmentRepository : IRepository<Appointment, int>
{
    private static readonly List<Appointment> _appointments = [];

    /// <summary>
    /// Вернуть все посещения
    /// </summary>
    /// <returns></returns>
    public List<Appointment> GetAll() => _appointments;

    /// <summary>
    /// Вернуть посещение по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Appointment? Get(int id) => _appointments.Find(a => a.Id == id);

    /// <summary>
    /// Удалить почещение по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool Delete(int id)
    {
        var deletedAppounment = Get(id);

        if (deletedAppounment == null) return false;

        _appointments.Remove(deletedAppounment);
        return true;
    }

    /// <summary>
    /// Добавить посещение
    /// </summary>
    /// <param name="newObj"></param>
    public void Post(Appointment newObj) 
    {
        newObj.Id = _appointments.Count;
        _appointments.Add(newObj);
    } 

    /// <summary>
    /// Изменить посещение по идентификатора
    /// </summary>
    /// <param name="newObj"></param>
    /// <param name="id"></param>
    /// <returns></returns>
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
