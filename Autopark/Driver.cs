using System;

namespace Autopark
{
    /// <summary>
    /// Водитель.
    /// </summary>
    public class Driver
    {
        /// <summary>
        /// Код водителя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ФИО водителя.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Код машины.
        /// </summary>
        public int CarId { get; set; }

        /// <summary>
        /// Стаж в годах.
        /// </summary>
        public int Experience { get; set; }

        /// <summary>
        /// Номер прав.
        /// </summary>
        public string License { get; set; }

        /// <summary>
        /// Опытный ли водитель (стаж больше 5).
        /// </summary>
        public bool IsExperienced
        {
            get
            {
                return Experience > 5;
            }
        }

        /// <summary>
        /// Конструктор с проверкой данных.
        /// </summary>
        public Driver(int id, string fullName, int carId, int experience, string license)
        {
            if (fullName == null)
            {
                throw new ArgumentNullException("ФИО не может быть null.");
            }

            if (license == null)
            {
                throw new ArgumentNullException("Номер прав не может быть null.");
            }

            if (fullName.Length == 0)
            {
                throw new ArgumentException("ФИО не может быть пустым.");
            }

            if (license.Length == 0)
            {
                throw new ArgumentException("Номер прав не может быть пустым.");
            }

            if (experience < 0)
            {
                throw new ArgumentException("Стаж не может быть отрицательным.");
            }

            Id = id;
            FullName = fullName;
            CarId = carId;
            Experience = experience;
            License = license;
        }

        /// <summary>
        /// Информация о водителе.
        /// </summary>
        public string GetInfo()
        {
            return FullName + " (" + Experience + " лет стажа)";
        }
    }
}