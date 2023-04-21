
using BankAPI.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Data
{
    internal class BankDataContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Database=bankdb;Port=5432;User Id=postgres;Password=1235789"); 
        }
    }
}
