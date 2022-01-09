using Scheduler.Auxiliar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler
{
    public static class Scheduler
    {
        private static DayOfWeek[] weekDays = { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };
        private static DayOfWeek[] weekendDays = { DayOfWeek.Saturday, DayOfWeek.Sunday };

        public static DateDescription GetNextDateOutput(Configuration configuration)
        {
            ValidateGeneralData(configuration);

            return GetNextDate(configuration);
        }

        private static DateDescription GetNextDate(Configuration configuration)
        {
            DateTime FirstDay = configuration.StartDate.HasValue == false || configuration.StartDate.Value < configuration.CurrentDate.Day
                                ? configuration.CurrentDate.Day
                                : configuration.StartDate.Value;

            switch(configuration.Frecuency)
            {
                case Frecuency.Once:
                    return GetNextDateOnceMessage(configuration);
                case Frecuency.Daily:
                    return GetNextDateDaily(configuration, FirstDay);
                case Frecuency.Weekly:
                    return GetNextDateWeekly(configuration, FirstDay);
                case Frecuency.Monthly:
                    return GetNextDateMonthly(configuration, FirstDay);
                default:
                    throw new ArgumentNullException("Frecuency", ExceptionMessages.FrecuencyNull);
            }
        }

        private static DateDescription GetNextDateMonthly(Configuration configuration, DateTime firstDay)
        {
            switch(configuration.MonthlyFrecuencyType)
            {
                case TypeOfMonthlyFrecuency.DayOfMonth:
                    return GetNextDayMonthlyWithDayOfMonth(configuration, firstDay);
                case TypeOfMonthlyFrecuency.DayOfWeek:
                    return GetNextDayMonthlyWithDayOfWeek(configuration, firstDay);
                default:
                    throw new ArgumentNullException("Type of monthly frecuency", ExceptionMessages.MonthlyFrecuencyTypeNull);
            }
        }

        private static DateDescription GetNextDayMonthlyWithDayOfWeek(Configuration configuration, DateTime firstDay)
        {
            //Func<Configuration, DateTime, bool> DayMatchesWithConditions = (conf, day) => DayOfWeekMonthlyMatches(conf.DaysOfWeekMonthly, day);
            Date NextDate = null;
            TimeSpan hourDate = configuration.DailyMode == Mode.Once ? configuration.HourOnce : configuration.StartHour.Value;
            //if(NextDate == null)
            //{
            var firstDayOfMonth = new DateTime(firstDay.Year, firstDay.Month, 1);
            DateTime nextDay = GetNextDateInMonth(configuration, new DateTime(firstDay.Year, firstDay.Month, 1));
            //si se trata del mismo día se comprueban las horas, si la hora actual es posterior, se suma el intervalo de meses
            if (nextDay == firstDay)
            {
                NextDate = GetNextDateCurrentOrStartDay(configuration, firstDay, (conf, day) => true);
                if(NextDate == null)
                {
                    NextDate = new Date(GetNextDateInMonth(configuration, firstDayOfMonth.AddMonths(configuration.MonthInterval)), hourDate);
                }
            }
            else
            {
                NextDate = new Date(nextDay, hourDate);
            }
            //si es menor que el primer día se pasa al mes siguiente
            if (NextDate == null || nextDay < firstDay)
            {
                NextDate = new Date(GetNextDateInMonth(configuration, firstDayOfMonth.AddMonths(1)), hourDate);
            }
                       
            string message = OutputMessages.GetMessage(configuration, NextDate);
            return new DateDescription(NextDate, message);
        }

        private static DateTime GetNextDateInMonth(Configuration configuration, DateTime dayOfMonth)
        {
            int index = 0;
            var nextDay = new DateTime();
            int month = dayOfMonth.Month;
            while (month == dayOfMonth.Month)
            {
                if (DayOfWeekMonthlyMatches(configuration.DaysOfWeekMonthly, dayOfMonth))
                {
                    index++;
                    if (DayOfMonthMatchesWithFrecuency(configuration.MonthlyFrecuency, index))
                    {
                        nextDay = dayOfMonth;
                    }
                }
                
                dayOfMonth = dayOfMonth.AddDays(1);
            }
            return nextDay;
        }

        private static bool DayOfWeekMonthlyMatches(DaysOfWeekMonthly daysOfWeekMonthly, DateTime dayOfMonth)
        {
            switch(daysOfWeekMonthly)
            {
                case DaysOfWeekMonthly.Monday:
                    return dayOfMonth.DayOfWeek == DayOfWeek.Monday;
                case DaysOfWeekMonthly.Tuesday:
                    return dayOfMonth.DayOfWeek == DayOfWeek.Tuesday;
                case DaysOfWeekMonthly.Wednesday:
                    return dayOfMonth.DayOfWeek == DayOfWeek.Wednesday;
                case DaysOfWeekMonthly.Thursday:
                    return dayOfMonth.DayOfWeek == DayOfWeek.Thursday;
                case DaysOfWeekMonthly.Friday:
                    return dayOfMonth.DayOfWeek == DayOfWeek.Friday;
                case DaysOfWeekMonthly.Saturday:
                    return dayOfMonth.DayOfWeek == DayOfWeek.Saturday;
                case DaysOfWeekMonthly.Sunday:
                    return dayOfMonth.DayOfWeek == DayOfWeek.Sunday;
                case DaysOfWeekMonthly.Weekday:
                    return weekDays.Contains(dayOfMonth.DayOfWeek);
                case DaysOfWeekMonthly.WeekendDay:
                    return weekendDays.Contains(dayOfMonth.DayOfWeek);
                case DaysOfWeekMonthly.Day:
                    return true;
                default:
                    throw new ArgumentException(ExceptionMessages.DaysOfWeekMonthlyNullOfNotRecognized, "Days of week");
            }
        }

        private static bool DayOfMonthMatchesWithFrecuency(MonthlyFrecuency monthlyFrecuency, int numberOfWeekDayInMonth)
        {
            switch(monthlyFrecuency)
            {
                case MonthlyFrecuency.First:
                    return numberOfWeekDayInMonth == 1;
                case MonthlyFrecuency.Second:
                    return numberOfWeekDayInMonth == 2;
                case MonthlyFrecuency.Third:
                    return numberOfWeekDayInMonth == 3;
                case MonthlyFrecuency.Fourth:
                    return numberOfWeekDayInMonth == 4;
                case MonthlyFrecuency.Last:
                    return numberOfWeekDayInMonth == 4
                        || numberOfWeekDayInMonth == 5;
                default:
                    throw new ArgumentException("You must indicate monthly frecuency: first, second, third, forth or last.", "Monthly frecuency");
            }
        }

        private static DateDescription GetNextDayMonthlyWithDayOfMonth(Configuration configuration, DateTime firstDay)
        {
            Func<Configuration, DateTime, bool> DayMatchesWithConditions = (conf, day) => day.Day == conf.DayOfMonth;
            Date NextDate = GetNextDateCurrentOrStartDay(configuration, firstDay, (conf, day) => DayMatchesWithConditions(conf,day));
            if(NextDate == null)
            {
                DateTime NextDay;
                if (firstDay.Day < configuration.DayOfMonth)
                {
                    NextDay = new DateTime(firstDay.Year, firstDay.Month, configuration.DayOfMonth);
                }
                else
                {
                    DateTime nextMonthDate = firstDay.AddMonths(1);
                    NextDay = new DateTime(nextMonthDate.Year, nextMonthDate.Month, configuration.DayOfMonth);
                }

                TimeSpan HourOfDay = configuration.DailyMode == Mode.Once
                                     ? configuration.HourOnce
                                     : configuration.StartHour.Value;

                NextDate = new Date(NextDay, HourOfDay);
            }
            string message = OutputMessages.GetMessage(configuration, NextDate);
            return GetDateDescription(configuration, message, NextDate);
        }

        private static Date GetNextDateInCurrentDay(Configuration configuration, DateTime firstDay)
        {
            if(firstDay != configuration.CurrentDate.Day)
            {
                return null;
            }
            if(configuration.DailyMode == Mode.Once
                && configuration.CurrentDate.Hour < configuration.HourOnce)
            {
                return new Date(firstDay, configuration.HourOnce);
            }
            else if(configuration.DailyMode == Mode.Recurring                
                && configuration.CurrentDate.Hour < configuration.EndHour)
            {
                TimeSpan StartHour = configuration.StartHour ?? TimeSpan.Zero;
                TimeSpan EndHour = configuration.EndHour ?? TimeSpan.MaxValue;

                TimeSpan IntervalOfTime = GetIntervalOfTime(configuration);

                TimeSpan? HourOfDay = GetNextHourInCurrentDay(StartHour, EndHour, configuration.CurrentDate, IntervalOfTime);

                if(HourOfDay.HasValue)
                {
                    return new Date(firstDay, HourOfDay.Value);
                }
            }
            return null;
        }

        private static DateDescription GetNextDateDaily(Configuration configuration, DateTime firstDate)
        {            
            if (configuration.NumberOfDays.HasValue == false)
            {
                throw new ArgumentNullException("Number of days", ExceptionMessages.DailyIntervalNull);
            }
            if (configuration.DailyMode == Mode.Once)
            {
                return GetNextDateDailyOnceMessage(configuration, firstDate);
            }
            return GetNextDateDailyWithDailyFrecuencyMessage(configuration, firstDate);
        }

        private static DateDescription GetNextDateDailyOnceMessage(Configuration configuration, DateTime firstDay)
        {
            Date NextDate = GetNextDateCurrentOrStartDay(configuration, firstDay, (conf, day) => true);
            if (NextDate == null)
            {
                NextDate = new Date(firstDay.AddDays(configuration.NumberOfDays.Value), configuration.HourOnce);
            }

            string message = OutputMessages.GetMessage(configuration, NextDate);
            return GetDateDescription(configuration, message, NextDate);
        }

        private static Date GetNextDateCurrentOrStartDay(Configuration configuration, DateTime firstDay, Func<Configuration, DateTime, bool> condition)
        {
            Date nextDate = GetNextDateInCurrentDay(configuration, firstDay);
            if (nextDate != null && condition(configuration, nextDate.Day)) return nextDate;
            if (configuration.StartDate.HasValue && firstDay == configuration.StartDate)
            {
                nextDate = new Date(firstDay, configuration.DailyMode == Mode.Once ? configuration.HourOnce : configuration.StartHour ?? TimeSpan.MinValue);
            }
            if (nextDate != null && condition(configuration, nextDate.Day))
            {
                return nextDate;
            }
            else
            {
                return null;
            }
        }

        private static DateDescription GetNextDateDailyWithDailyFrecuencyMessage(Configuration configuration, DateTime firstDay)
        {
            Date NextDate = GetNextDateCurrentOrStartDay(configuration, firstDay, (conf, day) => true);

            if (NextDate == null)
            {
                NextDate = new Date(firstDay.AddDays(configuration.NumberOfDays.Value), configuration.StartHour);
            }
            
            string message = OutputMessages.GetMessage(configuration, NextDate);
            return GetDateDescription(configuration, message, NextDate);
        }

        private static TimeSpan? GetNextHourInCurrentDay(TimeSpan startHour, TimeSpan endHour, Date currentDate, TimeSpan intervalOfTime)
        {
            TimeSpan? NextDateHourCurrentDate = null;
            TimeSpan Hour = startHour;
            while (Hour <= endHour)
            {
                if (Hour <= currentDate.Hour)
                {
                    Hour = Hour.Add(intervalOfTime);
                }
                else
                {
                    NextDateHourCurrentDate = Hour;
                    break;
                }
            }
            return NextDateHourCurrentDate;
        }

        private static void ValidateDailyFrecuency(Configuration configuration)
        {         
            if(configuration.DailyMode == Mode.Recurring)
            {
                switch (configuration.DailyFrecuency)
                {
                    case DailyFrecuency.Hour:
                        if (configuration.HourInterval is null)
                        {
                            throw new ArgumentNullException("Hour interval", ExceptionMessages.TimeIntervalNull);
                        }
                        if (configuration.HourInterval.Value < 1 || configuration.HourInterval.Value >= 24)
                        {
                            throw new ArgumentOutOfRangeException("Hour interval", ExceptionMessages.HourIntervalOutOfRange);
                        }
                        break;
                    case DailyFrecuency.Minute:
                        if (configuration.MinuteInterval is null)
                        {
                            throw new ArgumentNullException("Minute interval", ExceptionMessages.TimeIntervalNull);
                        }
                        if (configuration.MinuteInterval.Value < 1 || configuration.MinuteInterval.Value >= 1440)
                        {
                            throw new ArgumentOutOfRangeException("Minute interval", ExceptionMessages.MinuteIntervalOutOfRange);
                        }
                        break;
                    case DailyFrecuency.Second:
                        if (configuration.SecondInterval is null)
                        {
                            throw new ArgumentNullException("Second interval", ExceptionMessages.TimeIntervalNull);
                        }
                        if (configuration.SecondInterval.Value < 1 || configuration.SecondInterval.Value >= 86400)
                        {
                            throw new ArgumentOutOfRangeException("Second interval", ExceptionMessages.SecondIntervalOutOfRange);
                        }
                        break;
                }
            }          
        }

        private static TimeSpan GetIntervalOfTime(Configuration configuration)
        {
            var interval = new TimeSpan();
            switch(configuration.DailyFrecuency)
            {
                case DailyFrecuency.Hour:
                    interval = new TimeSpan(configuration.HourInterval.Value, 0, 0);
                    break;
                case DailyFrecuency.Minute:
                    interval = new TimeSpan(0, configuration.MinuteInterval.Value, 0);
                    break;
                case DailyFrecuency.Second:
                    interval = new TimeSpan(0, 0, configuration.SecondInterval.Value);
                    break;
            }
            return interval;
        }

        private static DateDescription GetNextDateOnceMessage(Configuration configuration)
        {
            if (configuration.DateOnce is null)
            {
                throw new ArgumentNullException("Configuration datetime", ExceptionMessages.ConfigurationDateTimeNull);
            }

            string message = OutputMessages.GetMessage(configuration, configuration.DateOnce);         
            return GetDateDescription(configuration, message, configuration.DateOnce);
        }

        private static bool ValidateDateLimits(Date currentDate, Date nextDate, DateTime? startDate, DateTime? endDate)
        {
            DateTime StartDate = startDate ?? DateTime.MinValue;
            DateTime EndDate = endDate ?? DateTime.MaxValue;

            if (nextDate < currentDate
               || (nextDate.Day < StartDate || nextDate.Day > EndDate))
            {
                return false;
            }
            return true;
        }

        private static DateDescription GetDateDescription(Configuration configuration, string message, Date nextDate)
        {
            if (ValidateDateLimits(configuration.CurrentDate, nextDate, configuration.StartDate, configuration.EndDate))
            {
                return new DateDescription(nextDate, message);
            }
            return null;
        }

        private static void ValidateGeneralData(Configuration configuration)
        {
            Validator.ValidateLimitDates(configuration.StartDate, configuration.EndDate);
            Validator.ValidateLimitHours(configuration.StartHour, configuration.EndHour);
            ValidateDailyFrecuency(configuration);
        }

        private static DateDescription GetNextDateWeekly(Configuration configuration, DateTime firstDate)
        {
            if (configuration.WeekInterval.HasValue == false)
            {
                throw new ArgumentNullException(ExceptionMessages.WeeklyIntervalNull);
            }

            if (configuration.DailyMode == Mode.Once)
            {
                return GetNextDateWeeklyOnceMessage(configuration, firstDate);
            }
            return GetNextDateWeeklyWithDailyFrecuencyMessage(configuration, firstDate);
        }

        private static Date GetNextDateWeeklyWithDailyFrecuency(Configuration configuration, DateTime firstDay)
        {
            Date NextDate;

            NextDate = GetNextDateCurrentOrStartDay(configuration, firstDay, (conf, day) => conf.DaysOfWeek.Contains(day.DayOfWeek));
            if (NextDate != null)
            {
                return NextDate;
            }

            //Comprobamos si dentro de esta semana se encuentra el siguiente día
            Week Week = configuration.CurrentDate.Day.GetWeek();
            DateTime? NextDay = GetNextDayInWeek(Week, firstDay, configuration.DaysOfWeek);
            if (NextDay != null)
            {
                return new Date(NextDay.Value, configuration.StartHour);
            }

            //Si en esta semana no coincide ningún día, se busca en la semana siguiente
            Week FollowingWeek = Week.GetNextWeek(configuration.WeekInterval.Value);
            NextDay = GetNextDayInWeek(FollowingWeek, FollowingWeek.Sunday, configuration.DaysOfWeek);
            return new Date(NextDay.Value, configuration.StartHour);
        }

        private static DateDescription GetNextDateWeeklyWithDailyFrecuencyMessage(Configuration configuration, DateTime firstDay)
        {
            Date NextDate = GetNextDateWeeklyWithDailyFrecuency(configuration, firstDay);
            string message = OutputMessages.GetMessage(configuration, NextDate);
            return GetDateDescription(configuration, message, NextDate);
        }

        private static DateTime? GetNextDayInWeek(Week week, DateTime date, DayOfWeek[] daysOfWeek)
        {
            foreach (DateTime eachDay in week.Days)
            {
                if (eachDay > date && daysOfWeek.Contains(eachDay.DayOfWeek))
                {
                    return eachDay;
                }
            }
            return null;
        }

        private static DateDescription GetNextDateWeeklyOnceMessage(Configuration configuration, DateTime firstDate)
        {
            Date NextDate = null;
            NextDate = GetNextDateCurrentOrStartDay(configuration, firstDate, (conf, day) => conf.DaysOfWeek.Contains(day.DayOfWeek));

            if (NextDate is null)
            {
                Week Week = firstDate.GetWeek();
                DateTime? NextDay = GetNextDayInWeek(Week, firstDate, configuration.DaysOfWeek);
                if (NextDay is null)
                {
                    Week = Week.GetNextWeek(configuration.WeekInterval.Value);
                    NextDay = GetNextDayInWeek(Week, Week.Sunday, configuration.DaysOfWeek);
                }

                NextDate = new Date(NextDay.Value, configuration.HourOnce);
            }
            var message = OutputMessages.GetMessage(configuration, NextDate);
            return GetDateDescription(configuration, message, NextDate);
        }
    }
}
