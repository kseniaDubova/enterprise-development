using PolyclinicClasses;

namespace PoluclinicTest
{
    /// <summary>
    /// Заполнение тестовыми данными списков посещений, докторов, пациентов
    /// </summary>
    public class PolyclinicFixture
    {
        /// <summary>
        /// Объявление списков данных
        /// </summary>
        private List<Doctor>? _doctors;
        private List<Patient>? _patients;
        private List<Appointment>? _appointments;

        /// <summary>
        /// Заполнение списка докторов
        /// </summary>
        /// <returns></returns>
        public List<Doctor> GetDoctors()
        {
            if (_doctors != null) return _doctors;

            var doctorsReader = new PolyclinicFileReader();
            _doctors = doctorsReader.ReadDoctor("doctors.csv");
            return _doctors;
        }

        /// <summary>
        /// Заполнение списка пациентов
        /// </summary>
        /// <returns></returns>
        public List<Patient> GetPatient()
        {
            if (_patients != null) return _patients;

            var patientsReader = new PolyclinicFileReader();
            _patients = patientsReader.ReadPatient("csv/patients.csv");
            return _patients;
        }

        /// <summary>
        /// Заполнение списка посещений
        /// </summary>
        /// <returns></returns>
        public List<Appointment> GetAppointment()
        {
            if (_appointments != null) return _appointments;

            var appointmentsReader = new PolyclinicFileReader();
            _appointments = appointmentsReader.ReadAppointment("csv/appointments.csv", "csv/doctors.csv", "csv/patients.csv");
            return _appointments;
        }
    }
}
