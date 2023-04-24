using ErrorOr;

namespace BankAPI.Services.Time;

public interface ITimeService
{
    Task<ErrorOr<Updated>> UpdateTime(int timeMonths); 
}