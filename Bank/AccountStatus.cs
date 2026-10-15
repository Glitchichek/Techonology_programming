namespace Bank;

/// <summary>
/// Статус клиента банка. Определяет ежемесячную комиссию и бонусы.
/// </summary>
public enum AccountStatus
{
    /// <summary>Обычный клиент. Комиссия 50 руб/мес.</summary>
    Regular,

    /// <summary>Премиум-клиент. Без комиссии, 1% на остаток свыше 10 000.</summary>
    Premium,

    /// <summary>VIP-клиент. Без комиссии, 3% на остаток свыше 10 000, 5% кэшбэк на списания.</summary>
    Vip
}