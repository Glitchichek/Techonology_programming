using System;

namespace Autopark
{
    /// <summary>
    /// Машина.
    /// </summary>
    public class Car
    {
        /// <summary>
        /// Код машины.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Модель машины.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Код маршрута.
        /// </summary>
        public int RouteId { get; set; }

        /// <summary>
        /// Год выпуска.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Номер машины.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Новая ли машина (год от 2020).
        /// </summary>
        public bool IsNew
        {
            get { return Year >= 2020; }
        }

        /// <summary>
        /// Конструктор с проверкой данных.
        /// </summary>
        public Car(int id, string model, int routeId, int year, string number)
        {
            if (model == null)
            {
                throw new ArgumentNullException("Модель не может быть null.");
            }

            if (number == null)
            {
                throw new ArgumentNullException("Номер не может быть null.");
            }

            if (model.Length == 0)
            {
                throw new ArgumentException("Модель не может быть пустой.");
            }

            if (number.Length == 0)
            {
                throw new ArgumentException("Номер не может быть пустым.");
            }

            if (year < 1900 || year > 2100)
            {
                throw new ArgumentException("Год должен быть от 1900 до 2100.");
            }

            Id = id;
            Model = model;
            RouteId = routeId;
            Year = year;
            Number = number;
        }

        /// <summary>
        /// Информация о машине.
        /// </summary>
        public string GetInfo()
        {
            return Model + " (" + Year + ", " + Number + ")";
        }
    }
}