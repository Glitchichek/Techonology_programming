using System;
using System.Collections.Generic;
using System.IO;

namespace Autopark
{
    /// <summary>
    /// Репозиторий из CSV.
    /// </summary>
    public class CsvRepository
    {
        private string _basePath;

        /// <summary>
        /// Конструктор с проверкой пути.
        /// </summary>
        public CsvRepository(string basePath)
        {
            if (basePath == null)
            {
                throw new ArgumentNullException("Путь не может быть null.");
            }

            if (basePath.Length == 0)
            {
                throw new ArgumentException("Путь не может быть пустым.");
            }

            _basePath = basePath;
        }

        /// <summary>
        /// Читает маршруты из routes.csv.
        /// </summary>
        public List<Route> GetRoutes()
        {
            if (_basePath == null)
            {
                throw new ArgumentNullException("Путь не задан.");
            }

            List<Route> result = new List<Route>();
            string path = Path.Combine(_basePath, "routes.csv");
            if (!File.Exists(path)) return result;

            string[] lines = File.ReadAllLines(path);
            if (lines == null || lines.Length < 2) return result;

            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i] == null) continue;

                string[] parts = lines[i].Split(',');
                if (parts.Length < 3) continue;

                int id = int.Parse(parts[0]);
                string name = parts[1];
                int distance = int.Parse(parts[2]);

                Route route = new Route(id, name, distance);
                result.Add(route);
            }
            return result;
        }

        /// <summary>
        /// Читает машины из cars.csv.
        /// </summary>
        public List<Car> GetCars()
        {
            if (_basePath == null)
            {
                throw new ArgumentNullException("Путь не задан.");
            }

            List<Car> result = new List<Car>();
            string path = Path.Combine(_basePath, "cars.csv");
            if (!File.Exists(path)) return result;

            string[] lines = File.ReadAllLines(path);
            if (lines == null || lines.Length < 2) return result;

            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i] == null) continue;

                string[] parts = lines[i].Split(',');
                if (parts.Length < 5) continue;

                int id = int.Parse(parts[0]);
                string model = parts[1];
                int routeId = int.Parse(parts[2]);
                int year = int.Parse(parts[3]);
                string number = parts[4];

                Car car = new Car(id, model, routeId, year, number);
                result.Add(car);
            }
            return result;
        }

        /// <summary>
        /// Читает водителей из drivers.csv.
        /// </summary>
        public List<Driver> GetDrivers()
        {
            if (_basePath == null)
            {
                throw new ArgumentNullException("Путь не задан.");
            }

            List<Driver> result = new List<Driver>();
            string path = Path.Combine(_basePath, "drivers.csv");
            if (!File.Exists(path)) return result;

            string[] lines = File.ReadAllLines(path);
            if (lines == null || lines.Length < 2) return result;

            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i] == null) continue;

                string[] parts = lines[i].Split(',');
                if (parts.Length < 5) continue;

                int id = int.Parse(parts[0]);
                string fullName = parts[1];
                int carId = int.Parse(parts[2]);
                int experience = int.Parse(parts[3]);
                string license = parts[4];

                Driver driver = new Driver(id, fullName, carId, experience, license);
                result.Add(driver);
            }
            return result;
        }
    }
}