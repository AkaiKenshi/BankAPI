
using BankAPI.Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace BankAPI.Data;

public class BankDataContext : DbContext
{
    public BankDataContext(DbContextOptions<BankDataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<int>("account_seq")
            .StartsAt(1)
            .IncrementsBy(1)
            .HasMin(1);

        modelBuilder.Entity<Account>()
            .Property(e => e.Id)
            .HasDefaultValueSql("lpad(nextval('account_seq')::VARCHAR(10), 10, '0')")
            .ValueGeneratedOnAdd();
    }


    public DbSet<Customer> Customers { get; set; }
    public DbSet<Account> Accounts { get; set; }


}
