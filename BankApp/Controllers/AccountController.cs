using System.Diagnostics;
using BankApp.Api;
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
        private readonly MoneySender _moneySender;
        public AccountController(BankDbContext dbContext, MoneySender moneySender)
        {
            _dbContext = dbContext;
            _moneySender = moneySender;
        }

        [HttpPost]
        public IActionResult AddAccount([FromBody] AccountDto accountDto)
        {
            AddAccountRecord(
             accountDto.UserId,
             accountDto.Iban,
             accountDto.Currency);

            return Ok();
        }

        [HttpPut("SendMoney")]
        public IActionResult SendMoney(string accountFrom, string accountTo, decimal amount)
        {
            _moneySender.SendMoney(accountFrom, accountTo, amount);

            return Ok();
        }

        [HttpPut("SendMoneyInternal")]
        public IActionResult SendMoneyInternal(string accountFrom, string accountTo, decimal amount)
        {
            _moneySender.SendMoneyInternal(accountFrom, accountTo, amount);

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

            return Ok();
        }

        [HttpPost("DepositMoney")]
        public IActionResult DepositMoney(decimal amount, string iban, string currency)
        {
           _moneySender.DepositMoney(amount, iban, currency);

            return Ok();    
        }

        [HttpPost("AddDeposit")]
        public IActionResult AddDeposit([FromBody] DepositDto depositDto)
        {
            AddAccountRecord(
                depositDto.UserId, 
                depositDto.Iban, 
                depositDto.Currency, 
                depositDto.IsDeposit, 
                depositDto.WithdrawDate);

            return Ok();
        } 

        private void AddAccountRecord(
            string userId, 
            string iban,
            string currency,
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

        [HttpPut("WithdrawFromDepositAccount")]
        public IActionResult WithdrawFromDepositAccount(string accountFrom, string accountTo, 
                                                        decimal amount)

        {
           _moneySender.WithdrawFromDepositAccount(accountFrom, accountTo, amount);

            return Ok();

        }

        [HttpPut("DepositToAccount")]
        public IActionResult DepositToAccount(string accountFrom, string accountTo, decimal amount)
        {
            _moneySender.DepositToAccount(accountFrom, accountTo, amount);

            return Ok();
        }
    }
}