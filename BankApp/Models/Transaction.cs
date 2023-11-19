using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Models
{
    public class Transaction
    {
        public Guid? Id { get; set; }
        public string? Debit {  get; set; }
        public string? Credit { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }   
        public string? Currency {  get; set; }
    }
}
