namespace Bank;
/// <summary>
/// Накопительный счёт. Начисляет проценты на остаток в конце месяца.
/// </summary>
public class InterestEarningAccount : BankAccount
{
    /// <summary>
    /// Создаёт накопительный счёт с нулевым минимальным балансом.
    /// </summary>
    /// <param name="name">Имя владельца счёта.</param>
    /// <param name="initialBalance">Начальный баланс счёта.</param>
    public InterestEarningAccount(string name, decimal initialBalance)
        : base(name, initialBalance)
    {

    }

    // override позволяет в дочернем классе определить новую реализацию
    // метода PerformMonthAndTransactions

    /// <summary>
    /// Начисляет проценты на остаток, если баланс > 500. Ставка 2% от текущего баланса.
    /// </summary>
    public override void PerformMonthAndTransactions()
    {
        if (Balance > 500m)
        {
            decimal interest = Balance * 0.02m;
            MakeDeposit(interest, DateTime.UtcNow, "Apply month interest");
        }
    }
}