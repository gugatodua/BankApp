namespace BankApp.Domain
{
    public interface IMoneySender
    {
        void SendMoney(string accountFrom, string accountTo, decimal amount);
    }
}
