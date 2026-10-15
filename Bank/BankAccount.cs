using System;
using System.Collections.Generic;
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

    /// <summary>Имя владельца счёта.</summary>
    public string Owner { get; private set; } = string.Empty;

    /// <summary>
    /// Статус клиента. Определяет ежемесячные начисления и комиссии.
    /// По умолчанию — <see cref="AccountStatus.Regular"/>.
    /// </summary>
    // ОТЧЁТ (Задача 1): добавлено свойство Status типа AccountStatus.
    // От статуса зависят ежемесячные комиссии и бонусы.
    // Значение по умолчанию — Regular.
    public AccountStatus Status { get; set; } = AccountStatus.Regular;

    /// <summary>Текущий баланс счёта, рассчитанный как сумма всех транзакций.</summary>
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
    public BankAccount(string name, decimal initialBalance)
        : this(name, initialBalance, 0) { }

    /// <summary>Создаёт счёт с заданным минимальным балансом.</summary>
    public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
    {
        Owner = name;
        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;
        _minimumBalance = minimumBalance;

        if (initialBalance > 0)
            MakeDeposit(initialBalance, DateTime.UtcNow, "Initial balance");
    }

    /// <summary>Пополняет счёт на указанную сумму.</summary>
    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount),
                "Amount of deposit must be positive");

        var deposit = new Transaction(amount, date, note);
        _allTransactions.Add(deposit);
        // ОТЧЁТ (Задача 2): здесь создаётся Transaction для пополнения.
        // Поэтому сюда добавлен вызов AuditLog.Record с типом Deposit.
        AuditLog.Record(Number, "Deposit", amount, date);
    }

    /// <summary>Снимает средства со счёта.</summary>
    public virtual void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

        Transaction? overdraftTransaction
            = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
        Transaction? withdrawal = new(-amount, date, note);

        _allTransactions.Add(withdrawal);

        // ОТЧЁТ (Задача 2): здесь создаётся Transaction для списания.
        // Вызов AuditLog.Record с типом Withdrawal.
        AuditLog.Record(Number, "Withdrawal", amount, date);

        if (overdraftTransaction is not null)
        {
            _allTransactions.Add(overdraftTransaction);

            // ОТЧЁТ (Задача 2): штрафная транзакция за овердрафт —
            // тоже операция, тоже логируется.
            AuditLog.Record(Number, "Withdrawal", -overdraftTransaction.Amount, date);
        }
    }

    /// <summary>Проверяет, не вышел ли баланс за минимум.</summary>
    protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
    {
        if (isOverdrawn)
            throw new InvalidOperationException("Not sufficient funds for this withdrawal");
        return default;
    }

    /// <summary>Возвращает историю операций счёта.</summary>
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
    /// Выполняет ежемесячные операции: списывает комиссию и начисляет
    /// бонусы в зависимости от <see cref="Status"/>.
    /// </summary>
    // ОТЧЁТ (Задача 1): здесь реализована логика статусов.
    // Раньше метод был пустой, а теперь внутри switch: Regular списывает 50 руб, Premium начисляет 1% на остаток > 10 000, Vip начисляет 3% на остаток > 10 000 и 5% кэшбэк.
    //
    // Наблюдения для отчёта:
    // Логика процентов (Premium/Vip) похожа на то, что делает InterestEarningAccount, есть дублирование.
    //
    // Сколько мест в коде пришлось изменить? — 2 (свойство + метод).
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

    // ОТЧЁТ (Задача 1): новый вспомогательный метод для VIP-кэшбэка.
    // Считает 5% от суммы всех списаний по счёту.
    // VIP-статус требует кэшбэка, которого раньше не было.
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
    public override string ToString()
        => $"Owner: {Owner}\taccount number: {Number} (тип счета {GetType().Name})";
}