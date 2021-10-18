using System;

namespace Scheduler
{
    public class Configuration
    {
        public Configuration()
        { }
        public DateTime CurrentDate { get; private set; }
        public DateTime? Date  { get; private set; }
        public Type Type { get; private set; }
        public Frecuency DayFrecuency { get; private set; }
        public int NumberOfDays { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public TimeSpan StartHour { get; private set; }
        public TimeSpan EndHour { get; private set; }
        public int HourInterval { get; private set; } 

        public void SetCurrentDate(string currentDate) => this.CurrentDate = Validator.ValidateDateTime(currentDate);
        public void SetDate(string date) => this.Date = Validator.ValidateDateTime(date);
        public void SetNumberOfDays(string number) => this.NumberOfDays = Validator.ValidateIntNumber(number);
        public void SetStartDate(string startDate) => this.StartDate = Validator.ValidateDateTime(startDate);
        public void SetEndDate(string endDate) => this.EndDate = Validator.ValidateDateTime(endDate);
        public void SetType(string type) => this.Type = Validator.ValidateType(type);
        public void SetFrecuency(string frecuency) => this.DayFrecuency = Validator.ValidateFrecuency(frecuency);
        public void SetStartHour(string startHour) => this.StartHour = Validator.ValidateTimeSpan(startHour);
        public void SetEndHour(string endHour) => this.EndHour = Validator.ValidateTimeSpan(endHour);
    }
}
