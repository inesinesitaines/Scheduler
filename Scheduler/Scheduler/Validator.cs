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
    }
}
