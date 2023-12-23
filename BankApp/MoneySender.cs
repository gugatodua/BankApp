using System.Security.Principal;
using BankApp.Api.Services;
using BankApp.Models;

namespace BankApp.Api
{
    public class MoneySender : IMoneySender
    {
        private readonly BankDbContext _dbContext;
        public MoneySender(BankDbContext dbContext)
        {
           _dbContext = dbContext;
        }

        public void SendMoney(string accountFrom, string accountTo, decimal amount)
        {
            var credit = _dbContext.Accounts.Where(x => x.Iban == accountFrom).FirstOrDefault();
            var debit = _dbContext.Accounts.Where(x => x.Iban == accountTo).FirstOrDefault();

            if (credit == null)
            {
                throw new ArgumentException("Credit account not found");
            }

            if (debit == null)
            {
                throw new ArgumentException("Debit account not found");
            }

            if (credit.Balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds in the credit account");
            }

            credit.Balance -= amount;
            debit.Balance += amount;

            _dbContext.SaveChanges();
        }

        public void SendMoneyInternal(string accountFrom, string accountTo, decimal amount)
        {
            var credit = _dbContext.Accounts.Where(x => x.Iban == accountFrom).FirstOrDefault();
            var debit = _dbContext.Accounts.Where(x => x.Iban == accountTo).FirstOrDefault();

            if (credit == null || debit == null)
            {
                throw new ArgumentException("Invalid account information");
            }

            if (amount <= 0)
            {
                throw new ArgumentException("Amount should be greater than zero");
            }

            if (credit.Balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds in the credit account");
            }

            if (credit.UserId != debit.UserId)
            {
                throw new InvalidOperationException("Accounts do not belong to the same user");
            }

            credit.Balance -= amount;
            debit.Balance += amount;

            _dbContext.SaveChanges();

        }

        public void DepositMoney(decimal amount, string iban, string currency)
        {
            var account = _dbContext.Accounts.Where(x => x.Iban == iban).FirstOrDefault();
            var bankWallet = _dbContext.BankWallets.Where(x => x.Currency.Name == currency).FirstOrDefault();

            if (account == null)
            {
                throw new ArgumentException("Invalid account");
            }

            if (bankWallet == null)
            {
                throw new ArgumentException("Bank wallet for the specified currency not found");
            }

            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount should be greater than zero");
            }

            account.Balance += amount;
            bankWallet.Amount += amount;

            _dbContext.SaveChanges();
        }
        public void WithdrawFromDepositAccount(string accountFrom, string accountTo, decimal amount)
        {
            var credit = _dbContext.Accounts.Where(x => x.Iban == accountFrom).FirstOrDefault();
            var debit = _dbContext.Accounts.Where(x => x.Iban == accountTo).FirstOrDefault();
            
            if (credit == null || debit == null)
            {
                throw new ArgumentException("Invalid account information");
            }

            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount should be greater than zero");
            }

            if (credit.Balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds in the credit account");
            }

            if (credit.WithdrawDate.HasValue && (DateTime.Now - credit.WithdrawDate.Value).TotalDays > 365)
            {
                throw new InvalidOperationException("Withdrawal not allowed after 1 year since last withdrawal");
            }
            credit.Balance -= amount;
            debit.Balance += amount;

            _dbContext.SaveChanges();
        }
        public void DepositToAccount(string accountFrom, string accountTo, decimal amount)
        {
            Account credit = _dbContext.Accounts.Where(x => x.Iban == accountFrom).FirstOrDefault();
            Account debit = _dbContext.Accounts.Where(x => x.Iban == accountTo).FirstOrDefault();

            if (credit == null || debit == null)
            {
                throw new ArgumentException("Invalid account information");
            }

            if (credit.Balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds in the credit account");
            }

            if (credit.UserId != debit.UserId)
            {
                throw new InvalidOperationException("Accounts do not belong to the same user");
            }

            credit.Balance -= amount;
            debit.Balance += amount;

            _dbContext.SaveChanges();
        }

    }
}
