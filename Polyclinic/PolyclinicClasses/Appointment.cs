﻿namespace PolyclinicClasses
{
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
        /// Индификатор пациента
        /// </summary>
        public required Patient PatientId { get; set; }
        /// <summary>
        /// Индификатор доктора
        /// </summary>
        public required Doctor DoctorId { get; set; }
        /// <summary>
        /// Дата и время посещения
        /// </summary>
        public required DateTime Date { get; set; }
        /// <summary>
        /// Заключение
        /// </summary>
        public required ConclusionTypes Conclusion { get; set; }
    }
}