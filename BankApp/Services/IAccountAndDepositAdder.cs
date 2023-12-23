using BankApp.Models;

namespace BankApp.Api.Services

{
    public interface IAccountAndDepositAdder
    {
        void AddAccountRecord(
           string userId,
           string iban,
           Currency currency,
           bool isDeposit = false,
           DateTime? withdrawDate = null);
    }
}
