using System;
using System.Collections.Generic;
using System.Text;

namespace pr2
{
    public class InterestEarningAccount
    {
        public InterestEarningAccount(string name, decimal initalBalance) : base(name, initalBalance) { }
    }
    //override позволяет в дочернем классе определить новую реализацию
    //метода PerformMothAndTransactions
    public override void PerformMonthAndTrnsactions()
        {
            if (Balance > 500m)
            {
                decimalmal interest = Balance * 0.02m;
                MakeDeposit(interest, DateTime.UtcNow, "Apply month interest");
            }
        }
    }
}
