namespace Bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount account = new BankAccount("Ivan", 35000);
            BankAccount account1 = new BankAccount("B0ba", 35500);
            Console.WriteLine(account.Owner + " " +  account.Balance + " " + account.Number);
            Console.WriteLine(account1.Owner + " " +  account1.Balance + " " + account1.Number);

            account.MakeWithdrawal(200, DateTime.UtcNow, ":)");
            Console.WriteLine(account1.Balance);
            account1.MakeWithdrawal(200, DateTime.UtcNow, ":)");
            Console.WriteLine(account1.Balance);


            try
            {
                account1.MakeWithdrawal(2000000, DateTime.UtcNow, ":)");

            }
            catch (InvalidOperationException) {
            Console.WriteLine(account1.Balance);

            }
        }
    }
}
