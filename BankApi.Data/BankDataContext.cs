
using BankAPI.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Data;

public class BankDataContext : DbContext
{
    public BankDataContext(DbContextOptions<BankDataContext> options) : base(options){}

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Account> Accounts { get; set; }
}
