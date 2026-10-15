namespace Bank;

/// <summary>
/// Счет с кредитной линией. Допускает отрицательный баланс в пределах лимита.
/// Начисляет проценты на сумму долга в конце месяца.
/// </summary>
public class LineOfCreditAccount : BankAccount
{
    /// <summary>
    /// Создаёт счет с кредитной линией и указанным лимитом.
    /// </summary>
    /// <param name="name">Имя владельца.</param>
    /// <param name="initialBalance">Начальный баланс.</param>
    /// <param name="creditLimit">Кредитный лимит. Баланс не может стать меньше −creditLimit.</param>
    public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit)
        : base(name, initialBalance, -creditLimit)
    {
    }

    /// <summary>
    /// Начисляет 7% от суммы долга, если баланс отрицательный.
    /// Начисление оформляется как списание — долг растёт.
    /// </summary>
    public override void PerformMonthAndTransactions()
    {
        if (Balance < 0)
        {
            decimal interest = -Balance * 0.07m;
            MakeWithdrawal(interest, DateTime.UtcNow, "Charge monthly interest");
        }
    }

    /// <summary>
    /// Возвращает штрафную транзакцию, если снятие выводит баланс за кредитный лимит.
    /// </summary>
    /// <param name="isOverdrawn">Признак выхода за минимально допустимый баланс.</param>
    /// <returns>Транзакция с комиссией 20 или <c>null</c>, если превышения нет.</returns>
    protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn)
        => isOverdrawn
            ? new Transaction(-20m, DateTime.UtcNow, "apply overdraft")
            : default;
}