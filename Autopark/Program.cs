using System;
using System.Collections.Generic;

namespace Autopark
{
    class Program
    {
        static void Main()
        {
            List<Route>? routes = null;
            List<Car>? cars = null;
            List<Driver>? drivers = null;

            try
            {
                Console.WriteLine("Выберите источник данных:");
                Console.WriteLine("1 - InMemoryRepository");
                Console.WriteLine("2 - CsvRepository");
                Console.Write("Ваш выбор: ");

                string? input = Console.ReadLine();
                if (input == null)
                {
                    Console.WriteLine("Ввод отсутствует.");
                    return;
                }

                int choice = int.Parse(input);

                switch (choice)
                {
                    case 1:
                        InMemoryRepository mem = new InMemoryRepository();
                        routes = mem.GetRoutes();
                        cars = mem.GetCars();
                        drivers = mem.GetDrivers();
                        break;

                    case 2:
                        CsvRepository csv = new CsvRepository("data");
                        routes = csv.GetRoutes();
                        cars = csv.GetCars();
                        drivers = csv.GetDrivers();
                        break;

                    default:
                        Console.WriteLine("Неверный выбор");
                        return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
                return;
            }

            if (routes == null || cars == null || drivers == null)
            {
                Console.WriteLine("Данные не загружены.");
                return;
            }

            Console.WriteLine();

            Driver? d1 = FindDriver(cars, drivers, "А123БВ");
            Console.WriteLine("1. FindDriver(\"А123БВ\"): " + (d1 != null ? d1.GetInfo() : "null"));

            Route? r1 = FindRoute(cars, routes, "Toyota Camry");
            Console.WriteLine("2. FindRoute(car \"Toyota Camry\"): " + (r1 != null ? r1.GetInfo() : "null"));

            Console.WriteLine("3. GetTotalDistance: " + GetTotalDistance(routes) + " км");

            Dictionary<string, int> multi = GetDriversWithMultipleCars(drivers);
            Console.Write("4. GetDriversWithMultipleCars: ");
            bool first = true;
            foreach (KeyValuePair<string, int> pair in multi)
            {
                if (!first) Console.Write(", ");
                Console.Write(pair.Key + " (" + pair.Value + ")");
                first = false;
            }
            Console.WriteLine();

            Console.WriteLine("5. PrintAllCars:");
            PrintAllCars(cars, routes, drivers);

            Driver? notFound = FindDriver(cars, drivers, "Х000ХХ");
            Console.WriteLine("\nНе найдено: FindDriver(\"Х000ХХ\") -> " + (notFound != null ? notFound.GetInfo() : "null"));

            Console.WriteLine("\nГотово. Нажмите Enter.");
            Console.ReadLine();
        }

        /// <summary>
        /// Ищет водителя по номеру машины.
        /// </summary>
        static Driver? FindDriver(List<Car> cars, List<Driver> drivers, string number)
        {
            if (cars == null)
            {
                throw new ArgumentNullException("Список машин не может быть null.");
            }

            if (drivers == null)
            {
                throw new ArgumentNullException("Список водителей не может быть null.");
            }

            if (number == null)
            {
                throw new ArgumentNullException("Номер не может быть null.");
            }

            Car? foundCar = null;
            foreach (Car c in cars)
            {
                if (c.Number == number)
                {
                    foundCar = c;
                    break;
                }
            }
            if (foundCar == null) return null;

            foreach (Driver d in drivers)
            {
                if (d.CarId == foundCar.Id)
                {
                    return d;
                }
            }
            return null;
        }

        /// <summary>
        /// Ищет маршрут машины по модели.
        /// </summary>
        static Route? FindRoute(List<Car> cars, List<Route> routes, string model)
        {
            if (cars == null)
            {
                throw new ArgumentNullException("Список машин не может быть null.");
            }

            if (routes == null)
            {
                throw new ArgumentNullException("Список маршрутов не может быть null.");
            }

            if (model == null)
            {
                throw new ArgumentNullException("Модель не может быть null.");
            }

            Car? foundCar = null;
            foreach (Car c in cars)
            {
                if (c.Model == model)
                {
                    foundCar = c;
                    break;
                }
            }
            if (foundCar == null) return null;

            foreach (Route r in routes)
            {
                if (r.Id == foundCar.RouteId)
                {
                    return r;
                }
            }
            return null;
        }

        /// <summary>
        /// Считает общую длину маршрутов.
        /// </summary>
        static int GetTotalDistance(List<Route> routes)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("Список маршрутов не может быть null.");
            }

            int total = 0;
            foreach (Route r in routes)
            {
                total += r.Distance;
            }
            return total;
        }

        /// <summary>
        /// Ищет водителей с несколькими машинами.
        /// </summary>
        static Dictionary<string, int> GetDriversWithMultipleCars(List<Driver> drivers)
        {
            if (drivers == null)
            {
                throw new ArgumentNullException("Список водителей не может быть null.");
            }

            Dictionary<string, List<int>> map = new Dictionary<string, List<int>>();

            foreach (Driver d in drivers)
            {
                if (!map.ContainsKey(d.FullName))
                {
                    map[d.FullName] = new List<int>();
                }

                bool exists = false;
                foreach (int id in map[d.FullName])
                {
                    if (id == d.CarId) { exists = true; break; }
                }
                if (!exists)
                {
                    map[d.FullName].Add(d.CarId);
                }
            }

            Dictionary<string, int> result = new Dictionary<string, int>();
            foreach (KeyValuePair<string, List<int>> pair in map)
            {
                if (pair.Value.Count > 1)
                {
                    result[pair.Key] = pair.Value.Count;
                }
            }
            return result;
        }

        /// <summary>
        /// Печатает все машины с водителем и маршрутом.
        /// </summary>
        static void PrintAllCars(List<Car> cars, List<Route> routes, List<Driver> drivers)
        {
            if (cars == null)
            {
                throw new ArgumentNullException("Список машин не может быть null.");
            }

            if (routes == null)
            {
                throw new ArgumentNullException("Список маршрутов не может быть null.");
            }

            if (drivers == null)
            {
                throw new ArgumentNullException("Список водителей не может быть null.");
            }

            foreach (Car c in cars)
            {
                Driver? driver = null;
                foreach (Driver d in drivers)
                {
                    if (d.CarId == c.Id)
                    {
                        driver = d;
                        break;
                    }
                }

                Route? route = null;
                foreach (Route r in routes)
                {
                    if (r.Id == c.RouteId)
                    {
                        route = r;
                        break;
                    }
                }

                string driverText = driver != null ? driver.GetInfo() : "—";
                string routeText = route != null ? "\"" + route.Name + "\" (" + route.Distance + " км)" : "—";

                Console.WriteLine("\"" + c.GetInfo() + "\" — водитель " + driverText + ", маршрут " + routeText);
            }
        }
    }
}