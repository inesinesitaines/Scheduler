using System;
using Scheduler.Auxiliar;

namespace Scheduler
{
    public class Configuration
    {
        public Configuration()
        { }
        public Date CurrentDate { get; set; }
        public Date DateOnce { get; set; }
        public Frecuency? Frecuency { get; set; }
        public int? NumberOfDays { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Mode DailyMode { get; set; }
        public DailyFrecuency DailyFrecuency { get; set; }
        public TimeSpan HourOnce { get; set; }
        public TimeSpan? StartHour { get; set; }
        public TimeSpan? EndHour { get; set; }
        public int? SecondInterval { get; set; }
        public int? MinuteInterval { get; set; }
        public int? HourInterval { get; set; } 
        public int? WeekInterval { get; set; }
        public DayOfWeek[] DaysOfWeek { get; set; }  
        public MonthlyFrecuency MonthlyFrecuency { get; set;}
        public DaysOfWeekMonthly DaysOfWeekMonthly { get; set; }    
        public int MonthInterval { get; set; }
        public int DayOfMonth { get; set; }
        public TypeOfMonthlyFrecuency MonthlyFrecuencyType { get; set; }

    }
}
