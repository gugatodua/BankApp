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
        private readonly Converter _converter;
        public AccountController(BankDbContext dbContext, MoneySender moneySender, Converter converter)
        {
            _dbContext = dbContext;
            _moneySender = moneySender;
            _converter = converter; 
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
           _converter.Convert(accountFrom, accountTo, amount);

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