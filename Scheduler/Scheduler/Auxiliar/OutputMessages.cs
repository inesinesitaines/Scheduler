using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Auxiliar
{
    public static class OutputMessages
    {
        //public string OutputMessage(string frecuency, DateTime nextDate, DateTime? startDate, DateTime? endDate)
        //{
        //    string StartDateStr = startDate.HasValue ? $" starting on {startDate.Value.Date}" : string.Empty;
        //    string EndDateStr = endDate.HasValue ? $" ending on {endDate.Value.Date}" : string.Empty;

        //    return string.Format("Occurs {0}. Schedule will be used on {1} at {2}{3}{4}",
        //                         frecuency, nextDate.Date, nextDate.TimeOfDay, StartDateStr, EndDateStr);
        //}

        public static string GetOutputMessage(string frecuency, Date dateOnce, DateTime? startDate, DateTime? endDate)
        {
            var message = new StringBuilder($"Occurs {frecuency.ToLower()}. Schedule will be used on {dateOnce.Day.ToShortDateString()} at {dateOnce.Hour.Value}");

            message.AppendLimitDates(startDate, endDate);
            message.Append('.');
            return message.ToString();
        }

        public static string GetOutputMessageEveryXDaysOnceADay(int numberOfDays, Date nextDate, DateTime? startDate, DateTime? endDate)
        {
            string frecuencyExpression = GetExpressionFromEveryXDays(numberOfDays);
            var message = new StringBuilder($"Occurs {frecuencyExpression}. Schedule will be used on {nextDate.Day.ToShortDateString()} at {nextDate.Hour}");
            message.AppendLimitDates(startDate, endDate);
            message.Append('.');
            return message.ToString();
        }

        public static string GetOutputMessageEveryXDaysDailyRecurring(int numberOfDays, Date nextDate, DateTime? startDate, DateTime? endDate, TimeSpan? startHour, TimeSpan? endHour)
        {
            string frecuencyExpression = GetExpressionFromEveryXDays(numberOfDays);
            var message = new StringBuilder($"Occurs {frecuencyExpression}. Schedule will be used on {nextDate.Day.ToShortDateString()} at {nextDate.Hour}");
            message.AppendLimitDates(startDate, endDate);
            message.AppendLimitHours(startHour, endHour);
            message.Append('.');
            return message.ToString();
        }

        public static string GetOutPutMessageWeeklyFrecuencyOnceADay(int numberOfWeeks, Date nextDate, DateTime? startDate, DateTime? endDate)
        {
            string frecuencyExpression = GetExpressionFromEveryXWeeks(numberOfWeeks);
            var message = new StringBuilder($"Occurs {frecuencyExpression}. Schedule will be used on {nextDate.Day.ToShortDateString()} at {nextDate.Hour}");
            message.AppendLimitDates(startDate, endDate);
            message.Append('.');
            return message.ToString();
        }

        public static string GetOutPutMessageWeeklyFrecuencyDailyRecurring(int numberOfWeeks, Date nextDate, DateTime? startDate, DateTime? endDate, TimeSpan? startHour, TimeSpan? endHour)
        {
            string frecuencyExpression = GetExpressionFromEveryXWeeks(numberOfWeeks);
            var message = new StringBuilder($"Occurs {frecuencyExpression}. Schedule will be used on {nextDate.Day.ToShortDateString()} at {nextDate.Hour}");
            message.AppendLimitDates(startDate, endDate);
            message.AppendLimitHours(startHour, endHour);
            message.Append('.');
            return message.ToString();
        }

        private static StringBuilder AppendLimitDates(this StringBuilder message, DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue)
            {
                message.Append($" starting on {startDate.Value.ToShortDateString()}");
            }
            if (endDate.HasValue)
            {
                message.Append($" ending on {endDate.Value.ToShortDateString()}");
            }
            return message;
        }

        private static StringBuilder AppendLimitHours(this StringBuilder message, TimeSpan? startHour, TimeSpan? endHour)
        {
            if (startHour.HasValue)
            {
                message.Append($" starting on {startHour.Value}");
            }
            if (endHour.HasValue)
            {
                message.Append($" ending on {endHour.Value}");
            }
            return message;
        }

        private static string GetExpressionFromEveryXDays(int numberOfDays)
        {
            switch(numberOfDays)
            {
                case 1:
                    return "everyday";
                default:
                    return $"every {numberOfDays} days";
            }
        }

        private static string GetExpressionFromEveryXWeeks(int numberOfWeeks)
        {
            switch(numberOfWeeks)
            {
                case 1:
                    return "every week";
                default:
                    return $"every {numberOfWeeks} weeks";
            }
        }
    }
}
