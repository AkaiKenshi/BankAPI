namespace BankAPI.Model;

public class Account
{
    public string AccountId { get; set; } = string.Empty;
    public double AccountBalance { get; set; }
    public AccountType AccountTypeId { get; set; }
    public string AccountOwnerId { get; set; } = string.Empty;
    public DateOnly AccountCraetedDate { get; set; }
    public int? AccountTerm { get; set; } 
}
