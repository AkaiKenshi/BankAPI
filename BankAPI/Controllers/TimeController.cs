using BankAPI.Services.Time;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    public class TimeController : ApiController
    {
        private readonly ITimeService _timeService;

        public TimeController(ITimeService timeService)
        {
            _timeService = timeService;
        }

        [HttpPut("passTime/{time}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PassTime(int time)
        {
            var updatePassTime = await _timeService.UpdateTime(time);

            return updatePassTime.Match(
                time => NoContent(),
                errors => Problem(errors));
        }
    }
}
