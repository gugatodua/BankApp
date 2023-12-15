namespace BankApp.Domain
{
    public interface IMoneySender
    {
        void SendMoney(string accountFrom, string accountTo, decimal amount);
        void SendMoneyInternal(string accountFrom, string accountTo, decimal amount);
        void DepositMoney(decimal amount, string iban, string currency);
        void WithdrawFromDepositAccount(string accountFrom, string accountTo, decimal amount);
        public void DepositToAccount(string accountFrom, string accountTo, decimal amount);
    }
}
