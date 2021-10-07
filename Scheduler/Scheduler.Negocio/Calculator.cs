using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class Calculator
    {
        private Configuration configuration;

        private DateTime? nextDate { get; set; }
        private DateTime? startDate { get; set; }
        public void CalculateNextDate(Configuration configuration)
        {
            this.configuration = configuration;
            this.startDate = configuration.StartDate;
            DateTime ReferenceDate = GetReferenceDate(configuration.CurrentDate, configuration.Date);

            this.ValidateDateInLimits(ReferenceDate, configuration.StartDate, configuration.EndDate);
            this.SetNextDate(ReferenceDate);
        }

        private static DateTime GetReferenceDate(DateTime currentDate, DateTime? date)
        {
            if(date.HasValue == false || date.Value < currentDate)
            {
                return currentDate;
            }
            return date.Value;
        }

        private void ValidateDateInLimits(DateTime referenceDate, DateTime? startDate, DateTime? endDate)
        {
            bool validDateStart = startDate.HasValue == false || startDate.Value <= referenceDate;
            bool validDateEnd = endDate.HasValue == false || endDate.Value >= referenceDate;
            if((validDateEnd && validDateStart) == false)
            {
                this.nextDate = null;
                return;
            }
        }

        private void SetNextDate(DateTime referenceDate)
        {
            if (configuration.Type == Type.Once)
            {
                this.nextDate = referenceDate;
            }
            else
            {
                this.nextDate = referenceDate.AddDays(configuration.NumberOfDays);
            }
        }
    }
}
