using BankApp.Api.Services;
using BankApp.Models;


namespace BankApp.Api
{
    public class AccountAndDepositAdder : IAccountAndDepositAdder
    {
        private readonly BankDbContext _dbContext;
        public AccountAndDepositAdder(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddAccountRecord(
           string userId,
           string iban,
           Currency currency,
           bool isDeposit = false,
           DateTime? withdrawDate = null)
        {
            var user = _dbContext.Users
             .Where(x => x.Id == userId)
             .FirstOrDefault();

            var account = new Account
            {
                Id = Guid.NewGuid(),
                Balance = 0,
                Iban = iban,
                User = user,
                Currency = currency,
                IsDeposit = isDeposit,
                WithdrawDate = withdrawDate
            };

            _dbContext.Add(account);
            _dbContext.SaveChanges();
        }
    }
}
