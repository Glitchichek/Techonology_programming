using Bank;
using System;

namespace Bank
{
    /// <summary>
    /// Счёт с кредитной линией. Может уходить в минус до установленного лимита. Начисляет проценты на отрицательный баланс.
    /// </summary>
    public class LineOfCreditAccount : BankAccount
    {

        private readonly decimal _creditLimit;
        /// <summary>
        /// Создаёт счёт с кредитной линией и указанным лимитом.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="initialBalance"></param>
        /// <param name="creditLimit"></param>
        public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit)
            : base(name, initialBalance, -creditLimit)
        {
            _creditLimit = creditLimit;
        }
        /// <summary>
        /// Проверяет, превышен ли лимит снятия средств. Если баланс отрицательный, то снимается комиссия за овердрафт.
        /// </summary>
        /// <param name="isOverdrawn"></param>
        /// <returns></returns>
        protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn)
            => isOverdrawn
                ? new Transaction(-20m, DateTime.UtcNow, "apply overdraft fee")
                : default;
        /// <summary>
        /// Начисляет 7% годовых на отрицательный баланс.
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
        /// Возвращает строковое представление счёта с кредитной линией, включая лимит кредита.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{base.ToString()} Credit limit: {_creditLimit}";
    }
}