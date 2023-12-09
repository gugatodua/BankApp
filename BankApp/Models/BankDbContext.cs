using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Models
{
    public class BankDbContext : IdentityDbContext<User, Role, string>
    {
        public BankDbContext(DbContextOptions<BankDbContext> options)
            : base(options)
        {

        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Currency> Currencies { get; set; } 
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }  
        public DbSet<BankWallet> BankWallets { get; set;}
        public DbSet<Deposit> Deposits { get; set; }


    }

}
