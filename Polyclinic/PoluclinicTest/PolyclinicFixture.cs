using PolyclinicClasses;

namespace PoluclinicTest;

/// <summary>
/// Заполнение тестовыми данными списков посещений, докторов, пациентов
/// </summary>
public class PolyclinicFixture
{
    /// <summary>
    /// Объявление списков данных
    /// </summary>
    public List<Doctor>? Doctors { get; set; }
    public List<Patient>? Patients { get; set; }
    public List<Appointment>? Appointments { get; set; }
    /// <summary>
    /// Конструктор чтения файлов
    /// </summary>
    public PolyclinicFixture()
    {
        Doctors = PolyclinicFileReader.ReadDoctor(Path.Combine("data", "doctors.csv"));
        Patients = PolyclinicFileReader.ReadPatient(Path.Combine("data", "patients.csv"));
        Appointments = PolyclinicFileReader.ReadAppointment(
            Path.Combine("data", "appointments.csv"),
            Path.Combine("data", "doctors.csv"),
            Path.Combine("data", "patients.csv"));
    }
}
