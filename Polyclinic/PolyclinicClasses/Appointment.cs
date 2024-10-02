namespace PolyclinicClasses;

/// <summary>
/// Посещение
/// </summary>
public class Appointment
{
    /// <summary>
    /// Идентификатор посещения
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Идентификатор пациента
    /// </summary>
    public required Patient Patient { get; set; }
    /// <summary>
    /// Идентификатор доктора
    /// </summary>
    public required Doctor Doctor { get; set; }
    /// <summary>
    /// Дата и время посещения
    /// </summary>
    public required DateTime Date { get; set; }
    /// <summary>
    /// Заключение
    /// </summary>
    public ConclusionTypes? Conclusion { get; set; }
}
