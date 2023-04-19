using ErrorOr;

namespace BankAPI.ServiceErrors;

public class Errors
{
    public class Customer
    {
        public static Error NotFound => Error.NotFound(
         code: "Customer.NotFound",
         description: "Customer Not Found");
        public static Error ConflictID => Error.Validation(
            code: "Customer.Conflict",
            description: "Customer id already exists");

        public static Error ConflictUserName => Error.Validation(
            code: "Customer.Conflict",
            description: "Customer user name already exists");
        public static Error Unexpected => Error.Unexpected(
            code: "Customer.UnexpectedError",
            description: "There was an unexpected Error");
        public static Error InvalidId => Error.Validation(
            code: "Customer.InvalidId",
            description: "The Id you gave is Invalid");
    }
  
}
