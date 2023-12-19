using BankApp.Api;
using BankApp.Models;

namespace BankApp.Test
{
    public class MoneySenderTests
    {
        private readonly BankDbContext _db;
        public MoneySenderTests(BankDbContext db)
        {
            _db = db;
        }

        [Fact]
        public void SendMoney()
        {
            string accountFrom = "validAccountFrom";
            string accountTo = "validAccountTo";
            decimal amount = 100;

            var creditAccount = new Account { Iban = accountFrom, Balance = 200 };
            var debitAccount = new Account { Iban = accountTo, Balance = 50 };

            _db.Accounts.Add(creditAccount);
            _db.Accounts.Add(debitAccount);

            var moneySender = new MoneySender(_db);
            moneySender.SendMoney(accountFrom, accountTo, amount);

            Assert.Equal(100, creditAccount.Balance);
            Assert.Equal(150, debitAccount.Balance);

        }
    }
}