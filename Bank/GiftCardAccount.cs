namespace Bank;

/// <summary>
/// Подарочная карта. Ежемесячно пополняется на заданную сумму.
/// </summary>
public class GiftCardAccount : BankAccount
{
    private readonly decimal _monthlyDeposit = 0m;


    // monthlyDeposit - параметр по умолчанию (принимает 0),
    // при создании new GiftCartAccount("Yana",1000); => monthlyDeposit = 0
    // new GiftCartAccount("Yana",1000,5000); => monthlyDeposit = 5000
    

    /// <summary>
    /// Создаёт подарочную карту.
    /// </summary>
    /// <param name="name">Имя владельца.</param>
    /// <param name="initialBalance">Начальный баланс.</param>
    /// <param name="monthlyDeposit">Ежемесячное пополнение. По умолчанию 0 — пополнений нет.</param>
    public GiftCardAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0m)
        : base(name, initialBalance)
        => _monthlyDeposit = monthlyDeposit;

    /// <summary>
    /// Пополняет карту на сумму ежемесячного пополнения, если она задана.
    /// </summary>
    public override void PerformMonthAndTransactions()
    {
        if (_monthlyDeposit != 0)
        {
            MakeDeposit(_monthlyDeposit, DateTime.UtcNow, "Add monthly deposit");
        }
    }

    /// <summary>
    /// Возвращает текстовое описание карты, включая размер ежемесячного пополнения.
    /// </summary>
    /// <returns>Строка с типом счёта, владельцем, номером и ежемесячным пополнением.</returns>
    public override string ToString()
        => $"{base.ToString()} Monthly deposit: {_monthlyDeposit}";
}