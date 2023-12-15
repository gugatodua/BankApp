using BankApp.Domain;
using BankApp.Models;

namespace BankApp.Api
{
    public class Converter : IConverter
    {
        private readonly BankDbContext _dbContext;
        public Converter(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Convert(string accountFrom, string accountTo, decimal amount)
        {
            var credit = _dbContext.Accounts.Where(x => x.Iban == accountFrom).FirstOrDefault();
            var debit = _dbContext.Accounts.Where(x => x.Iban == accountTo).FirstOrDefault();

            if (credit.Balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds in the credit account");
            }

            switch (credit.Currency)
            {
                case "GEL" when debit.Currency == "GEL":
                    credit.Balance -= amount;
                    debit.Balance += amount;
                    break;
                case "USD" when debit.Currency == "USD":
                    credit.Balance -= amount;
                    debit.Balance += amount;
                    break;
                case "GEL" when debit.Currency == "USD":
                    credit.Balance -= amount;
                    debit.Balance += amount / 3;
                    break;
                case "USD" when debit.Currency == "GEL":
                    credit.Balance -= amount;
                    debit.Balance += amount * 3;
                    break;
                default:

                    break;
            }

            _dbContext.SaveChanges();
        }
    }
}
