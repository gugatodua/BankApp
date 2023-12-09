namespace BankApp.Dtos
{
    public class DepositDto : AccountDto
    {
        public DateTime? WithdrawDate { get; set; }
        public bool IsDeposit { get; set; }
    }
}
