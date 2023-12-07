using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Models
{
    public class Loan
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public string Currency { get; set; }
        public int PaymentDay { get; set; }
        public int TimeLimit { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
