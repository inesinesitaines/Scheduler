using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Auxiliar
{
    public static class ExceptionMessages
    {
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

        public static string StartDateBiggerThanEndDate
            => "Start date should be smaller than end date.";

        public static string StartHourBiggerThanEndHour
            => "Start hour should be smaller than end hour.";

        public static string ConfigurationDateTimeNull
            => "You must indicate the configuration datetime in once type schedule.";

        public static string TimeIntervalNull
            => "You must indicate the daily frecuency.";

        public static string HourIntervalOutOfRange
            => "Frecuency in hours must be between 1 and 23.";

        public static string MinuteIntervalOutOfRange
            => "Frecuency in minutes must be between 1 and 1439.";

        public static string SecondIntervalOutOfRange
            => "Frecuency in seconds must be between 1 and 86399.";

        public static string WeeklyIntervalNull
            => "You must indicate the number of weeks in weekly configuration.";
    }
}
