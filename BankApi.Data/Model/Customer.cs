using System.ComponentModel.DataAnnotations;

namespace BankAPI.Data.Model
{
    public class Customer
    {
        [MaxLength(10)]
        public string Id { get; set; } = null!;
        [MaxLength(50)]
        public string Username { get; set; } = null!;
        [MaxLength (50)]    
        public string FirstName { get; set; } = null!;
        [MaxLength(50)] 
        public string LastName { get; set; } = null!;
        [MaxLength(50)] 
        public string Email { get; set; } = null!; 
        [MaxLength(50)] 
        public string Password { get; set; } = null!;

        public ICollection<Account> Accounts { get; set; } = null!;

    }
}
