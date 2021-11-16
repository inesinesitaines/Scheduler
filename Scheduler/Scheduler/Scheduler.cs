using Scheduler.Auxiliar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler
{
    public static class Scheduler
    {
        public static string GetNextDateOutput(Configuration configuration)
        {
            ValidateGeneralData(configuration);
            
            if(configuration.TypeOfSchedule == Mode.Once)
            {
                return GetNextDateOnceMessage(configuration);
            }
            return GetNextDateRecurring(configuration);
        }

        private static string GetNextDateRecurring(Configuration configuration)
        {
            DateTime FirstDay = configuration.StartDate.HasValue == false || configuration.StartDate.Value < configuration.CurrentDate.Day
                                ? configuration.CurrentDate.Day
                                : configuration.StartDate.Value;

            if (configuration.Frecuency == Frecuency.Daily)
            {
                return GetNextDateDaily(configuration, FirstDay);
            }
            return GetNextDateWeekly(configuration, FirstDay);
        }

        private static string GetNextDateDaily(Configuration configuration, DateTime firstDate)
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
        
        private static string GetNextDateDailyOnceMessage(Configuration configuration, DateTime firstDay)
        {
            //no hay que sumar días si es el primer día
            DateTime NextDay;
            if ((configuration.CurrentDate.Hour ?? TimeSpan.Zero) < configuration.HourOnce
                || (configuration.StartDate.HasValue && firstDay == configuration.StartDate))
            {
                NextDay = firstDay;
            }
            else
            {
                NextDay = firstDay.AddDays(configuration.NumberOfDays.Value);
            }
            var NextDate = new Date(NextDay, configuration.HourOnce);
            return OutputMessages.GetOutputMessageEveryXDaysOnceADay(configuration.NumberOfDays.Value, NextDate, configuration.StartDate, configuration.EndDate);
        }

        private static string GetNextDateDailyWithDailyFrecuencyMessage(Configuration configuration, DateTime firstDay)
        {
            TimeSpan StartHour = configuration.StartHour ?? TimeSpan.Zero;
            TimeSpan EndHour = configuration.EndHour ?? TimeSpan.MaxValue;

            TimeSpan IntervalOfTime = ValidateDailyFrecuency(configuration);

            Date NextDate = null;

            if (firstDay == configuration.CurrentDate.Day)
            {
                NextDate = GetNextDateCurrentDay(StartHour, EndHour, configuration.CurrentDate, IntervalOfTime);
            }
            if(NextDate is null)
            {
                DateTime NextDay = firstDay.AddDays(configuration.NumberOfDays.Value);
                NextDate = new Date(NextDay, StartHour);
            }
            
            string message = OutputMessages.GetOutputMessageEveryXDaysDailyRecurring(configuration.NumberOfDays.Value, NextDate, configuration.StartDate, configuration.EndDate, 
                configuration.StartHour, configuration.EndHour);
            return ValidateDateLimits(configuration.CurrentDate, NextDate, configuration.StartDate, configuration.EndDate, message);
        }

        private static Date GetNextDateCurrentDay(TimeSpan startHour, TimeSpan endHour, Date currentDate, TimeSpan intervalOfTime)
        {
            if (currentDate.Hour < endHour)
            {
                TimeSpan? NextDateHourCurrentDate = GetNextHourInCurrentDay(startHour, endHour, currentDate, intervalOfTime);
                if (NextDateHourCurrentDate.HasValue)
                {
                    return new Date(currentDate.Day, NextDateHourCurrentDate);
                }
            }
            return null;
        }

        private static TimeSpan? GetNextHourInCurrentDay(TimeSpan startHour, TimeSpan endHour, Date currentDate, TimeSpan intervalOfTime)
        {
            TimeSpan? NextDateHourCurrentDate = null;
            TimeSpan Hour = startHour;
            while (Hour < endHour)
            {
                if (Hour < currentDate.Hour)
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

        private static TimeSpan ValidateDailyFrecuency(Configuration configuration)
        {
            TimeSpan IntervalOfTime = new();

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
                    IntervalOfTime = new TimeSpan(configuration.HourInterval.Value, 0, 0);
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
                    IntervalOfTime = new TimeSpan(0,configuration.MinuteInterval.Value , 0);
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
                    IntervalOfTime = new TimeSpan(0, 0, configuration.SecondInterval.Value);
                    break;
            }
            return IntervalOfTime;
        }

        private static string GetNextDateOnceMessage(Configuration configuration)
        {
            if (configuration.DateOnce is null)
            {
                throw new ArgumentNullException("Configuration datetime", ExceptionMessages.ConfigurationDateTimeNull);
            }

            string message = OutputMessages.GetOutputMessage(configuration.TypeOfSchedule.ToString(), configuration.DateOnce, configuration.StartDate, configuration.EndDate);
            return ValidateDateLimits(configuration.CurrentDate, configuration.DateOnce, configuration.StartDate, configuration.EndDate, message);            
        }

        private static string ValidateDateLimits(Date currentDate, Date nextDate, DateTime? startDate, DateTime? endDate, string message)
        {
            DateTime StartDate = startDate ?? DateTime.MinValue;
            DateTime EndDate = endDate ?? DateTime.MaxValue;

            if (nextDate < currentDate
               || (nextDate.Day < StartDate || nextDate.Day > EndDate))
            {
                return string.Empty;
            }
            return message;
        }

        private static void ValidateGeneralData(Configuration configuration)
        {
            ValidateTypeOfSchedule(configuration.TypeOfSchedule);
            Validator.ValidateLimitDates(configuration.StartDate, configuration.EndDate);
            Validator.ValidateLimitHours(configuration.StartHour, configuration.EndHour);
        }

        private static void ValidateTypeOfSchedule(Mode? typeOfSchedule)
        {
            if (typeOfSchedule == null)
            {
                throw new ArgumentNullException("Type of schedule", ExceptionMessages.TypeOfScheduleEmpty);
            }
        }

        private static string GetNextDateWeekly(Configuration configuration, DateTime firstDate)
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
            TimeSpan StartHour = configuration.StartHour ?? TimeSpan.Zero;
            TimeSpan EndHour = configuration.EndHour ?? TimeSpan.MaxValue;

            TimeSpan IntervalOfTime = ValidateDailyFrecuency(configuration);
            Date NextDate;

            if (firstDay == configuration.CurrentDate.Day && configuration.DaysOfWeek.Contains(firstDay.DayOfWeek))
            {
                NextDate = GetNextDateCurrentDay(StartHour, EndHour, configuration.CurrentDate, IntervalOfTime);
                if (NextDate != null)
                {
                    return NextDate;
                }
            }
            //Comprobamos si dentro de esta semana se encuentra el siguiente día
            Week Week = configuration.CurrentDate.Day.GetWeek();
            DateTime? NextDay = GetNextDayInWeek(Week, firstDay, configuration.DaysOfWeek);
            if (NextDay != null)
            {
                return new Date(NextDay.Value, StartHour);
            }

            //Si en esta semana no coincide ningún día, se busca en la semana siguiente
            Week FollowingWeek = Week.GetNextWeek(configuration.WeekInterval.Value);
            NextDay = GetNextDayInWeek(FollowingWeek, FollowingWeek.Sunday, configuration.DaysOfWeek);
            return new Date(NextDay.Value, StartHour);
        }

        private static string GetNextDateWeeklyWithDailyFrecuencyMessage(Configuration configuration, DateTime firstDay)
        {
            Date NextDate = GetNextDateWeeklyWithDailyFrecuency(configuration, firstDay);
            string message = OutputMessages.GetOutPutMessageWeeklyFrecuencyDailyRecurring(configuration.WeekInterval.Value, NextDate,
                configuration.StartDate, configuration.EndDate, configuration.StartHour, configuration.EndHour);
            return ValidateDateLimits(configuration.CurrentDate, NextDate, configuration.StartDate, configuration.EndDate, message);
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

        private static string GetNextDateWeeklyOnceMessage(Configuration configuration, DateTime firstDate)
        {
            Date NextDate = null;
            if (configuration.DaysOfWeek.Contains(firstDate.DayOfWeek))
            {
                if ((firstDate == configuration.CurrentDate.Day && configuration.CurrentDate.Hour <= configuration.HourOnce)
                  || firstDate != configuration.CurrentDate.Day)
                {
                    NextDate = new Date(firstDate, configuration.HourOnce);
                }
            }
            if(NextDate is null)
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
            var message = OutputMessages.GetOutPutMessageWeeklyFrecuencyOnceADay(configuration.WeekInterval.Value, NextDate, configuration.StartDate, configuration.EndDate);
            return ValidateDateLimits(configuration.CurrentDate, NextDate, configuration.StartDate, configuration.EndDate, message);
        }
    }
}
