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
        /// <summary>
        /// Сравнение пациентов
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj is Patient other)
            {
                return this.Password == other.Password;
            }
            return false;
        }
        /// <summary>
        /// Поля для сравнений
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.Password.GetHashCode();
        }
    }
}
