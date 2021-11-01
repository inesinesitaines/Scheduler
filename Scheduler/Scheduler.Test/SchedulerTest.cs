using System;
using FluentAssertions;
using Xunit;
using Scheduler.Auxiliar;

namespace Scheduler.Test
{
    public class SchedulerTest
    {
        [Fact]
        private void Next_date_once_without_date_data_should_be_current_date()
        {
            Configuration conf = new Configuration();
            conf.TypeOfSchedule = Mode.Once;
            Date Date = new Date(new DateTime(2021, 1, 3));
            conf.CurrentDate = Date;

            Schedule schedule = new Schedule(conf);

            schedule.GetNextDateOnce().Should().BeEquivalentTo(Date);
        }

        [Fact]
        private void Schedule_should_throw_exception_when_datetime_in_once_mode_is_smaller_than_current_date()
        {
            Configuration conf = new Configuration();
            conf.TypeOfSchedule = Mode.Once;
            conf.CurrentDate = new Date(new DateTime(2021, 1, 3));
            conf.DateOnce = new DateTime(2021, 1, 2);

            Action act = () => new Schedule(conf);

            act.Should().Throw<Exception>(ExceptionMessages.DateTimeBeforeCurrentDate);
        }

        [Fact]
        private void Schedule_should_throw_exception_when_datetime_in_once_mode_is_smaller_than_current_date_in_time()
        {
            DateTime dateTime = new DateTime(2021, 1, 2);
            TimeSpan timeSpan = new TimeSpan(1, 0, 0);
            Configuration conf = new Configuration();
            conf.TypeOfSchedule = Mode.Once;
            conf.CurrentDate = new Date(dateTime, timeSpan.Add(TimeSpan.FromHours(1)));
            conf.DateOnce = dateTime;
            conf.HourOnce = timeSpan;

            Action act = () => new Schedule(conf);

            act.Should().Throw<Exception>(ExceptionMessages.DateTimeBeforeCurrentDate);
        }

        [Fact]
        private void Schedule_should_return_correct_date_in_daily_frecuency_once_a_day()
        {
            DateTime dateTime = new DateTime(2021, 1, 2);
            Configuration conf = new Configuration();
            conf.TypeOfSchedule = Mode.Recurring;
            conf.Frecuency = Frecuency.Daily;
            conf.CurrentDate = new Date(dateTime, new TimeSpan(7, 0, 0));
            conf.NumberOfDays = 3;
            conf.HourOnce = new TimeSpan(8, 0, 0);

            Date NextDate = new Date(new DateTime(2021, 1, 2), conf.HourOnce);

            Schedule schedule = new Schedule(conf);

            schedule.GetNextDateDaily().Should().BeEquivalentTo(NextDate);
        }

        [Fact]
        private void Next_day_should_be_current_plus_days_of_interval_when_current_hour_bigger_than_hour_once()
        {
            DateTime dateTime = new DateTime(2021, 1, 2);
            Configuration conf = new Configuration();
            conf.TypeOfSchedule = Mode.Recurring;
            conf.Frecuency = Frecuency.Daily;
            conf.CurrentDate = new Date(dateTime, new TimeSpan(9, 0, 0));
            conf.NumberOfDays = 3;
            conf.HourOnce = new TimeSpan(8, 0, 0);

            Date NextDate = new Date(new DateTime(2021, 1, 5), conf.HourOnce);

            Schedule schedule = new Schedule(conf);

            schedule.GetNextDateDaily().Should().BeEquivalentTo(NextDate);
        }

        [Fact]
        private void Next_date_should_be_current_when_day_of_week_match_and_end_hour_is_after_current_hour()
        {
            Configuration conf = new Configuration();
            conf.TypeOfSchedule = Mode.Recurring;
            conf.Frecuency = Frecuency.Weekly;
            conf.StartHour = new TimeSpan(7, 0, 0);
            conf.EndHour = new TimeSpan(13, 0, 0);
            conf.HourInterval = 1;
            conf.CurrentDate = new Date(new DateTime(2021, 1, 5), new TimeSpan(6, 0, 0));
            conf.DaysOfWeek = new[] { DayOfWeek.Tuesday, DayOfWeek.Friday };
            conf.WeekInterval = 2;

            Date NextDate = new Date(new DateTime(2021, 1, 5), new TimeSpan(7, 0, 0));

            Schedule schedule = new Schedule(conf);

            schedule.GetNextDateWeekly().Should().BeEquivalentTo(NextDate);
        }

        [Fact]
        private void Next_date_should_be_following_day_when_current_day_does_not_match_with_day_of_week()
        {
            Configuration conf = new Configuration();
            conf.TypeOfSchedule = Mode.Recurring;
            conf.Frecuency = Frecuency.Weekly;
            conf.StartHour = new TimeSpan(7, 0, 0);
            conf.EndHour = new TimeSpan(13, 0, 0);
            conf.HourInterval = 1;
            conf.CurrentDate = new Date(new DateTime(2021, 1, 4), new TimeSpan(6, 0, 0));
            conf.DaysOfWeek = new[] { DayOfWeek.Tuesday, DayOfWeek.Friday };
            conf.WeekInterval = 2;

            Date NextDate = new Date(new DateTime(2021, 1, 5), new TimeSpan(7, 0, 0));

            Schedule schedule = new Schedule(conf);

            schedule.GetNextDateWeekly().Should().BeEquivalentTo(NextDate);
        }
    }
}
