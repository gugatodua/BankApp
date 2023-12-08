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
        public IActionResult IssueLoan(decimal amount, string currency, int timeLimit, 
                                        decimal rate, int paymentDay, string userId)
        {
            var loan = new Loan
            {
                Amount = amount,
                Currency = currency,
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
