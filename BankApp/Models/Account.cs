using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Iban {  get; set; }
        public decimal Balance { get; set; }
        public string UserId { get; set; } = null!;

        [ForeignKey("UserId")]
        public virtual  User User { get; set; } 

        public string Currency { get; set; }
    }
}