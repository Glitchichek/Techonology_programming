using System.Text;

namespace Bank;

/// <summary>
/// Банковский счёт, хранящий историю операций и рассчитывающий текущий баланс.
/// </summary>
public class BankAccount
{
    private readonly decimal _minimumBalance;
    private static int s_accountNumberSeed = 1000000000;
    private readonly List<Transaction> _allTransactions = new();

    /// <summary>Уникальный номер счёта.</summary>
    public string Number { get; } = string.Empty;

    /// <summary>Имя владельца счёта. Устанавливается в конструкторе; извне — только чтение.</summary>
    public string Owner { get; private set; } = string.Empty;

    /// <summary>
    /// Статус клиента. Определяет ежемесячные начисления и комиссии.
    /// По умолчанию — <see cref="AccountStatus.Regular"/>.
    /// </summary>
    public AccountStatus Status { get; set; } = AccountStatus.Regular;

    /// <summary>
    /// Текущий баланс счёта, рассчитанный как сумма всех транзакций.
    /// </summary>
    public decimal Balance
    {
        get
        {
            decimal balance = 0;
            foreach (var item in _allTransactions)
                balance += item.Amount;
            return balance;
        }
    }

    /// <summary>Создаёт счёт с нулевым минимальным балансом.</summary>
    /// <param name="name">Имя владельца.</param>
    /// <param name="initialBalance">Начальный баланс.</param>
    public BankAccount(string name, decimal initialBalance)
        : this(name, initialBalance, 0) { }

    /// <summary>Создаёт счет с заданным минимальным балансом.</summary>
    /// <param name="name">Имя владельца.</param>
    /// <param name="initialBalance">Начальный баланс.</param>
    /// <param name="minimumBalance">Минимально допустимый баланс.</param>
    public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
    {
        Owner = name;
        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;
        _minimumBalance = minimumBalance;

        if (initialBalance > 0)
            MakeDeposit(initialBalance, DateTime.UtcNow, "Initial balance");
    }

    /// <summary>Пополняет счет на указанную сумму.</summary>
    /// <param name="amount">Сумма пополнения. Должна быть положительной.</param>
    /// <param name="date">Дата операции.</param>
    /// <param name="note">Комментарий к операции.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Бросается, если <paramref name="amount"/> меньше или равен нулю.
    /// </exception>
    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount),
                "Amount of deposit must be positive");

        var deposit = new Transaction(amount, date, note);
        _allTransactions.Add(deposit);
    }

    /// <summary>
    /// Снимает средства со счёта. Если после снятия баланс выходит за минимум,
    /// вызывается <see cref="CheckWithdrawalLimit"/>.
    /// </summary>
    /// <param name="amount">Сумма списания. Должна быть положительной.</param>
    /// <param name="date">Дата операции.</param>
    /// <param name="note">Комментарий к операции.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Бросается, если <paramref name="amount"/> меньше или равен нулю.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Бросается, если снятие превышает допустимый минимум.
    /// </exception>
    public virtual void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

        Transaction? overdraftTransaction
            = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
        Transaction? withdrawal = new(-amount, date, note);

        _allTransactions.Add(withdrawal);

        if (overdraftTransaction is not null)
            _allTransactions.Add(overdraftTransaction);
    }

    /// <summary>
    /// Проверяет, не вышел ли баланс за минимально допустимый.
    /// Базовая реализация бросает исключение; наследники могут вернуть штрафную транзакцию.
    /// </summary>
    /// <param name="isOverdrawn">Признак выхода за минимальный баланс.</param>
    /// <returns>Штрафная транзакция или <c>null</c>, если штрафа нет.</returns>
    /// <exception cref="InvalidOperationException">
    /// Бросается, если снятие недопустимо.
    /// </exception>
    protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
    {
        if (isOverdrawn)
            throw new InvalidOperationException("Not sufficient funds for this withdrawal");
        return default;
    }

    /// <summary>Возвращает историю операций счёта в виде текста.</summary>
    /// <returns>Многострочная строка с датой, суммой, балансом и комментарием.</returns>
    public string GetAccountHistory()
    {
        var report = new StringBuilder();
        decimal balance = 0;
        report.AppendLine("Date\t\tAmount\tBalance\tNote");
        foreach (var item in _allTransactions)
        {
            balance += item.Amount;
            report.AppendLine(
                $"{item.Date.ToShortDateString()}\t" +
                $"{item.Amount}\t{balance}\t{item.Note}");
        }
        return report.ToString();
    }

    /// <summary>
    /// Выполняет операции, положенные в конце месяца.
    /// Базовая реализация ничего не делает.
    /// </summary>
    /// /// <summary>
    /// Выполняет ежемесячные операции: списывает комиссию и начисляет бонусы
    /// в зависимости от <see cref="Status"/>.
    /// </summary>
    public virtual void PerformMonthAndTransactions()
    {
        switch (Status)
        {
            case AccountStatus.Regular:
                if (Balance >= 50m)
                    MakeWithdrawal(50m, DateTime.UtcNow, "Monthly fee (Regular)");
                break;

            case AccountStatus.Premium:
                if (Balance > 10000m)
                {
                    decimal interest = (Balance - 10000m) * 0.01m;
                    MakeDeposit(interest, DateTime.UtcNow, "Premium interest");
                }
                break;

            case AccountStatus.Vip:
                if (Balance > 10000m)
                {
                    decimal interest = (Balance - 10000m) * 0.03m;
                    MakeDeposit(interest, DateTime.UtcNow, "VIP interest");
                }
                decimal cashback = CalculateCashback();
                if (cashback > 0)
                    MakeDeposit(cashback, DateTime.UtcNow, "VIP cashback");
                break;
        }
    }

    private decimal CalculateCashback()
    {
        decimal totalWithdrawals = 0;
        foreach (var t in _allTransactions)
        {
            if (t.Amount < 0 && t.Note != "VIP cashback")
                totalWithdrawals += -t.Amount;
        }
        return totalWithdrawals * 0.05m;
    }

    /// <summary>Возвращает текстовое описание счёта.</summary>
    /// <returns>Строка с владельцем, номером и фактическим типом счёта.</returns>
    public override string ToString()
        => $"Owner: {Owner}\taccount number: {Number} (тип счета {GetType().Name})";
}