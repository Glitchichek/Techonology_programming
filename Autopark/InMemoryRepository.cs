using System;
using System.Collections.Generic;

namespace Autopark
{
    /// <summary>
    /// Репозиторий в памяти.
    /// </summary>
    public class InMemoryRepository
    {
        private List<Route> _routes;
        private List<Car> _cars;
        private List<Driver> _drivers;

        /// <summary>
        /// Заполняет репозиторий данными.
        /// </summary>
        public InMemoryRepository()
        {
            _routes = new List<Route>()
            {
                new Route(1, "Городской",   25),
                new Route(2, "Загородный",  75),
                new Route(3, "Пригородный", 40),
                new Route(4, "Экспресс",    120),
                new Route(5, "Кольцевой",   60)
            };

            _cars = new List<Car>()
            {
                new Car(1, "Toyota Camry",    1, 2021, "А123БВ"),
                new Car(2, "Kia Rio",         2, 2019, "Б456ВГ"),
                new Car(3, "Ford Focus",      1, 2022, "В789ГД"),
                new Car(4, "Hyundai Solaris", 3, 2018, "Г321ДЕ"),
                new Car(5, "Toyota Camry",    4, 2023, "Д654ЕЖ")
            };

            _drivers = new List<Driver>()
            {
                new Driver(1, "Иванов И.И.",  1, 10, "A"),
                new Driver(2, "Петров П.П.",  2, 3,  "B"),
                new Driver(3, "Сидоров С.С.", 3, 8,  "A"),
                new Driver(4, "Иванов И.И.",  4, 10, "A"),
                new Driver(5, "Орлов А.А.",   5, 6,  "C")
            };
        }

        /// <summary>
        /// Возвращает маршруты.
        /// </summary>
        public List<Route> GetRoutes()
        {
            if (_routes == null)
            {
                throw new InvalidOperationException("Список маршрутов не инициализирован.");
            }
            return _routes;
        }

        /// <summary>
        /// Возвращает машины.
        /// </summary>
        public List<Car> GetCars()
        {
            if (_cars == null)
            {
                throw new InvalidOperationException("Список машин не инициализирован.");
            }
            return _cars;
        }

        /// <summary>
        /// Возвращает водителей.
        /// </summary>
        public List<Driver> GetDrivers()
        {
            if (_drivers == null)
            {
                throw new InvalidOperationException("Список водителей не инициализирован.");
            }
            return _drivers;
        }
    }
}