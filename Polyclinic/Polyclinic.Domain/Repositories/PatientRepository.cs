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
    /// <returns></returns>
    public List<Patient> GetAll () => _patients;

    /// <summary>
    /// Вернуть пациента по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Patient? Get(int id) => _patients.Find(p => p.Id == id);

    /// <summary>
    /// Удалить пациента по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool Delete(int id)
    {
        var deletedPatient = Get(id);

        if (deletedPatient == null) return false; 

        _patients.Remove(deletedPatient);  
        return true;
    }

    /// <summary>
    /// Добавить пациента
    /// </summary>
    /// <param name="newObj"></param>
    public void Post(Patient newObj) 
    {
        newObj.Id = _patients.Count;
        _patients.Add(newObj);
    }

    /// <summary>
    /// Изменить пациента по идентификатору
    /// </summary>
    /// <param name="newObj"></param>
    /// <param name="id"></param>
    /// <returns></returns>
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
