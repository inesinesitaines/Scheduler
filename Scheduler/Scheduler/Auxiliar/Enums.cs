using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Auxiliar
{
    public enum Frecuency
    {
        Once,
        Daily,
        Weekly,
        Monthly
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

    public enum MonthlyFrecuency
    {
        First = 1,
        Second = 2,
        Third = 3, 
        Fourth = 4, 
        Last = 5
    }

    public enum DaysOfWeekMonthly
    {
        Monday, 
        Tuesday, 
        Wednesday, 
        Thursday, 
        Friday, 
        Saturday, 
        Sunday, 
        Day, 
        Weekday, 
        WeekendDay
    }

    public enum TypeOfMonthlyFrecuency
    {
        DayOfMonth,
        DayOfWeek
    }
}
