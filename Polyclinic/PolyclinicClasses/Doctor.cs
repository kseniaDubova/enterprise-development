namespace PolyclinicClasses;

/// <summary>
/// Доктор
/// </summary>
public class Doctor
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Паспорт
    /// </summary>
    public required string Passport { get; set; }
    /// <summary>
    /// ФИО
    /// </summary>
    public required string FullName { get; set; }
    /// <summary>
    /// День рождения
    /// </summary>
    public required DateTime Birth { get; set; }
    /// <summary>
    /// Опыт работя
    /// </summary>
    public required double Experience { get; set; }
    /// <summary>
    /// Специализация
    /// </summary>
    public required SpecializationTypes Specialization { get; set; }
}
