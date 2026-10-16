namespace WeaponGame;

/// <summary>
/// Базовый абстрактный класс оружия.
/// </summary>
public abstract class Weapon
{
    /// <summary>
    /// Уникальный идентификатор оружия.
    /// </summary>
    private readonly int _id;

    /// <summary>
    /// Название оружия.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Урон оружия.
    /// </summary>
    public int Damage { get; set; }

    /// <summary>
    /// Скорость атаки.
    /// </summary>
    public double AttackSpeed { get; set; }

    /// <summary>
    /// Тип оружия. По умолчанию Melee.
    /// </summary>
    public WeaponType Type { get; set; } = WeaponType.Melee;

    /// <summary>Урон в секунду (вычисляемое свойство).</summary>
    public double DamagePerSecond => Damage * AttackSpeed;

    /// <summary>Создаёт оружие с указанными параметрами.</summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="name">Название.</param>
    /// <param name="damage">Урон.</param>
    public Weapon(int id, string name, int damage)
    {
        _id = id;
        Name = name;
        Damage = damage;
        AttackSpeed = 1.0;
    }

    /// <summary>Создаёт оружие с именем, остальные параметры — по умолчанию.</summary>
    /// <param name="name">Название.</param>
    public Weapon(string name) : this(0, name, 0) { }

    /// <summary>Атакует. Должен быть переопределён в наследниках.</summary>
    public abstract void Attack();

    /// <summary>Возвращает текстовое описание оружия.</summary>
    public override string ToString()
        => $"[{GetType().Name}] {Name}, {Damage} dmg, type: {Type}";
}