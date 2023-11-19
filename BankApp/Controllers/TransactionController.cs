using BankApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers
{
    //შპარგალკა
    //var credit = _dbContext.Accounts.Where(x=>x.Iban == accountFrom).FirstOrDefault();
    //Accounts ცხრილში აიღებს ისეთ ჩანაწერს, სადაც Where-ის პირობა სრულდება. 
    //FistOrDefault აიღებს პირველივე ჩანაწერს რომელიც ამ პირობას აკმაყოფილებს. 
    //Accounts-ში არის ყველა ის ჩანაწერი, რომელიც Accounts ცხრილშია ბაზაში. ამ ჩანაწერებს Where ფილტრავს კონკრეტული პირობის მიხედვით.
    //FirstOrDefault იღებს ამ გაფილტრული ჩანაწერებიდან პირველს. 
    //ToList() რომ იყოს FirstOrDefault-ის მაგივრად, მაშინ მთლიანი სია ჩაიწერებოდა credit-ში. 
    //LINQ-ის რომელიმე მეთოდი თუ არ იცი, დაგუგლე

    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase 
    {
        private readonly BankDbContext _dbContext;
        public TransactionController(BankDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPut("SendMoney")]
        public string SendMoney(string accountFrom, string accountTo, decimal amount)
        {
            var credit = _dbContext.Accounts.Where(x=>x.Iban == accountFrom).FirstOrDefault();
            var debit = _dbContext.Accounts.Where(x=>x.Iban == accountTo).FirstOrDefault();

            credit.Balance -= amount;
            debit.Balance += amount;

            _dbContext.SaveChanges();

            return "ok";
        }

        [HttpPost("AddAccount")]
        public void AddAccount(Account account)
        {
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
        }

        
    }
}
