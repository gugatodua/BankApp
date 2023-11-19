using Microsoft.AspNetCore.Identity;

namespace BankApp.Models
{
    public class User : IdentityUser
    { 
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public virtual ICollection<Loan> Loans { get; set; } = new HashSet<Loan>();
        public virtual ICollection<Account> Accounts { get; set; } = new HashSet<Account>(); 
    }
}
