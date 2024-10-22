namespace Polyclinic.Domain.Repositories;
/// <summary>
/// Репозиторий пациентов
/// </summary>
public class PatientRepository : IRepository<Patient, int>
{
    private static readonly List<Patient> _patients = [];

    /// <summary>
    /// Вернуть всех пациентов
    /// </summary>
    /// <returns><see cref="Patient"/></returns>
    public List<Patient> GetAll () => _patients;

    /// <summary>
    /// Вернуть пациента по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns><see cref="Patient"/></returns>
    public Patient? Get(int id) => _patients.Find(p => p.Id == id);

    /// <summary>
    /// Удалить пациента по идентификатору
    /// </summary>
    /// <param name="id">идентификатор пациента</param>
    /// <returns>false при ошибке,true иначе </returns>
    public bool Delete(int id)
    {
        var deletedPatient = Get(id);

        if (deletedPatient == null) return false; 

        return _patients.Remove(deletedPatient);
    }

    /// <summary>
    /// Добавить пациента
    /// </summary>
    /// <param name="newObj">объект класса пациента</param>
    public void Post(Patient newObj) 
    {
        newObj.Id = _patients.Count;
        _patients.Add(newObj);
    }

    /// <summary>
    /// Изменить пациента по идентификатору
    /// </summary>
    /// <param name="newObj">объект класса пациента</param>
    /// <param name="id">идентификатор пациента</param>
    /// <returns>false при ошибке,true иначе</returns>
    public bool Put(Patient newObj, int id)
    {
        var oldPatient = Get(id);

        if (oldPatient == null) return false;

        oldPatient.Id = newObj.Id;
        oldPatient.Passport = newObj.Passport;
        oldPatient.Adress = newObj.Adress;
        oldPatient.Birth = newObj.Birth;
        oldPatient.FullName = newObj.FullName;

        return true;
    }
}
