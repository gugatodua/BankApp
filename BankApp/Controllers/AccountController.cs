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
        private readonly AccountAndDepositAdder _accountAndDepositAdder;
        public AccountController(BankDbContext dbContext, MoneySender moneySender, Converter converter, AccountAndDepositAdder accountAndDepositAdder)
        {
            _dbContext = dbContext;
            _moneySender = moneySender;
            _converter = converter;
            _accountAndDepositAdder = accountAndDepositAdder;   
        }

        [HttpPost]
        public IActionResult AddAccount([FromBody] AccountDto accountDto)
        {
            _accountAndDepositAdder.AddAccountRecord(
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
            _accountAndDepositAdder.AddAccountRecord(
                depositDto.UserId, 
                depositDto.Iban, 
                depositDto.Currency, 
                depositDto.IsDeposit, 
                depositDto.WithdrawDate);

            return Ok();
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