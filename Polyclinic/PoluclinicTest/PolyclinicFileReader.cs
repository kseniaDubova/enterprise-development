using PolyclinicClasses;
using System.Globalization;

namespace PoluclinicTest
{
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
        public List<Doctor> ReadDoctor(string fileName)
        {
            var steamReader = new StreamReader(fileName);
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
                    Password = int.Parse(tokens[0]),
                    Name = tokens[1],
                    Birth = DateTime.ParseExact(tokens[2], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Experience = int.Parse(tokens[3]),
                    Specialization = (SpecializationTypes)Enum.Parse(typeof(SpecializationTypes), tokens[4]),
                };
                doctors.Add(doctor);

            }
            steamReader.Dispose();
            return doctors;
        }
        /// <summary>
        /// Чтение из файла списка пациентов
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public List<Patient> ReadPatient(string fileName)
        {
            var steamReader = new StreamReader(fileName);
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
                    Password = int.Parse(tokens[0]),
                    Name = tokens[1],
                    Birth = DateTime.ParseExact(tokens[2], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Adress = tokens[3],
                };
                patients.Add(patient);

            }
            steamReader.Dispose();
            return patients;
        }
        /// <summary>
        /// Поиск по паспорту в списке докторов
        /// </summary>
        /// <param name="password"></param>
        /// <param name="doctors"></param>
        /// <returns></returns>
        public Doctor findDoctor(int password, List<Doctor> doctors)
        { 
            return doctors.FirstOrDefault(d => d.Password == password); ;
        }
        /// <summary>
        /// Поиск по паспорту в списке пациентов
        /// </summary>
        /// <param name="password"></param>
        /// <param name="patients"></param>
        /// <returns></returns>
        public Patient findPatient(int password, List<Patient> patients)
        {
            return patients.FirstOrDefault(p => p.Password == password); ;
        }
        /// <summary>
        /// Чтение из файла списка посещений
        /// </summary>
        /// <param name="fileAppointment"></param>
        /// <param name="fileDoctor"></param>
        /// <param name="filePatient"></param>
        /// <returns></returns>
        public List<Appointment> ReadAppointment(string fileAppointment, string fileDoctor, string filePatient)
        {
            var steamReader = new StreamReader(fileAppointment);
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
                Doctor? doctor = findDoctor(doctorId, doctors);

                int patientId = int.Parse(tokens[1]);
                Patient? patient = findPatient(patientId, patients);

                if(doctor == null || patient == null)
                {
                    continue;
                }

                var appointment = new Appointment
                {
                    Id = int.Parse(tokens[0]),
                    PatientId = patient,
                    DoctorId = doctor,
                    Date = DateTime.ParseExact(tokens[3], "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                    Conclusion = (ConclusionTypes)Enum.Parse(typeof(ConclusionTypes), tokens[4]),
                };
                appointments.Add(appointment);

            }
            steamReader.Dispose();
            return appointments;
        }
    }
}
