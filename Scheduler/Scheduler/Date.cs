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

        public static bool operator >(Date date1, Date date2)
        {
            if(date1.Day == date2.Day)
            {
                return date1.Hour > date2.Hour;
            }
            return date1.Day > date2.Day;

        }

        public static bool operator <(Date date1, Date date2)
        {
            if (date1.Day == date2.Day)
            {
                return date1.Hour < date2.Hour;
            }
            return date1.Day < date2.Day;
        }
    }
}
