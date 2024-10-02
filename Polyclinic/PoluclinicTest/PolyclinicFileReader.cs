using PolyclinicClasses;
using System.Globalization;

namespace PoluclinicTest;

/// <summary>
/// Чтение из файлов списков посещений, докторов, пациентов
/// </summary>
public class PolyclinicFileReader()
{
    /// <summary>
    /// Чтение из файла списка докторов
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static List<Doctor> ReadDoctor(string fileName)
    {
        using var steamReader = new StreamReader(fileName);
        var doctors = new List<Doctor>();

        while (!steamReader.EndOfStream)
        {
            var line = steamReader.ReadLine();
            if (line == null) continue;
            var tokens = line.Split(',');

            for (int i = 0; i < tokens.Length; i++)
            {
                tokens[i] = tokens[i].Trim('"');
            }

            var doctor = new Doctor
            {
                Id = int.Parse(tokens[0]),
                Passport = int.Parse(tokens[1]),
                FullName = tokens[2],
                Birth = DateTime.ParseExact(tokens[3], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Experience = float.Parse(tokens[4]),
                Specialization = (SpecializationTypes)Enum.Parse(typeof(SpecializationTypes), tokens[5]),
            };
            doctors.Add(doctor);

        }
        return doctors;
    }
    /// <summary>
    /// Чтение из файла списка пациентов
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static List<Patient> ReadPatient(string fileName)
    {
        using var steamReader = new StreamReader(fileName);
        var patients = new List<Patient>();

        while (!steamReader.EndOfStream)
        {
            var line = steamReader.ReadLine();
            if (line == null) continue;
            var tokens = line.Split(',');

            for (int i = 0; i < tokens.Length; i++)
            {
                tokens[i] = tokens[i].Trim('"');
            }

            var patient = new Patient
            {
                Id = int.Parse(tokens[0]),
                Passport = int.Parse(tokens[1]),
                FullName = tokens[2],
                Birth = DateTime.ParseExact(tokens[3], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Adress = tokens[4],
            };
            patients.Add(patient);

        }
        return patients;
    }
    /// <summary>
    /// Поиск по паспорту в списке докторов
    /// </summary>
    /// <param name="passport"></param>
    /// <param name="doctors"></param>
    /// <returns></returns>

    public static Doctor? FindDoctor(int passport, List<Doctor> doctors) => doctors.FirstOrDefault(d => d.Passport == passport);
    /// <summary>
    /// Поиск по паспорту в списке пациентов
    /// </summary>
    /// <param name="passport"></param>
    /// <param name="patients"></param>
    /// <returns></returns>
    public static Patient FindPatient(int passport, List<Patient> patients) => patients.FirstOrDefault(p => p.Passport == passport); 
    /// <summary>
    /// Чтение из файла списка посещений
    /// </summary>
    /// <param name="fileAppointment"></param>
    /// <param name="fileDoctor"></param>
    /// <param name="filePatient"></param>
    /// <returns></returns>
    public static List<Appointment> ReadAppointment(string fileAppointment, string fileDoctor, string filePatient)
    {
        using var steamReader = new StreamReader(fileAppointment);
        var appointments = new List<Appointment>();
        List<Patient> patients = ReadPatient(filePatient);
        List<Doctor> doctors = ReadDoctor(fileDoctor);

        while (!steamReader.EndOfStream)
        {
            var line = steamReader.ReadLine();
            if (line == null) continue;
            var tokens = line.Split(',');

            for (int i = 0; i < tokens.Length; i++)
            {
                tokens[i] = tokens[i].Trim('"');
            }

            int doctorId = int.Parse(tokens[2]);
            Doctor? doctor = FindDoctor(doctorId, doctors);

            int patientId = int.Parse(tokens[1]);
            Patient? patient = FindPatient(patientId, patients);

            if(doctor == null || patient == null)
            {
                continue;
            }

            var appointment = new Appointment
            {
                Id = int.Parse(tokens[0]),
                Patient = patient,
                Doctor = doctor,
                Date = DateTime.ParseExact(tokens[3], "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                Conclusion = (ConclusionTypes)Enum.Parse(typeof(ConclusionTypes), tokens[4]),
            };
            appointments.Add(appointment);

        }
        return appointments;
    }
}
