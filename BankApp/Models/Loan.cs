using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Models
{
    public class Loan
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        [ForeignKey("CurrencyId")]
        public virtual Currency? Currency { get; set; }
        public Guid CurrencyId { get; set; }
        public int PaymentDay { get; set; }
        public int TimeLimit { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
