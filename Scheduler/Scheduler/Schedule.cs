using Scheduler.Auxiliar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scheduler
{
    public class Schedule
    {
        private TimeSpan[] hoursInDay;
        public Schedule(Configuration configuration)
        {
            this.ValidateData(configuration);
            this.SetData(configuration);
        }
        public Date StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public TimeSpan StartHour { get; private set; }
        public TimeSpan EndHour { get; private set; }
        public DateTime? DateOnce { get; private set; }
        public TimeSpan? HourOnce { get; private set; }
        public Date CurrentDate { get; private set; }
        public Mode TypeOfSchedule { get; private set; }
        public Frecuency Frecuency { get; private set; }
        public DayOfWeek[] DaysOfWeek { get; private set; }
        public int? WeeksOfInterval { get; private set; }
        public int? DaysOfInterval { get; private set; }
        public int? HoursOfInterval { get; private set; }
        private Date NextDate { get; set; }

        private void ValidateData(Configuration configuration)
        {
            this.ValidateTypeOfSchedule(configuration.TypeOfSchedule);
            this.ValidateCurrentDate(configuration.CurrentDate);
            Validator.ValidateLimitDates(configuration.StartDate, configuration.EndDate);
            Validator.ValidateLimitHours(configuration.StartHour, configuration.EndHour);
            if(configuration.TypeOfSchedule == Mode.Recurring)
            {
                this.ValidateRecurringData(configuration);
            }
            else
            {
                this.ValidateOnceData(configuration);
            }
            this.ValidateHoursOfInterval(configuration.HourInterval);
        }

        private void ValidateOnceData(Configuration configuration)
        {
            if(configuration.DateOnce < configuration.CurrentDate.Day
                || (configuration.DateOnce.Equals(configuration.CurrentDate.Day)
                    && configuration.HourOnce < configuration.CurrentDate.Hour))
            {
                throw new Exception(ExceptionMessages.DateTimeBeforeCurrentDate);
            }
        }

        private void ValidateHoursOfInterval(int? hoursOfInterval)
        {
            if (hoursOfInterval.HasValue && (hoursOfInterval < 1 || hoursOfInterval >= 24))
            {
                throw new Exception(ExceptionMessages.InvalidHourInterval);
            }
        }

        private void ValidateRecurringData(Configuration configuration)
        {
            if (configuration.Frecuency == null)
            {
                throw new Exception(ExceptionMessages.FrecuencyEmpty);
            }

            if(configuration.Frecuency == Frecuency.Daily)
            {
                if (configuration.NumberOfDays == null)
                {
                    throw new Exception(ExceptionMessages.DaysIntervalEmpty);
                }         
            }
            else if(configuration.Frecuency == Frecuency.Weekly)
            {
                if(configuration.WeekInterval == null)
                {
                    throw new Exception(ExceptionMessages.WeekIntervalEmpty);
                }
            }
        }

        private void ValidateTypeOfSchedule(Mode? typeOfSchedule)
        {
            if (typeOfSchedule == null)
            {
                throw new Exception(ExceptionMessages.TypeOfScheduleEmpty);
            }
        }

        private void ValidateCurrentDate(Date currentDate)
        {
            //Si es nulo, se podría considerar DateTime.Now?
            if (currentDate == null)
            {
                throw new Exception(ExceptionMessages.CurrentDateEmpty);
            }
        }

        private void SetData(Configuration configuration)
        {
            this.TypeOfSchedule = configuration.TypeOfSchedule.Value;
            if(this.TypeOfSchedule == Mode.Recurring)
            {
                this.Frecuency = configuration.Frecuency.Value;
            }
            this.CurrentDate = this.NextDate = configuration.CurrentDate;
            this.ConfigureModeOnce(configuration.DateOnce, configuration.HourOnce);
            this.SetLimitDates(configuration.StartDate, configuration.EndDate, configuration.CurrentDate);
            this.SetLimitHours(configuration.StartHour, configuration.EndHour);
            this.HourOnce = configuration.HourOnce;
            this.DaysOfWeek = configuration.DaysOfWeek;
            this.HoursOfInterval = configuration.HourInterval;
            this.DaysOfInterval = configuration.NumberOfDays;
            this.WeeksOfInterval = configuration.WeekInterval;
            this.SetHoursInDay();
        }

        private void SetLimitDates(DateTime? startDate, DateTime? endDate, Date currentDate)
        {
            bool StartDayIsCurrentDay = startDate.HasValue == false || startDate.Value < currentDate.Day;
            if (StartDayIsCurrentDay)
            {
                this.StartDate = new Date(currentDate.Day, currentDate.Hour);
            }
            else
            {
                this.StartDate = new Date(startDate.Value);
            }

            this.EndDate = endDate ?? DateTime.MaxValue;
        }

        private void SetLimitHours(TimeSpan? startHour, TimeSpan? endHour)
        {
            this.StartHour = startHour ?? TimeSpan.Zero;
            this.EndHour = endHour ?? new TimeSpan(23, 59, 59);
        }

        private void SetHoursInDay()
        {
            if(this.HoursOfInterval.HasValue)
            {
                var Hours = new List<TimeSpan>();
                TimeSpan Hour = this.StartHour;
                while (Hour < this.EndHour)
                {
                    Hours.Add(Hour);
                    Hour = new TimeSpan(Hour.Hours + this.HoursOfInterval.Value, 0, 0);
                }
                this.hoursInDay = Hours.ToArray();
            }          
        }

        public Date GetNextDateOnce()
        {
            Date NextDate = new Date(this.DateOnce.Value, this.HourOnce);
            return NextDate;
        }

        public Date GetNextDateDaily()
        {
            Date NextDate;
            if(this.HourOnce.HasValue)
            {
                if((this.NextDate.Hour ?? TimeSpan.Zero) < this.HourOnce.Value)
                {
                    NextDate = new Date(this.NextDate.Day, this.HourOnce);  
                }
                else
                {
                    NextDate = new Date(this.NextDate.Day.AddDays(this.DaysOfInterval.Value), this.HourOnce);
                }
                return NextDate;
            }
            else
            {
                foreach (TimeSpan eachHour in this.hoursInDay)
                {
                    if (this.NextDate.Hour <= eachHour)
                    {
                        this.NextDate = new Date(this.CurrentDate.Day, eachHour);
                        return this.NextDate;
                    }
                }
                return new Date(this.NextDate.Day.AddDays(this.DaysOfInterval.Value), this.StartHour);
            }
        }

        public Date GetNextDateWeekly()
        {
            //Si la siguiente cita es hoy porque la hora es posterior a la actual. Se establece la hora de current date con esta hora
            Date NextDate = this.HourOnce.HasValue ? this.GetNextDateInDayOnce() : this.GetNextDateInDayRecurring();
            if (NextDate != null)
            {
                this.NextDate = NextDate;
                return this.NextDate;
            }

            //Comprobamos si dentro de esta semana se encuentra el siguiente día
            Week Week = this.CurrentDate.Day.GetWeek();
            NextDate = this.GetNextDayInWeek(Week);

            if(NextDate != null)
            {
                this.NextDate = NextDate;
                return this.NextDate;
            }

            //Si en esta semana no coincide ningún día, se busca en la semana siguiente
            Week FollowingWeek = Week.GetNextWeek(this.WeeksOfInterval.Value);
            NextDate = this.GetNextDayInWeek(FollowingWeek);

            if (NextDate != null)
            {
                this.NextDate = NextDate;
                return this.NextDate;
            }
            return null;
        } 

        private Date GetNextDayInWeek(Week week)
        {
            DateTime? NextDateTime = week.Days.FirstOrDefault(D => D >= this.CurrentDate.Day
                                                           && this.DaysOfWeek.Contains(D.DayOfWeek)
                                                           && D <= this.EndDate);            
            if(NextDateTime.HasValue)
            {
                TimeSpan Hour = this.HourOnce ?? this.StartHour;
                return new Date(NextDateTime.Value, Hour);
            }
            return null;
        }

        private Date GetNextDateInDayRecurring()
        {
            if (this.DaysOfWeek.Contains(this.NextDate.Day.DayOfWeek))
            {
                foreach (TimeSpan eachHour in this.hoursInDay)
                {             
                    if (this.NextDate.Hour <= eachHour)
                    {
                        this.NextDate = new Date(this.CurrentDate.Day, eachHour);
                        return this.NextDate;
                    }
                }
            }
            return null;
        }

        private Date GetNextDateInDayOnce()
        {
            if (this.DaysOfWeek.Contains(this.NextDate.Day.DayOfWeek))
            {
                if (this.HourOnce.HasValue && this.NextDate.Hour <= this.HourOnce)
                {
                    this.NextDate = new Date(this.CurrentDate.Day, this.HourOnce);
                    return this.NextDate;
                }
            }
            return null;
        }

        private void ConfigureModeOnce(DateTime? dateTime, TimeSpan? hour)
        {
            if (this.TypeOfSchedule == Mode.Once)
            {
                this.DateOnce = dateTime ?? this.CurrentDate.Day;
            }
            this.HourOnce = hour;
        }
    }
}
