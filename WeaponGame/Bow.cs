namespace WeaponGame;

/// <summary>
/// Лук. Стрелковое оружие. Можно перезарядить.
/// </summary>
public class Bow : Weapon, IReloadable
{
    /// <summary>
    /// Количество стрел в колчане.
    /// </summary>
    public int ArrowCount { get; set; }

    /// <summary>
    /// Пустой ли колчан (0 стрел).
    /// </summary>
    public bool IsEmpty => ArrowCount == 0;

    /// <summary>
    /// Создаёт лук.
    /// </summary>
    /// <param name="name">Название.</param>
    /// <param name="damage">Урон.</param>
    /// <param name="arrowCount">Количество стрел.</param>
    public Bow(string name, int damage, int arrowCount)
        : base(name)
    {
        Damage = damage;
        ArrowCount = arrowCount;
        Type = WeaponType.Ranged;
    }

    /// <summary>
    /// Стреляет из лука.
    /// </summary>
    public override void Attack()
    {
        Console.WriteLine($"{Name} fires an arrow.");
    }

    /// <summary>
    /// Перезаряжает лук (добавляет 10 стрел).
    /// </summary>
    public void Reload()
    {
        ArrowCount += 10;
        Console.WriteLine($"{Name} reloads arrows.");
    }

    /// <summary>
    /// Возвращает описание лука.
    /// </summary>
    public override string ToString()
        => $"{base.ToString()}, arrows: {ArrowCount}\n  dps: {DamagePerSecond}, empty: {IsEmpty}";
}