namespace WeaponGame;

/// <summary>
/// Интерфейс для оружия, которое можно перезарядить.
/// </summary>
public interface IReloadable
{
    /// <summary>
    /// Перезаряжает оружие.
    /// </summary>
    void Reload();
}