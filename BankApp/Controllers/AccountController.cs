using BankApp.Dtos;
using BankApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly BankDbContext _dbContext;
        public AccountController(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult AddAccount([FromBody] AccountDto accountDto)
        {
            var user = _dbContext.Users
                .Where(x => x.Id == accountDto.UserId)
                .FirstOrDefault();

            var account = new Account
            {
                Id = Guid.NewGuid(),
                Balance = 0,
                Iban = accountDto.Iban,
                User = user,
                Currency = accountDto.Currency
            };

            _dbContext.Add(account);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpPut("SendMoney")]
        public IActionResult SendMoney(string accountFrom, string accountTo, decimal amount)
        {
            var credit = _dbContext.Accounts.Where(x => x.Iban == accountFrom).FirstOrDefault();
            var debit = _dbContext.Accounts.Where(x => x.Iban == accountTo).FirstOrDefault();

            credit.Balance -= amount;
            debit.Balance += amount;

            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpPut("SendMoneyInternal")]
        public IActionResult SendMoneyInternal(string accountFrom, string accountTo, decimal amount)
        {
            Account credit = _dbContext.Accounts.Where(x => x.Iban == accountFrom).FirstOrDefault();
            Account debit = _dbContext.Accounts.Where(x => x.Iban == accountTo).FirstOrDefault();

            if (credit.Balance < amount)
            {
                return BadRequest();
            }

            if (credit.UserId != debit.UserId)
            {
                return BadRequest();
            }

            credit.Balance -= amount;
            debit.Balance += amount;

            _dbContext.SaveChanges();

            return Ok();

        }

        [HttpPut("Convert")]
        public IActionResult Convert(string accountFrom, string accountTo, decimal amount)
        {
            var credit = _dbContext.Accounts.Where(x => x.Iban == accountFrom).FirstOrDefault();
            var debit = _dbContext.Accounts.Where(x => x.Iban == accountTo).FirstOrDefault();

            if (credit.Balance < amount)
            {
                return BadRequest();
            }

            if(credit.Currency == "GEL" && debit.Currency == "GEL") 
            {
                credit.Balance -= amount;
                debit.Balance += amount;
            } 
            else if(credit.Currency == "USD" && debit.Currency == "USD")
            {
                credit.Balance -= amount;
                debit.Balance += amount;
            }
            else if( credit.Currency == "GEL" &&  debit.Currency == "USD")
            {
                credit.Balance -= amount;
                debit.Balance += amount / 3;
            }
            else if(credit.Currency == "USD" && debit.Currency == "GEL" )
            {
                credit.Balance -= amount;
                debit.Balance += amount * 3;
            }

            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("DepositMoney")]
        public IActionResult DepositMoney(decimal amount, string iban, string currency)
        {
            var account = _dbContext.Accounts.Where(x => x.Iban == iban).FirstOrDefault();
            var bankWallet = _dbContext.BankWallets.Where(x => x.Currency == currency).FirstOrDefault();

            account.Balance += amount;
            bankWallet.Amount += amount;

            _dbContext.SaveChanges();

            return Ok();    
        } 
    }
}
