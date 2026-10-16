namespace WeaponGame;

/// <summary>
/// Меч. Ближний бой.
/// </summary>
public class Sword : Weapon
{
    /// <summary>
    /// Длина клинка в сантиметрах.
    /// </summary>
    public int BladeLength { get; set; }

    /// <summary>
    /// Двуручный ли меч (клинок длиннее 90 см).
    /// </summary>
    public bool IsTwoHanded => BladeLength > 90;

    /// <summary>
    /// Создаёт меч.
    /// </summary>
    /// <param name="name">Название.</param>
    /// <param name="damage">Урон.</param>
    /// <param name="bladeLength">Длина клинка.</param>
    public Sword(string name, int damage, int bladeLength)
        : base(name)
    {
        Damage = damage;
        BladeLength = bladeLength;
        Type = WeaponType.Melee;
    }

    /// <summary>
    /// Атакует мечом.
    /// </summary>
    public override void Attack()
    {
        Console.WriteLine($"{Name} swings and strikes.");
    }

    /// <summary>
    /// Возвращает описание меча.
    /// </summary>
    public override string ToString()
        => $"{base.ToString()}\n  dps: {DamagePerSecond}, two-handed: {IsTwoHanded}";
}