using System;
using Scheduler.Auxiliar;

namespace Scheduler
{
    public class Configuration
    {
        public Configuration()
        { }
        public Date CurrentDate { get; set; }
        public DateTime? DateOnce  { get; set; }
        public Mode? TypeOfSchedule { get; set; }
        public Frecuency? Frecuency { get; set; }
        public int? NumberOfDays { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Mode DailyMode { get; set; }
        public TimeSpan? HourOnce { get; set; }
        public TimeSpan StartHour { get; set; }
        public TimeSpan EndHour { get; set; }
        public int? HourInterval { get; set; } 
        public int? WeekInterval { get; set; }
        public DayOfWeek[] DaysOfWeek { get; set; }

        //public void SetCurrentDate(string currentDate) => this.CurrentDate = Validator.ValidateDateTime(currentDate);
        public void SetDate(string date) => this.DateOnce = Validator.ValidateDateTimeNullable(date);
        public void SetNumberOfDays(string number) => this.NumberOfDays = Validator.ValidateIntNumber(number);
        public void SetStartDate(string startDate) => this.StartDate = Validator.ValidateDateTimeNullable(startDate);
        public void SetEndDate(string endDate) => this.EndDate = Validator.ValidateDateTimeNullable(endDate);
        public void SetType(string type) => this.TypeOfSchedule = Validator.ValidateType(type);
        public void SetFrecuency(string frecuency) => this.Frecuency = Validator.ValidateFrecuency(frecuency);
        public void SetHourOne(string hourOnce) => this.HourOnce = Validator.ValidateTimeSpan(hourOnce);
        public void SetStartHour(string startHour) => this.StartHour = Validator.ValidateTimeSpan(startHour);
        public void SetEndHour(string endHour) => this.EndHour = Validator.ValidateTimeSpan(endHour);
    }
}
