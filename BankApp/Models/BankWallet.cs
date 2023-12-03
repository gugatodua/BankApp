namespace BankApp.Models
{
    public class BankWallet 
    {
        public Guid Id { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }
    }
}