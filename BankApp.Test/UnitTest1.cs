using BankApp.Api;
using BankApp.Models;

namespace BankApp.Test
{
    public class UnitTest1
    {
        private readonly BankDbContext _db;
        public UnitTest1(BankDbContext db)
        {
            _db = db;
        }

        [Fact]
        public void SendMoney()
        {
            string accountFrom = "dsadsa";
            string accountTo = "djaskldjas";
            decimal amount = 10;

            MoneySender moneySender = new MoneySender(_db);
            moneySender.SendMoney(accountFrom, accountTo, amount);

            //შეამოწმე ბაზაში თუ სწორად შეცვალა დატა.
        }
    }
}