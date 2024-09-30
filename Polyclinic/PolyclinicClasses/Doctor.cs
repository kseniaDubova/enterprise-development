namespace PolyclinicClasses
{
    /// <summary>
    /// Доктор
    /// </summary>
    public class Doctor
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
        /// Опыт работя
        /// </summary>
        public required int Experience { get; set; }
        /// <summary>
        /// Специализация
        /// </summary>
        public required SpecializationTypes Specialization { get; set; }
        /// <summary>
        /// Сравнение докторов
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj is Doctor other)
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
