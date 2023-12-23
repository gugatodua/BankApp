using BankApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly BankDbContext _dbContext;

        public LoanController(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult IssueLoan(decimal amount, string currencyAbbreviation, int timeLimit, 
                                        decimal rate, int paymentDay, string userId)
        {
            var currencyFromDb = _dbContext.Currencies
                .Where(x=>x.Abbreviation==currencyAbbreviation)
                .FirstOrDefault();

            if (currencyFromDb == null || (currencyFromDb.Abbreviation != "GEL"
                && currencyFromDb.Abbreviation != "USD" && currencyFromDb.Abbreviation != "EUR"))
            {
                return BadRequest("Invalid currency");
            }
                

                var loan = new Loan
            {
                Amount = amount,
                Currency = currencyFromDb,
                TimeLimit = timeLimit,
                Rate = rate,
                PaymentDay = paymentDay,
                UserId = userId
            };

            var account = _dbContext.Accounts.Where(x => x.UserId == userId).FirstOrDefault();
            account.Balance += amount;
            _dbContext.Add(loan);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
