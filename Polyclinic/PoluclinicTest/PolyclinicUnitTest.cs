using PolyclinicClasses;

namespace PoluclinicTest;

/// <summary>
/// Тестирование с использованием данных из подготовленных файлов
/// </summary>
public class PolyclinicUnitTest(PolyclinicFixture reader) : IClassFixture<PolyclinicFixture>
{
    private readonly PolyclinicFixture _reader = reader;
    /// <summary>
    /// Проверка вывода всех докторов, опыт которых больше 10 лет
    /// </summary>
    [Fact]
    public void TestExperienceOfDoctors()
    {
        var doctors = _reader.Doctors
            .Where(d => d.Experience > 10).ToList();

        Assert.Equal(4, doctors.Count);
    }

    /// <summary>
    /// Проверка вывода всех пациентов указанного доктора, сортировка по имени
    /// </summary>
    [Fact]
    public void TestPatientsOfDoctor()
    {
        var doctorId = 3;

        var patient = _reader.Appointments
            .Where(a => a.Doctor.Id == 3)
            .Select(p => p.Patient)
            .Distinct()
            .OrderBy(p => p.FullName)
            .ToList();

        Assert.Equal(4, patient.Count);
        Assert.Equal("Akiro", patient[0].FullName);
        Assert.Equal("Walmart", patient[3].FullName);
    }

    /// <summary>
    /// Проверка вывода здоровых на данный момент пациентов
    /// </summary>
    [Fact]
    public void TestHealthyPatients()
    {
        var patient = _reader.Appointments
            .Where(a => a.Conclusion == ConclusionTypes.Healthy)
            .Select(p => p.Patient)
            .Distinct()
            .ToList();

        Assert.Equal(6, patient.Count);
    }

    /// <summary>
    /// Проверка вывода количества приемов пациентов по врачам за месяц
    /// </summary>
    [Fact]
    public void TestCountAppointmentsForDoctors()
    {
        var month = new DateTime(2024, 9, 1);

        var appointment = _reader.Appointments
            .Where(a => a.Date >= month)
            .GroupBy(p => p.Doctor.Passport)
            .Select(g => new
            {
                Doctor = g.Key,
                Count = g.Count(),
            })
            .OrderBy(g => g.Doctor)
            .ToList();

        Assert.Equal(1, appointment[0].Count);
        Assert.Equal(3, appointment[1].Count);
        Assert.Equal(1, appointment[2].Count);
        Assert.Equal(1, appointment[3].Count);
        Assert.Equal(2, appointment[4].Count);
    }

    /// <summary>
    /// Проверка вывода топа заболеваний
    /// </summary>
    [Fact]
    public void TestTopOfSpecialization()
    {
        var spetialization = _reader.Appointments
            .GroupBy(p => p.Doctor.Specialization)
            .Select(g => new
            {
                Specialization = g.Key,
                Count = g.Count(),
            })
            .OrderByDescending(g => g.Count)
            .Take(5)
            .ToList();

        Assert.Equal(8, spetialization[0].Count);
        Assert.Equal(4, spetialization[1].Count);
        Assert.Equal(3, spetialization[2].Count);
        Assert.Equal(2, spetialization[3].Count);
    }

    /// <summary>
    /// Проверка вывода пациентов, записаных к нескольким врачам, сортировка по дате рождения
    /// </summary>
    [Fact]
    public void TestPatientsWithSeveralAppointment()
    {
        var today = new DateTime(2024, 9, 1);

        var patient = _reader.Appointments
            .Where(a => (today.Year - a.Patient.Birth.Year) > 30)
            .GroupBy(p => p.Patient)
            .Where(g => g.Select(a => a.Doctor.Passport).Distinct().Count() > 1)
            .Select(g => g.Key)
            .OrderBy(p => p.Birth)
            .ToList();

        Assert.Equal(2, patient.Count);
    }
}