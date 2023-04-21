namespace BankAPI.Data.Model
{
    public class Customer
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!; 
        public string Password { get; set; } = null!;

        public ICollection<Account> Accounts { get; set; } = null!;

    }
}
