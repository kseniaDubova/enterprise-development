using Polyclinic.Domain;

namespace PoluclinicTest;

/// <summary>
/// Тестирование с использованием данных из подготовленных файлов
/// </summary>
public class PolyclinicUnitTest(PolyclinicFixture fixture) : IClassFixture<PolyclinicFixture>
{
    private readonly PolyclinicFixture _fixture = fixture;
    /// <summary>
    /// Проверка вывода всех докторов, опыт которых больше 10 лет
    /// </summary>
    [Fact]
    public void TestExperienceOfDoctors()
    {
        var doctors = _fixture.Doctors
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

        var patients = _fixture.Appointments
            .Where(a => a.Doctor.Id == doctorId)
            .Select(p => p.Patient)
            .Distinct()
            .OrderBy(p => p.FullName)
            .ToList();

        Assert.Equal(4, patients.Count);
        Assert.Equal("Akiro", patients[0].FullName);
        Assert.Equal("Walmart", patients[3].FullName);
    }

    /// <summary>
    /// Проверка вывода здоровых на данный момент пациентов
    /// </summary>
    [Fact]
    public void TestHealthyPatients()
    {
        var patients = _fixture.Appointments
            .Where(a => a.Conclusion == ConclusionTypes.Healthy)
            .Select(p => p.Patient)
            .Distinct()
            .ToList();

        Assert.Equal(6, patients.Count);
    }

    /// <summary>
    /// Проверка вывода количества приемов пациентов по врачам за месяц
    /// </summary>
    [Fact]
    public void TestCountAppointmentsForDoctors()
    {
        var month = new DateTime(2024, 9, 1);

        var appointments = _fixture.Appointments
            .Where(a => a.Date >= month)
            .GroupBy(p => p.Doctor.Passport)
            .Select(g => new
            {
                Doctor = g.Key,
                Count = g.Count(),
            })
            .OrderBy(g => g.Doctor)
            .ToList();

        Assert.Equal(1, appointments[0].Count);
        Assert.Equal(3, appointments[1].Count);
        Assert.Equal(1, appointments[2].Count);
        Assert.Equal(1, appointments[3].Count);
        Assert.Equal(2, appointments[4].Count);
    }

    /// <summary>
    /// Проверка вывода топа заболеваний
    /// </summary>
    [Fact]
    public void TestTopOfSpecialization()
    {
        var spetializations = _fixture.Appointments
            .GroupBy(p => p.Doctor.Specialization)
            .Select(g => new
            {
                Specialization = g.Key,
                Count = g.Count(),
            })
            .OrderByDescending(g => g.Count)
            .Take(5)
            .ToList();

        Assert.Equal(8, spetializations[0].Count);
        Assert.Equal(4, spetializations[1].Count);
        Assert.Equal(3, spetializations[2].Count);
        Assert.Equal(2, spetializations[3].Count);
    }

    /// <summary>
    /// Проверка вывода пациентов, записаных к нескольким врачам, сортировка по дате рождения
    /// </summary>
    [Fact]
    public void TestPatientsWithSeveralAppointment()
    {
        var today = new DateTime(2024, 9, 1);

        var patients = _fixture.Appointments
            .Where(a => (today.Year - a.Patient.Birth.Year) > 30)
            .GroupBy(p => p.Patient)
            .Where(g => g.Select(a => a.Doctor.Passport).Distinct().Count() > 1)
            .Select(g => g.Key)
            .OrderBy(p => p.Birth)
            .ToList();

        Assert.Equal(2, patients.Count);
    }
}