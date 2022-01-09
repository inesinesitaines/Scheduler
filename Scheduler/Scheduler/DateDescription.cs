using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class DateDescription
    {
        public DateDescription(Date date, string description)
        {
            (Date, Description) = (date, description);
        }

        public Date Date { get; private set; }
        public string Description { get; private set; }
    }
}
