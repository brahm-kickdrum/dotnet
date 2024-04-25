using System;

namespace BankingApplication
{
    public class BankAccount
    {
        private int _balance;

        public BankAccount()
        {
            _balance = 0;
        }

        public void Deposit(int amount)
        {
            _balance += amount;
            Console.WriteLine($"Deposited ₹{amount}. New _balance: {_balance}");
        }

        public void Withdraw(int amount)
        {
            if (amount > _balance)
            {
                Console.WriteLine("Insufficient _balance. Withdrawal failed.");
            }
            else
            {
                _balance -= amount;
                Console.WriteLine($"Withdrawn ₹{amount}. New _balance: {_balance}");
            }
        }
    }
}
