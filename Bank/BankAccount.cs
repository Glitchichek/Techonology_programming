using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    /// <summary>
    /// Банковский счёт. Хранит историю операций и рассчитывает баланс.
    /// </summary>
    public class BankAccount
    {
        private static int s_accountNumberSeed = 1000000000;
        private List<Transaction> _allTransactions = new List<Transaction>();
        /// <summary>
        /// Номер счёта.
        /// </summary>
        public string Number { get; }

        /// <summary>
        /// Владелец счёта.
        /// </summary>
        public string Owner { get; private set; }

        /// <summary>
        /// Минимально допустимый баланс.
        /// </summary>
        public decimal MinimumBalance { get; protected set; }

        /// <summary>
        /// Текущий баланс, высчитанный по истории транзакций.
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

        /// <summary>
        /// Создаёт счёт с нулевым минимальным балансом.
        /// </summary>
        public BankAccount(string name, decimal initialBalance)
            : this(name, initialBalance, 0m) { }
        /// <summary>
        /// Создаёт счёт с указанным минимальным балансом.
        /// </summary>
        public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
        {
            Owner = name;
            MinimumBalance = minimumBalance;
            Number = s_accountNumberSeed.ToString();
            s_accountNumberSeed++;

            if (initialBalance > 0)
                MakeDeposit(initialBalance, DateTime.UtcNow, "Initial balance");
        }

        /// <summary>
        /// Вносит средства на счёт.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="date"></param>
        /// <param name="note"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount),
                    "Amount of deposit must be positive");

            var deposit = new Transaction(amount, date, note);
            _allTransactions.Add(deposit);
        }
        /// <summary>
        /// Снимает средства со счёта. Если после снятия средств баланс будет меньше минимального, то вызывается метод CheckWithdrawalLimit.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="date"></param>
        /// <param name="note"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public virtual void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount),
                    "Amount of withdrawal must be positive");

            var overdraft = CheckWithdrawalLimit(Balance - amount < MinimumBalance);
            var withdrawal = new Transaction(-amount, date, note);
            _allTransactions.Add(withdrawal);

            if (overdraft != null)
                _allTransactions.Add(overdraft);
        }
        /// <summary>
        /// Проверяет, не превышен ли лимит снятия средств. Если превышен, то возвращает транзакцию по перерасходу.
        /// </summary>
        /// <param name="isOverdrawn"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
        {
            if (isOverdrawn)
                throw new InvalidOperationException("Not sufficient funds");
            return default;
        }
        /// <summary>
        /// Возвращает историю операций по счёту в виде строки.
        /// </summary>
        /// <returns></returns>
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
        /// Выполняет ежемесячные операции по счёту. По умолчанию не делает ничего.
        /// </summary>

        public virtual void PerformMonthAndTransactions() { }
        /// <summary>
        /// Возвращает строковое представление счёта.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"Type: {GetType().Name}\tOwner: {Owner}\tNumber: {Number}";
    }
}