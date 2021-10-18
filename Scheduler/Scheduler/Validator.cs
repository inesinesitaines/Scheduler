using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public static class Validator
    {
        public static void ValidateStartEndDates(DateTime? startDate, DateTime? endDate)
        {
            if(startDate.HasValue && endDate.HasValue && startDate > endDate)
            {
                throw new Exception("Start date should be smaller than the end date.");
            }
        }

        public static DateTime ValidateDateTime(string dateTime)
        {
            DateTime DateTime;
            if(DateTime.TryParse(dateTime, out DateTime) == false)
            {
                throw new Exception("This date has not a valid format. Please, insert with the following format: 'dd/MM/yyyy'.");
            }
            return DateTime;
        }

        public static int ValidateIntNumber(string number)
        {
            int Number;
            if(Int32.TryParse(number, out Number) == false)
            {
                throw new Exception("This is not an integer number.");
            }
            return Number;
        }

        public static void ValidatePositiveNumber(int number)
        {
            if(number < 0)
            {
                throw new Exception("This number must be positive.");
            }
        }

        public static Type ValidateType(string type)
        {
            Type Type;
            if(Enum.TryParse(type, out Type) == false)
            {
                throw new Exception("Type must be 'Once' or 'Recurring'.");
            }
            return Type;
        }

        public static Frecuency ValidateFrecuency(string frecuency)
        {
            Frecuency Frecuency;
            if(Enum.TryParse(frecuency, out Frecuency) == false)
            {
                throw new Exception("Frecuency must be 'Daily'.");
            }
            return Frecuency;
        }

        public static TimeSpan ValidateTimeSpan(string hour)
        {
            TimeSpan TimeSpan;
            if(TimeSpan.TryParse(hour, out TimeSpan) == false)
            {
                throw new Exception("This hour has not a valid format. Please, insert with the following format: 'HH:mm:ss'.");
            }
            return TimeSpan;
        }
    }
}
