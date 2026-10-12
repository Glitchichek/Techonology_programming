using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    /// <summary>
    /// Запись о банковской операции. Состояние неизменяемо.
    /// </summary>
    /// <param name="Amount">Сумма операции. Положительная — пополнение, отрицательная — списание.</param>
    /// <param name="Date">Дата и время операции.</param>
    /// <param name="Note">Текстовый комментарий к операции.</param>
    public record Transaction(decimal Amount, DateTime Date, string Note);
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
