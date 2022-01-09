using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Auxiliar
{
    public static class OutputMessages
    {
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

        public static string GetMessage(Configuration configuration, Date nextDate)
        {
            var message = new StringBuilder();
            message.Append("Occurs ");
            message.Append(GetExpressionOfFrecuency(configuration));
            message.Append($" Schedule will be used on {nextDate.Day.ToShortDateString()} at {nextDate.Hour}");
            message.AppendLimitDates(configuration.StartDate, configuration.EndDate);
            message.AppendLimitHours(configuration.StartHour, configuration.EndHour);
            message.Append('.');
            return message.ToString();
        }

        private static string GetExpressionOfFrecuency(Configuration configuration)
        {
            switch(configuration.Frecuency)
            {
                case Frecuency.Once:
                    return "once.";
                case Frecuency.Daily:
                    return configuration.NumberOfDays == 1 ? "everyday." : $"every {configuration.NumberOfDays} days.";
                case Frecuency.Weekly:
                    return configuration.WeekInterval == 1 ? "every week." : $"every {configuration.WeekInterval} weeks.";
                case Frecuency.Monthly:
                    return configuration.MonthInterval == 1 ? "every month." : $"every {configuration.MonthInterval} months.";
                default:
                    return string.Empty;
            }
        }
    }
}
