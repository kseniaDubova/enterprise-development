namespace PolyclinicClasses
{
    /// <summary>
    /// Пациент
    /// </summary>
    public class Patient
    {
        /// <summary>
        /// Паспорт
        /// </summary>
        public required int Password { get; set; }
        /// <summary>
        /// ФИО
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// День рождения
        /// </summary>
        public required DateTime Birth { get; set; }
        /// <summary>
        /// Адресс
        /// </summary>
        public required string Adress { get; set; }
    }
}
