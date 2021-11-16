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

        public static string DailyIntervalNull
            => "You must indicate the number of days in daily frecuency configuration.";

        public static string WeeklyIntervalNull
            => "You must indicate the number of weeks in weekly configuration.";
    }
}
