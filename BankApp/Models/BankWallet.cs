using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Models
{
    public class BankWallet 
    {
        public Guid Id { get; set; }
        [ForeignKey("CurrencyId")]
        public virtual Currency? Currency { get; set; }
        public Guid CurrencyId { get; set; }
        public decimal Amount { get; set; }
    }
}