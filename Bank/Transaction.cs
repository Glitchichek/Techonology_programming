using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    // You can't change the state of the class

    internal record Transaction(decimal Amount, DateTime Date, string Note); 

    //internal record Transaction
    //{
    //    public decimal Amount { get; }
    //    public DateTime Date { get; }
    //    public string Note { get; }

    //    public Transaction(decimal Amount, DateTime Date, string Note)
    //    {
    //        this.Amount = Amount;
    //        this.Date = Date;
    //        this.Note = Note;
    //    }
    //}
    
}
