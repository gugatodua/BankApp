namespace BankApp.Domain
{
    public  interface IAccountAndDepositAdder
    {
        void AddAccountRecord(
           string userId,
           string iban,
           string currency,
           bool isDeposit = false,
           DateTime? withdrawDate = null);
    }
}
