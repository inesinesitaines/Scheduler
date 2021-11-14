using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Auxiliar
{
    public enum Frecuency
    {
        Daily = 0,
        Weekly = 1
    }

    public enum Mode
    {
        Once = 0,
        Recurring = 1
    }

    public enum DailyFrecuency
    {
        Hour = 0,
        Minute = 1,
        Second = 2
    }
}
