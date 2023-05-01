using BankAPI.Data;
using BankAPI.Data.Model;
using BankAPI.ServiceErrors;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services.Time
{
    public class TimeService : ITimeService
    {
        private readonly BankDataContext _context;

        public TimeService(BankDataContext context)
        {
            _context = context;
        }
        public async Task<ErrorOr<Updated>> UpdateTime(int timeMonths)
        {
            var time = await _context.Time.FirstOrDefaultAsync();
            var accounts = await _context.Accounts.ToListAsync();

            if (timeMonths <= 0 ) { return Errors.Time.InvalidTime; }
            if (time == null) { return Errors.Time.NotFound; }
            else if (accounts == null) { return Errors.Account.NotFound; }

            time.CurrentDate = time.CurrentDate.AddMonths(timeMonths);

            for (int i = 0; i < accounts.Count; i++)
            {
                var multiplier = accounts[i].AccountType switch
                {
                    AccountType.Savings => 1.6 * timeMonths,
                    AccountType.Checking => 1,
                    AccountType.FixedTermInvestment => 4.5 * ((timeMonths > accounts[i].Term!) ? (double)accounts[i].Term! : timeMonths),
                    _ => 0
                };

                accounts[i].Balance *= multiplier;

                switch (accounts[i].AccountType)
                {
                    case AccountType.FixedTermInvestment when accounts[i].Term >= timeMonths:
                        accounts[i].Term = null;
                        accounts[i].AccountType = AccountType.Checking;
                        break;
                    case AccountType.FixedTermInvestment:
                        accounts[i].Term -= timeMonths;
                        break;
                }
            }

            _context.SaveChanges();

            return Result.Updated;
        }
    }
}
