using System;
using System.Collections.Generic;
using System.Text;

namespace WeaponGame
{
    /// <summary>
    /// Тип оружия. Определяет категорию: ближний бой, стрелковое, магическое.
    /// </summary>
    public enum WeaponType
    {
        /// <summary>
        /// Ближний бой 
        /// </summary>
        Melee,

        /// <summary>
        /// Стрелковое
        /// </summary>
        Ranged,

        /// <summary>
        /// Магическое
        /// </summary>
        Magic
    }
}
