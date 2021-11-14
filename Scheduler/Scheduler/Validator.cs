using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Auxiliar;

namespace Scheduler
{
    public static class Validator
    {
        public static void ValidateLimitDates(DateTime? startDate, DateTime? endDate)
        {
            if(startDate.HasValue && endDate.HasValue && startDate > endDate)
            {
                throw new InvalidOperationException(ExceptionMessages.StartDateBiggerThanEndDate);
            }
        }

        public static void ValidateLimitHours(TimeSpan? startHour, TimeSpan? endHour)
        {
            if(startHour.HasValue && endHour.HasValue && startHour > endHour)
            {
                throw new InvalidOperationException(ExceptionMessages.StartHourBiggerThanEndHour);
            }
        }

        public static void ValidatePositiveNumber(int number)
        {
            if(number <= 0)
            {
                throw new Exception("This number must be positive.");
            }
        }
    }
}
