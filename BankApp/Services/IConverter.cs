namespace BankApp.Api.Services
{
    public interface IConverterService
    {
        void Convert(string accountFrom, string accountTo, decimal amount);
    }
}


