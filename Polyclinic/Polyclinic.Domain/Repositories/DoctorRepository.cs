namespace Polyclinic.Domain.Repositories;
/// <summary>
/// Репозиторий докторов
/// </summary>
public class DoctorRepository : IRepository<Doctor, int>
{
    private int _id = 0;
    private static readonly List<Doctor> _doctors = [];

    /// <summary>
    /// Вернуть всех докторов
    /// </summary>
    /// <returns><see cref="Doctor"/></returns>
    public List<Doctor> GetAll() => _doctors;

    /// <summary>
    /// Вернуть доктора по идентификатору
    /// </summary>
    /// <param name="id">идентификатор доктора</param>
    /// <returns><see cref="Doctor"/></returns>
    public Doctor? Get(int id) => _doctors.Find(d => d.Id == id);

    /// <summary>
    /// Удалить доктора по идентификатору
    /// </summary>
    /// <param name="id">идентификатор доктора</param>
    /// <returns>false при ошибке,true иначе</returns>
    public bool Delete(int id)
    {
        var deletedDoctor = Get(id);

        if (deletedDoctor == null) return false;

        return _doctors.Remove(deletedDoctor);
    }

    /// <summary>
    /// Добавить доктора
    /// </summary>
    /// <param name="newObj">объект класса доктора</param>
    public void Post(Doctor newObj) 
    {
        newObj.Id = _id;
        _id++;
        _doctors.Add(newObj);
    }

    /// <summary>
    /// Изменить доктора по идентификатору
    /// </summary>
    /// <param name="newObj">объект класса доктора</param>
    /// <param name="id">идентификатор доктора</param>
    /// <returns>false при ошибке,true иначе</returns>
    public bool Put(Doctor newObj, int id)
    {
        var oldDoctor = Get(id);

        if (oldDoctor == null) return false;

        oldDoctor.Id = newObj.Id;
        oldDoctor.Passport = newObj.Passport;
        oldDoctor.Birth = newObj.Birth;
        oldDoctor.FullName = newObj.FullName;
        oldDoctor.Experience = newObj.Experience;
        oldDoctor.Specialization = newObj.Specialization;

        return true;
    }
}
