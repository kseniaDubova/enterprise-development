namespace Polyclinic.Domain;

/// <summary>
/// Пациент
/// </summary>
public class Patient
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
    /// Адресс
    /// </summary>
    public required string Adress { get; set; }
}
