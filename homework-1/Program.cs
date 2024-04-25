using System;

namespace BankingApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BankAccount account = new BankAccount();
            account.Deposit(100);
            account.Withdraw(50);
            account.Withdraw(70);
        }
    }
}
