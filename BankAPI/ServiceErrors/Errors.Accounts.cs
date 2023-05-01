using BankAPI.Contracts.DTOs.Customers;
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

        public static Error InvalidTargetAccount => Error.Validation(
            code: "Account.InvalidTarget",
            description: "Can't transfer to the same account you're transferring from"
            );

        public static Error UnauthorizedAccountAccess => Error.Custom(
            type: StatusCodes.Status403Forbidden,
            code: "Account.UnauthoizedAccess",
            description: "This account you're trying to access is not yours");
    }
}
