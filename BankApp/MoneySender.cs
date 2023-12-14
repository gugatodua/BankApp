using BankApp.Domain;
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

            credit.Balance -= amount;
            debit.Balance += amount;

            _dbContext.SaveChanges();
        }
    }
}
