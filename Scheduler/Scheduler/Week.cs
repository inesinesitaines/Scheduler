using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class Week
    {
        public Week(DateTime date)
        {
            this.SetDays(date);
        }

        public DateTime Monday { get; private set; }
        public DateTime Tuesday { get; private set; }
        public DateTime Wednesday { get; private set; }
        public DateTime Thursday { get; private set; }
        public DateTime Friday { get; private set; }
        public DateTime Saturday { get; private set; }
        public DateTime Sunday { get; private set; }

        public DateTime[] Days { get; private set; }

        private void SetDays(DateTime date)
        {
            this.Sunday = date.AddDays(-(int)date.DayOfWeek);
            this.Monday = date.AddDays(-(int)date.DayOfWeek + 1);
            this.Tuesday = date.AddDays(-(int)date.DayOfWeek + 2);
            this.Wednesday = date.AddDays(-(int)date.DayOfWeek + 3);
            this.Thursday = date.AddDays(-(int)date.DayOfWeek + 4);
            this.Friday = date.AddDays(-(int)date.DayOfWeek + 5);
            this.Saturday = date.AddDays(-(int)date.DayOfWeek + 6);

            this.Days = new DateTime[] { this.Sunday, this.Monday, this.Tuesday, this.Wednesday, this.Thursday, this.Friday, this.Saturday };
        }

        public DateTime GetDay(DayOfWeek dayOfWeek)
        {
            return this.Days.FirstOrDefault(D => D.DayOfWeek == dayOfWeek);
        }

        public Week GetNextWeek(int numberOfWeeks)
        {
            return new Week(this.Monday.AddDays(7 * numberOfWeeks));
        }
    }
}
