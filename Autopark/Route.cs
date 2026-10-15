using System;

namespace Autopark
{
    /// <summary>
    /// Маршрут.
    /// </summary>
    public class Route
    {
        /// <summary>
        /// Код маршрута.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название маршрута.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Длина маршрута в км.
        /// </summary>
        public int Distance { get; set; }

        /// <summary>
        /// Длинный ли маршрут (больше 50 км).
        /// </summary>
        public bool IsLong
        {
            get
            {
                return Distance > 50;
            }
        }

        /// <summary>
        /// Конструктор с проверкой данных.
        /// </summary>
        public Route(int id, string name, int distance)
        {
            if (name == null)
            {
                throw new ArgumentNullException("Название маршрута не может быть null.");
            }

            if (name.Length == 0)
            {
                throw new ArgumentException("Название маршрута не может быть пустым.");
            }

            if (distance < 0)
            {
                throw new ArgumentException("Длина маршрута не может быть отрицательной.");
            }

            Id = id;
            Name = name;
            Distance = distance;
        }

        /// <summary>
        /// Информация о маршруте.
        /// </summary>
        public string GetInfo()
        {
            return Name + " (" + Distance + " км)";
        }
    }
}