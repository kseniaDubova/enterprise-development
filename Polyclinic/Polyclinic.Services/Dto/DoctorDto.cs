using Polyclinic.Domain;
using System.ComponentModel.DataAnnotations;

namespace Polyclinic.Services.Dto;

public class DoctorDto
{
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
    [EnumDataType(typeof(SpecializationTypes))]
    public required string Specialization { get; set; }
}
