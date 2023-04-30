using ErrorOr;

namespace BankAPI.ServiceErrors;

public partial class Errors
{
    public class Customer
    {
        public static Error NotFound => Error.NotFound(
         code: "Customer.NotFound",
         description: "Customer Not Found");

        public static Error Unexpected => Error.Unexpected(
            code: "Customer.UnexpectedError",
            description: "There was an unexpected Error");

        public static Error IlligalPassword => Error.Validation(
            code: "Customer.IlligalPassword",
            description: "This is password is not a valid password");

        public static Error IlligalId => Error.Validation(
            code: "Customer.IlligalId",
            description: "This Id is not a valid Id"); 

        public static Error IlligalEmail => Error.Validation(
            code: "Customer.IlligalEmail",
            description: "This Email is not a valid Email");

        public static Error IlligalData => Error.Validation(
            code: "Custorm.IlligalData",
            description: "Some of the information you provided is empty");

        public static Error InvalidPassword => Error.Validation(
            code: "Customer.InvalidPassword",
            description: "Incorrect Password");

        public static Error InvalidLogin => Error.Validation(
            code: "Customer.InvalidLogin",
            description: "Incorrect username or login"); 

        public static Error InvalidId => Error.Validation(
            code: "Customer.InvalidId",
            description: "The Id you gave is Invalid");

        public static Error IdAlreadyExists => Error.Validation(
            code: "Customer.idValidation",
            description: "a Customer with this id Already exists");

        public static Error UsernameAlreadyExists => Error.Validation(
            code: "Customer.UsernameValidation",
            description: "a Customer with this username already exists");

        public static Error EmailAlreadyExists => Error.Validation(
            code: "customer.EmailValidation",
            description: "a Customer with this email already exists"); 

    }

}
