using BankAPI.DTOs.Account;
using ErrorOr;

namespace BankAPI.ServiceErrors;

public partial class Errors
{
    public class Account
    {
        public static Error NotFound => Error.NotFound(
            code: "Account.NotFound",
            description: "Account Not Found");
        public static Error AmountValidation => Error.Validation(
            code: "Account.AmountValidation",
            description: "Amount must be a positive number greater than 0");
        public static Error InsufficientFounds => Error.Validation(
            code: "Account.InsufficientFoundsValidation",
            description: "The transfer amount must be less than the account balance");
        public static Error InvalidAction => Error.Validation(
            code: "Account.InvalidAction",
            description: "This account does not support this action");
    }
}
