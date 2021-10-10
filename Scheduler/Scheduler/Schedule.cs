using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class Schedule
    {
        public Schedule(Type typeOfSchedule, DateTime? startDate, DateTime? endDate)
        {
            this.TypeOfSchedule = typeOfSchedule;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? NextDate { get; set; }
        public Type TypeOfSchedule { get; set; }
    }
}
