using Polyclinic.Domain;
namespace Polyclinic.Services.Dto;

public class AppointmentDto
{
    /// <summary>
    /// Идентификатор пациента
    /// </summary>
    public required int IdPatient { get; set; }
    /// <summary>
    /// Идентификатор доктора
    /// </summary>
    public required int IdDoctor { get; set; }
    /// <summary>
    /// Дата и время посещения
    /// </summary>
    public required DateTime Date { get; set; }
    /// <summary>
    /// Заключение
    /// </summary>
    public string? Conclusion { get; set; }
}
