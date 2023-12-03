namespace BankApp.Dtos
{
    public class AccountDto
    {
        public string Iban { get; set; }
        public string UserId { get; set; } = null!;

        public string Currency { get; set; }
    }
}
