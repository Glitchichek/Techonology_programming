using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Bank
{
    internal class BankAccount
    {
        public string Number { get; }
        public string Owner { get; private set; }
        public decimal Balance { 
            get {
                decimal balance = 0;
                foreach (var item in _allTransactions)
                {
                    balance += item.Amount;
                }

                return balance;
            }
        }
        private static int s_accountNumberSeed = 1000000000;
        private List<Transaction> _allTransactions = new List<Transaction>();

        public BankAccount(string Owner, decimal initialBalance)
        {
            this.Owner = Owner; 
            MakeDeposit(initialBalance, DateTime.UtcNow, "Initial Balance");
            Number = s_accountNumberSeed.ToString();
            s_accountNumberSeed++;
        }
        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount < 0) {
                throw new ArgumentOutOfRangeException(nameof(amount), "amount of deposit must be positive");         
            }
           
            var deposit = new Transaction(amount, date, note);
            _allTransactions.Add(deposit);
        }
        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "amount of withdrawal must be positive");
            }
            if (Balance < amount)
            {
                throw new InvalidOperationException("Not sufficient funds");
            }
            var withdrawal = new Transaction(-amount, date, note);
            _allTransactions.Add(withdrawal);
        }
    }
}
