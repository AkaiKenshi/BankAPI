using ErrorOr;

namespace BankAPI.ServiceErrors;

public partial class Errors
{
    public class Time
    {
        public static Error NotFound => Error.NotFound(
         code: "Time.NotFound",
         description: "Time Not Found");

        public static Error InvalidTime => Error.Validation(
            code: "Time.Validation",
            description: "Invalid Time, Time must be a positive non zero number");
    }
}
