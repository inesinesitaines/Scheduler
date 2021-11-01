using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class Date
    {
        public Date(DateTime day)
        {
            this.Day = day;
        }
        public Date(DateTime day, TimeSpan? hour)
            : this(day)
        {
            this.Hour = hour;
        }

        public DateTime Day { get; set; }
        public TimeSpan? Hour { get; set; }
    }
}
