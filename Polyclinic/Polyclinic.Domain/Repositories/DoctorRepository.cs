namespace Polyclinic.Domain.Repositories;
/// <summary>
/// Репозиторий докторов
/// </summary>
public class DoctorRepository : IRepository<Doctor, int>
{
    private static readonly List<Doctor> _doctors = [];//PolyclinicFileReader.FindDoctor(Path.Combine("data", "doctors.csv"));

    /// <summary>
    /// Вернуть всех докторов
    /// </summary>
    /// <returns></returns>
    public List<Doctor> GetAll() => _doctors;

    /// <summary>
    /// Вернуть доктора по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Doctor? Get(int id) => _doctors.Find(d => d.Id == id);

    /// <summary>
    /// Удалить доктора по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool Delete(int id)
    {
        var deletedDoctor = Get(id);

        if (deletedDoctor == null) return false;

        _doctors.Remove(deletedDoctor);
        return true;
    }

    /// <summary>
    /// Добавить доктора
    /// </summary>
    /// <param name="newObj"></param>
    public void Post(Doctor newObj) 
    {
        newObj.Id = _doctors.Count;
        _doctors.Add(newObj);
    } 

    /// <summary>
    /// Изменить доктора по идентификатору
    /// </summary>
    /// <param name="newObj"></param>
    /// <param name="id"></param>
    /// <returns></returns>
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
