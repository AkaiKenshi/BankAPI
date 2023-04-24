using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAPI.Data.Model
{
    public class Customer
    {
        [NotMapped]
        public string Token = null!; 
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
        public Byte[] PasswordHash { get; set; } = null!;
        public Byte[] PasswordSalt { get; set; } = null!;

        public ICollection<Account> Accounts { get; set; } = null!;

    }
}
 