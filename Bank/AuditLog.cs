using System;
using System.Collections.Generic;

namespace Bank;

/// <summary>
/// Журнал аудита банковских операций.
/// </summary>
public static class AuditLog
{
    private static readonly List<string> _entries = new();

    /// <summary>
    /// Записывает операцию в журнал аудита.
    /// </summary>
    /// <param name="accountNumber">Номер счёта.</param>
    /// <param name="operationType">Тип операции: Deposit, Withdrawal или Interest.</param>
    /// <param name="amount">Сумма операции.</param>
    /// <param name="date">Дата и время операции.</param>
    public static void Record(string accountNumber, string operationType,
                              decimal amount, DateTime date)
    {
        string entry = $"Дата: {date}, Счёт: {accountNumber}, Тип: {operationType}, Сумма: {amount}";
        _entries.Add(entry);
    }

    /// <summary>
    /// Возвращает все записи журнала аудита.
    /// </summary>
    /// <returns>Список строк журнала.</returns>
    public static IReadOnlyList<string> GetEntries() => _entries;
}