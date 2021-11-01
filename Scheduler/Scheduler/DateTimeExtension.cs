using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    static class DateTimeExtension
    {
        public static Week GetWeek(this DateTime dateTime)
        {
            return new Week(dateTime);
        }
    }
}
