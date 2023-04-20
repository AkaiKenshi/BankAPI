using ErrorOr;

namespace BankAPI.ServiceErrors;

public class Errors
{
    public class Customer
    {
        public static Error NotFound => Error.NotFound(
         code: "Customer.NotFound",
         description: "Customer Not Found");

        public static Error Unexpected => Error.Unexpected(
            code: "Customer.UnexpectedError",
            description: "There was an unexpected Error");

        public static Error InvalidPassword => Error.Validation(
            code: "Customer.InvalidPassword",
            description: "Incorrect Password"); 

        public static Error InvalidId => Error.Validation(
            code: "Customer.InvalidId",
            description: "The Id you gave is Invalid");

        public static Error IdAlreadyExists => Error.Validation(
            code: "Customer.idValidation",
            description: "a Customer with this id Already exists");

        public static Error UsernameAlredyExists => Error.Validation(
            code: "Customer.UsernameValidation",
            description: "a Customer with this username already exists");

        public static Error EmailAlreadyExists => Error.Validation(
            code: "customer.EmailValidation",
            description: "a Customer with this email already exists"); 

    }

}
