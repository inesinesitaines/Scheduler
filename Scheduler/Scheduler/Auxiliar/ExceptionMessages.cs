using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Auxiliar
{
    public static class ExceptionMessages
    {
        public static string DateTimeBeforeCurrentDate
            => "DateTime value should be after the current date.";

        public static string InvalidHourInterval
            => "The number of hours in Daily Frecuency configuration should be between 1 and 23 hours.";

        public static string FrecuencyEmpty
            => "Frecuency can not be empty in recurring schedules.";

        public static string DaysIntervalEmpty
            => "Number of days of interval can not be empty in daily frecuency schedules.";

        public static string WeekIntervalEmpty
            => "Number of weeks of interval can not be empty in weekly frecuency schedules.";

        public static string TypeOfScheduleEmpty
            => "Type of schedule should not be empty.";

        public static string CurrentDateEmpty
            => "Current date should not be empty.";
    }
}
