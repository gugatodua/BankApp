namespace BankApp.Domain
{
    public interface IConverter
    {
        void Convert(string accountFrom, string accountTo, decimal amount);
    }
}


