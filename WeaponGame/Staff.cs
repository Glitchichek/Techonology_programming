namespace WeaponGame;

/// <summary>
/// Посох. Магическое оружие.
/// </summary>
public class Staff : Weapon
{
    /// <summary>
    /// Стоимость заклинания в мане.
    /// </summary>
    public int ManaCost { get; set; }

    /// <summary>
    /// Мощный ли посох (стоимость маны > 50).
    /// </summary>
    public bool IsPowerful => ManaCost > 50;

    /// <summary>
    /// Создаёт посох.
    /// </summary>
    /// <param name="name">Название.</param>
    /// <param name="damage">Урон.</param>
    /// <param name="manaCost">Стоимость маны.</param>
    public Staff(string name, int damage, int manaCost)
        : base(name)
    {
        Damage = damage;
        ManaCost = manaCost;
        Type = WeaponType.Magic;
    }

    /// <summary>
    /// Атакует магией.
    /// </summary>
    public override void Attack()
    {
        Console.WriteLine($"{Name} casts a spell.");
    }

    /// <summary>
    /// Возвращает описание посоха.
    /// </summary>
    /// <returns>Описание посоха.</returns>
    public override string ToString()
        => $"{base.ToString()}, mana cost: {ManaCost}\n  dps: {DamagePerSecond}, powerful: {IsPowerful}";
}