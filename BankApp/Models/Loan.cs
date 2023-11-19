using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Models
{
    public class Loan
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDay { get; set; }
        public decimal Rate { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
