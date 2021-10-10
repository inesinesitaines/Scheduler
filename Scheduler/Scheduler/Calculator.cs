using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public static class Calculator
    {
        public static Schedule CreateSchedule(Configuration configuration)
        {
            Validator.ValidateStartEndDates(configuration.StartDate, configuration.EndDate);

            Schedule schedule = new Schedule(configuration.Type, configuration.StartDate, configuration.EndDate);

            DateTime referenceDate = Calculator.GetReferenceDate(configuration.CurrentDate, configuration.Date);
            
            DateTime? nextDate = null;
            if (Calculator.DateInLimits(referenceDate, configuration.StartDate, configuration.EndDate))
            {
                nextDate = Calculator.GetNextDate(configuration.Type, referenceDate, configuration.NumberOfDays);
            }
            
            schedule.NextDate = nextDate;

            return schedule;
        }

        private static DateTime GetReferenceDate(DateTime currentDate, DateTime? date)
        {
            if(date.HasValue == false || date.Value < currentDate)
            {
                return currentDate;
            }
            return date.Value;
        }

        public static bool DateInLimits(DateTime referenceDate, DateTime? startDate, DateTime? endDate)
        {
            bool validDateStart = startDate.HasValue == false || startDate.Value <= referenceDate;
            bool validDateEnd = endDate.HasValue == false || endDate.Value >= referenceDate;
            if((validDateEnd && validDateStart) == false)
            {
                return false;
            }
            return true;
        }

        private static DateTime GetNextDate(Type type, DateTime referenceDate, int numberOfDays)
        {
            if (type == Type.Once)
            {
                return referenceDate;
            }
            else
            {
                return referenceDate.AddDays(numberOfDays);
            }
        }
    }
}
