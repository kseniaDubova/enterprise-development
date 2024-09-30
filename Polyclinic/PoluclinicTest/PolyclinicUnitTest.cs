using PolyclinicClasses;

namespace PoluclinicTest
{
    /// <summary>
    /// “естирование с использованием данных из подготовленных файлов
    /// </summary>
    public class PolyclinicUnitTest(PolyclinicFixture reader): IClassFixture<PolyclinicFixture>
    {
        private PolyclinicFixture _reader = reader;
        /// <summary>
        /// ѕроверка вывода всех докторов, опыт которых больше 10 лет
        /// </summary>
        [Fact]
        public void TestExperienceOfDoctors()
        {
            var tmp = _reader.GetDoctors()
                .Where(d => d.Experience > 10).ToList();
            Assert.Equal(4, tmp.Count);

        }

        /// <summary>
        /// ѕроверка вывода всех пациентов указанного доктора, сортировка по имени
        /// </summary>
        [Fact]
        public void TestPatientsOfDoctor()
        {
            var tmp = _reader.GetAppointment()
                .Where(a => a.DoctorId.Password == 2930)
                .Select(p => p.PatientId)
                .Distinct()
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Password)
                .ToList();
            Assert.Equal(4, tmp.Count);
            Assert.Equal("Akiro", tmp[0].Name);
            Assert.Equal("Walmart", tmp[3].Name);
        }

        /// <summary>
        /// ѕроверка вывода здоровых на данный момент пациентов
        /// </summary>
        [Fact]
        public void TestHealthyPatients()
        {
            var tmp = _reader.GetAppointment()
                .GroupBy(p => p.PatientId)
                .Select(g => g.OrderByDescending(a => a.Date).First())
                .Where(a => a.Conclusion == ConclusionTypes.Healthy)
                .ToList();
            Assert.Equal(6, tmp.Count);
        }

        /// <summary>
        /// ѕроверка вывода количества приемов пациентов по врачам за мес€ц
        /// </summary>
        [Fact]
        public void TestCountAppointmentsForDoctors()
        {
            var month = DateTime.Now.AddMonths(-1);
            var tmp = _reader.GetAppointment()
                .Where(a => a.Date >= month)
                .GroupBy(p => p.DoctorId.Password)
                .Select(g => new
                {
                    Doctor = g.Key,
                    Count = g.Count(),
                })
                .OrderBy(g => g.Doctor)
                .ToList();
            Assert.Equal(1, tmp[0].Count);
            Assert.Equal(3, tmp[1].Count);
            Assert.Equal(1, tmp[2].Count);
            Assert.Equal(1, tmp[3].Count);
            Assert.Equal(2, tmp[4].Count);

        }

        /// <summary>
        /// ѕроверка вывода топа заболеваний
        /// </summary>
        [Fact]
        public void TestTopOfSpecialization()
        {
            var tmp = _reader.GetAppointment()
                .GroupBy(p => p.DoctorId.Specialization)
                .Select(g => new
                {
                    Specialization = g.Key,
                    Count = g.Count(),
                })
                .OrderByDescending(g => g.Count)
                .ToList();
            Assert.True(tmp[0].Count > tmp[3].Count);
        }

        /// <summary>
        /// ѕроверка вывода пациентов, записаных к нескольким врачам, сортировка по имени
        /// </summary>
        [Fact]
        public void TestPatientsWithSeveralAppointment() 
        { 
            var today = DateTime.Today;
            var tmp = _reader.GetAppointment()
                .Where(a => (today.Year - a.PatientId.Birth.Year) > 30)
                .GroupBy(p => p.PatientId)
                .Where(g => g.Select(a => a.DoctorId.Password).Distinct().Count() > 1)
                .Select(g => g.Key)
                .OrderBy(p => p.Birth)
                .ToList();
            Assert.Equal(2, tmp.Count);
        }
    }
}