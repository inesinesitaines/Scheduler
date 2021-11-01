using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Auxiliar
{
    public class OutputMessages
    {
        public string OutputMessage(string frecuency, DateTime nextDate, DateTime? startDate, DateTime? endDate)
        {
            string StartDateStr = startDate.HasValue ? $" starting on {startDate.Value.Date}" : string.Empty;
            string EndDateStr = endDate.HasValue ? $" ending on {endDate.Value.Date}" : string.Empty;

            return string.Format("Occurs {0}. Schedule will be used on {1} at {2}{3}{4}",
                                 frecuency, nextDate.Date, nextDate.TimeOfDay, StartDateStr, EndDateStr);
        }
    }
}
