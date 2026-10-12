using Bank;

namespace Bank
{
    /// <summary>
    /// Счёт подарочной карты. Может иметь ежемесячный депозит.
    /// </summary>
    public class GiftCardAccount : BankAccount
    {
        private readonly decimal _monthlyDeposit = 0m;
        /// <summary>
        /// Создаёт счёт подарочной карты с ежемесячным депозитом.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="initialBalance"></param>
        /// <param name="monthlyDeposit"></param>
        public GiftCardAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0m)
            : base(name, initialBalance)
        {
            _monthlyDeposit = monthlyDeposit;
        }
        /// <summary>
        /// Добавляет ежемесячный депозит, если его задали.
        /// </summary>

        public override void PerformMonthAndTransactions()
        {
            if (_monthlyDeposit != 0)
            {
                MakeDeposit(_monthlyDeposit, DateTime.UtcNow, "Add monthly deposit");
            }
        }
        /// <summary>
        /// Возвращает строковое представление счёта подарочной карты, включая ежемесячный депозит.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{base.ToString()} Monthly deposit: {_monthlyDeposit}";
    }
}