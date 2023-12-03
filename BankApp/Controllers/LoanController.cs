using System.Security.Cryptography.X509Certificates;
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
            this._dbContext = dbContext;
        }

        [HttpPost] 
        public IActionResult IssueLoan(string iban, decimal amount, decimal duration, int paymentDay, string currency)
        {
            var account = _dbContext.Accounts.Where(x => x.Iban == iban).FirstOrDefault();

            var loan = new Loan
            {
                UserId = account.UserId,
                Amount = amount,
                Duration = duration,
                PaymentDay = paymentDay,
                Currency = currency
            };

            account.Balance += amount;
            _dbContext.SaveChanges();   

            return Ok();
        }


    }
}
