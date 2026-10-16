using System;
using System.Collections.Generic;

namespace WeaponGame
{
    /// <summary>
    /// Точка входа приложения. Демонстрирует полиморфизм оружия.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Запускает демонстрацию.
        /// </summary>
        static void Main(string[] args)
        {
            // Список оружия базового типа Weapon
            List<Weapon> weapons = new List<Weapon>
            {
                new Sword("Excalibur", 50, 100),
                new Bow("Longbow", 30, 0),
                new Staff("Arcane Staff", 20, 80)
            };

            foreach (Weapon weapon in weapons)
            {
                // 1. ToString() выводит описание полиморфизм
                Console.WriteLine(weapon);

                // 2. Attack() своя реализация у каждого наследника
                weapon.Attack();

                // 3. Проверка через is / pattern matching
                if (weapon is IReloadable reloadable)
                {
                    reloadable.Reload();
                }
                else
                {
                    Console.WriteLine($"{weapon.Name} cannot reload.");
                }

                // 4. Специфичные проверки для наследников
                if (weapon is Sword sword)
                {
                    Console.WriteLine($"  sword blade length: {sword.BladeLength}");
                }
                else if (weapon is Bow bow)
                {
                    Console.WriteLine($"  bow arrows: {bow.ArrowCount}");
                }
                else if (weapon is Staff staff)
                {
                    Console.WriteLine($"  staff mana cost: {staff.ManaCost}");
                }
            }
        }
    }
}