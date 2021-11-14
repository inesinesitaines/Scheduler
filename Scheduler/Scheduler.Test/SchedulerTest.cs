using System;
using FluentAssertions;
using Xunit;
using Scheduler.Auxiliar;
using System.Threading.Tasks;

namespace Scheduler.Test
{
    public class SchedulerTest
    {
        [Fact]
        private void Once_type_schedule_should_return_datetime_config_as_next_date_if_it_is__exactly_current_date()
        {
            var conf = new Configuration();
            conf.TypeOfSchedule = Mode.Once;
            var Date = new Date(new DateTime(2021, 1, 3), new TimeSpan(12, 0, 0));
            conf.CurrentDate = Date;
            conf.DailyMode = Mode.Once;
            conf.DateOnce = new Date(new DateTime(2021, 1, 3), new TimeSpan(12, 0, 0));

            Scheduler.GetNextDateOutput(conf).Should().Be("Occurs once. Schedule will be used on 03/01/2021 at 12:00:00.");
        }

        [Fact]
        private void Once_type_schedule_should_return_datetime_config_as_next_date_if_it_is_after_current_hour()
        {
            var conf = new Configuration();
            conf.TypeOfSchedule = Mode.Once;
            var Date = new Date(new DateTime(2021, 1, 3), new TimeSpan(12, 0, 0));
            conf.CurrentDate = Date;
            conf.DailyMode = Mode.Once;
            conf.DateOnce = new Date(new DateTime(2021, 1, 3), new TimeSpan(13, 0, 0));

            Scheduler.GetNextDateOutput(conf).Should().Be("Occurs once. Schedule will be used on 03/01/2021 at 13:00:00.");
        }

        [Fact]
        private void Once_type_schedule_should_return_datetime_config_as_next_date_if_it_is_after_current_day()
        {
            var conf = new Configuration();
            conf.TypeOfSchedule = Mode.Once;
            var Date = new Date(new DateTime(2021, 1, 3), new TimeSpan(12, 0, 0));
            conf.CurrentDate = Date;
            conf.DailyMode = Mode.Once;
            conf.DateOnce = new Date(new DateTime(2021, 1, 4), new TimeSpan(11, 0, 0));

            Scheduler.GetNextDateOutput(conf).Should().Be("Occurs once. Schedule will be used on 04/01/2021 at 11:00:00.");
        }

        [Fact]
        private void Once_type_schedule_should_return_empty_string_if_next_date_is_before_current_day()
        {
            var conf = new Configuration();
            conf.TypeOfSchedule = Mode.Once;
            var Date = new Date(new DateTime(2021, 1, 3), new TimeSpan(12, 0, 0));
            conf.CurrentDate = Date;
            conf.DailyMode = Mode.Once;
            conf.DateOnce = new Date(new DateTime(2021, 1, 2), new TimeSpan(11, 0, 0));

            Scheduler.GetNextDateOutput(conf).Should().Be(string.Empty);
        }

        [Fact]
        private void Once_type_schedule_should_return_empty_string_if_next_date_is_before_start_day()
        {
            var conf = new Configuration();
            conf.TypeOfSchedule = Mode.Once;
            var Date = new Date(new DateTime(2021, 1, 3), new TimeSpan(12, 0, 0));
            conf.CurrentDate = Date;
            conf.DailyMode = Mode.Once;
            conf.DateOnce = new Date(new DateTime(2021, 1, 4), new TimeSpan(12, 0, 0));
            conf.StartDate = new DateTime(2021, 1, 5);

            Scheduler.GetNextDateOutput(conf).Should().Be(string.Empty);
        }

        [Fact]
        private void Once_type_schedule_should_return_empty_string_if_next_date_is_after_end_day()
        {
            var conf = new Configuration();
            conf.TypeOfSchedule = Mode.Once;
            var Date = new Date(new DateTime(2021, 1, 3), new TimeSpan(12, 0, 0));
            conf.CurrentDate = Date;
            conf.DailyMode = Mode.Once;
            conf.DateOnce = new Date(new DateTime(2021, 1, 5), new TimeSpan(12, 0, 0));
            conf.EndDate = new DateTime(2021, 1, 4);

            Scheduler.GetNextDateOutput(conf).Should().Be(string.Empty);
        }

        [Fact]
        private void Once_type_schedule_should_return_datetime_config_as_next_date_if_it_is_between_limit_dates()
        {
            var conf = new Configuration();
            conf.TypeOfSchedule = Mode.Once;
            var Date = new Date(new DateTime(2021, 1, 3), new TimeSpan(12, 0, 0));
            conf.CurrentDate = Date;
            conf.DailyMode = Mode.Once;
            conf.DateOnce = new Date(new DateTime(2021, 1, 4), new TimeSpan(12, 0, 0));
            conf.StartDate = new DateTime(2021, 1, 2);
            conf.EndDate = new DateTime(2021, 1, 5);

            Scheduler.GetNextDateOutput(conf).Should().Be("Occurs once. Schedule will be used on 04/01/2021 at 12:00:00 starting on 02/01/2021 ending on 05/01/2021.");
        }

        [Fact]
        private void Once_type_schedule_should_return_datetime_config_as_next_date_if_it_is_after_start_day()
        {
            var conf = new Configuration();
            conf.TypeOfSchedule = Mode.Once;
            var Date = new Date(new DateTime(2021, 1, 3), new TimeSpan(12, 0, 0));
            conf.CurrentDate = Date;
            conf.DailyMode = Mode.Once;
            conf.DateOnce = new Date(new DateTime(2021, 1, 4), new TimeSpan(12, 0, 0));
            conf.StartDate = new DateTime(2021, 1, 2);

            Scheduler.GetNextDateOutput(conf).Should().Be("Occurs once. Schedule will be used on 04/01/2021 at 12:00:00 starting on 02/01/2021.");
        }

        [Fact]
        private void Once_type_schedule_should_return_datetime_config_as_next_date_if_it_is_before_end_day()
        {
            var conf = new Configuration();
            conf.TypeOfSchedule = Mode.Once;
            var Date = new Date(new DateTime(2021, 1, 3), new TimeSpan(12, 0, 0));
            conf.CurrentDate = Date;
            conf.DailyMode = Mode.Once;
            conf.DateOnce = new Date(new DateTime(2021, 1, 4), new TimeSpan(12, 0, 0));
            conf.EndDate = new DateTime(2021, 1, 5);

            Scheduler.GetNextDateOutput(conf).Should().Be("Occurs once. Schedule will be used on 04/01/2021 at 12:00:00 ending on 05/01/2021.");
        }

        [Fact]
        private void Once_type_schedule_should_throw_exception_if_TypeOfSchedule_is_null()
        {
            var conf = new Configuration();           
            var Date = new Date(new DateTime(2021, 1, 3), new TimeSpan(12, 0, 0));
            conf.CurrentDate = Date;
            conf.DailyMode = Mode.Once;
            conf.DateOnce = new Date(new DateTime(2021, 1, 4), new TimeSpan(12, 0, 0));

            Action getNextDate = () => Scheduler.GetNextDateOutput(conf);
            getNextDate.Should().Throw<ArgumentNullException>(ExceptionMessages.TypeOfScheduleEmpty);
        }

        [Fact]
        private void Once_type_schedule_should_throw_exception_if_DateOnce_is_null()
        {
            var conf = new Configuration();
            conf.TypeOfSchedule = Mode.Once;
            var Date = new Date(new DateTime(2021, 1, 3), new TimeSpan(12, 0, 0));
            conf.CurrentDate = Date;
            conf.DailyMode = Mode.Once;

            Action getNextDate = () => Scheduler.GetNextDateOutput(conf);
            getNextDate.Should().Throw<ArgumentNullException>(ExceptionMessages.ConfigurationDateTimeNull);
        }

        [Fact]
        private void Scheduler_should_throw_exception_if_start_day_is_after_end_date()
        {
            var conf = new Configuration();
            conf.TypeOfSchedule = Mode.Once;
            var Date = new Date(new DateTime(2021, 1, 3), new TimeSpan(12, 0, 0));
            conf.CurrentDate = Date;
            conf.DailyMode = Mode.Once;
            conf.StartDate = new DateTime(2021, 1, 6);
            conf.EndDate = new DateTime(2021, 1, 5);

            Action getNextDate = () => Scheduler.GetNextDateOutput(conf);
            getNextDate.Should().Throw<InvalidOperationException>(ExceptionMessages.StartDateBiggerThanEndDate);
        }

        [Fact]
        private void Schedule_dailyFrecuency_nextDate_should_be_current_date_if_HourOnce_is_after_current_hour()
        {
            DateTime dateTime = new DateTime(2021, 1, 2);
            var conf = new Configuration();
            conf.TypeOfSchedule = Mode.Recurring;
            conf.Frecuency = Frecuency.Daily;
            conf.CurrentDate = new Date(dateTime, new TimeSpan(7, 0, 0));
            conf.NumberOfDays = 3;
            conf.HourOnce = new TimeSpan(8, 0, 0);

            Scheduler.GetNextDateOutput(conf).Should().Be("Occurs every 3 days. Schedule will be used on 02/01/2021 at 08:00:00.");
        }

        [Fact]
        private void Schedule_dailyFrecuency_nextDate_should_be_current_plus_days_of_interval_when_current_hour_bigger_than_HourOnce()
        {
            DateTime dateTime = new DateTime(2021, 1, 2);
            var conf = new Configuration();
            conf.TypeOfSchedule = Mode.Recurring;
            conf.Frecuency = Frecuency.Daily;
            conf.CurrentDate = new Date(dateTime, new TimeSpan(9, 0, 0));
            conf.NumberOfDays = 3;
            conf.HourOnce = new TimeSpan(8, 0, 0);

            Scheduler.GetNextDateOutput(conf).Should().Be("Occurs every 3 days. Schedule will be used on 05/01/2021 at 08:00:00.");
        }

        [Fact]
        private void Schedule_dailyFrecuency_everyday_nextDate_should_be_next_day_when_current_hour_bigger_than_HourOnce()
        {
            DateTime dateTime = new DateTime(2021, 1, 2);
            var conf = new Configuration();
            conf.TypeOfSchedule = Mode.Recurring;
            conf.Frecuency = Frecuency.Daily;
            conf.CurrentDate = new Date(dateTime, new TimeSpan(9, 0, 0));
            conf.NumberOfDays = 1;
            conf.HourOnce = new TimeSpan(8, 0, 0);

            Scheduler.GetNextDateOutput(conf).Should().Be("Occurs everyday. Schedule will be used on 03/01/2021 at 08:00:00.");
        }

        [Fact]
        private void Schedule_dailyFrecuency_nextDate_should_be_startDay_when_startDay_is_after_currentDate()
        {
            DateTime dateTime = new DateTime(2021, 1, 2);
            var conf = new Configuration();
            conf.TypeOfSchedule = Mode.Recurring;
            conf.Frecuency = Frecuency.Daily;
            conf.CurrentDate = new Date(dateTime, new TimeSpan(9, 0, 0));
            conf.NumberOfDays = 3;
            conf.HourOnce = new TimeSpan(8, 0, 0);
            conf.StartDate = new DateTime(2021, 1, 10);

            Scheduler.GetNextDateOutput(conf).Should().Be("Occurs every 3 days. Schedule will be used on 10/01/2021 at 08:00:00 starting on 10/01/2021.");
        }

        [Fact]
        private void Schedule_recurring_dailyFrecuency_hour_should_have_hour_interval_determined()
        {
            DateTime dateTime = new DateTime(2021, 1, 2);
            var conf = new Configuration
            {
                TypeOfSchedule = Mode.Recurring,
                Frecuency = Frecuency.Daily,
                DailyMode = Mode.Recurring,
                CurrentDate = new Date(dateTime, new TimeSpan(9, 0, 0)),
                NumberOfDays = 3,
                DailyFrecuency = DailyFrecuency.Hour
            };

            Action getNextDate = () => Scheduler.GetNextDateOutput(conf);
            getNextDate.Should().Throw<ArgumentNullException>(ExceptionMessages.TimeIntervalNull);
        }

        [Fact]
        private void Schedule_recurring_dailyFrecuency_minute_should_have_minute_interval_determined()
        {
            DateTime dateTime = new DateTime(2021, 1, 2);
            var conf = new Configuration
            {
                TypeOfSchedule = Mode.Recurring,
                Frecuency = Frecuency.Daily,
                DailyMode = Mode.Recurring,
                CurrentDate = new Date(dateTime, new TimeSpan(9, 0, 0)),
                NumberOfDays = 3,
                DailyFrecuency = DailyFrecuency.Minute
            };

            Action getNextDate = () => Scheduler.GetNextDateOutput(conf);
            getNextDate.Should().Throw<ArgumentNullException>(ExceptionMessages.TimeIntervalNull);
        }

        [Fact]
        private void Schedule_recurring_dailyFrecuency_second_should_have_second_interval_determined()
        {
            DateTime dateTime = new DateTime(2021, 1, 2);
            var conf = new Configuration
            {
                TypeOfSchedule = Mode.Recurring,
                Frecuency = Frecuency.Daily,
                DailyMode = Mode.Recurring,
                CurrentDate = new Date(dateTime, new TimeSpan(9, 0, 0)),
                NumberOfDays = 3,
                DailyFrecuency = DailyFrecuency.Second
            };

            Action getNextDate = () => Scheduler.GetNextDateOutput(conf);
            getNextDate.Should().Throw<ArgumentNullException>(ExceptionMessages.TimeIntervalNull);
        }

        [Fact]
        private void Next_date_should_be_current_when_day_of_week_match_and_end_hour_is_after_current_hour()
        {
            var conf = new Configuration
            {
                TypeOfSchedule = Mode.Recurring,
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Once,
                HourOnce = new TimeSpan(7, 0, 0),
                EndHour = new TimeSpan(13, 0, 0),
                HourInterval = 1,
                CurrentDate = new Date(new DateTime(2021, 1, 5), new TimeSpan(6, 0, 0)),
                DaysOfWeek = new[] { DayOfWeek.Tuesday, DayOfWeek.Friday },
                WeekInterval = 2
            };

            Scheduler.GetNextDateOutput(conf).Should().Be("Occurs every 2 weeks. Schedule will be used on 05/01/2021 at 07:00:00.");
        }

        [Fact]
        private void Next_date_should_be_following_day_when_current_day_does_not_match_with_day_of_week()
        {
            var conf = new Configuration
            {
                TypeOfSchedule = Mode.Recurring,
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Recurring,
                StartHour = new TimeSpan(7, 0, 0),
                EndHour = new TimeSpan(13, 0, 0),
                HourInterval = 1,
                CurrentDate = new Date(new DateTime(2021, 1, 4), new TimeSpan(6, 0, 0)),
                DaysOfWeek = new[] { DayOfWeek.Tuesday, DayOfWeek.Friday },
                WeekInterval = 2
            };

            Scheduler.GetNextDateOutput(conf).Should().Be("Occurs every 2 weeks. Schedule will be used on 05/01/2021 at 07:00:00 starting on 07:00:00 ending on 13:00:00.");
        }

        [Fact]
        private void Next_date_should_be_current_day_and_start_hour_when_current_hour_is_before_start_hour()
        {
            var conf = new Configuration
            {
                TypeOfSchedule = Mode.Recurring,
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Recurring,
                StartHour = new TimeSpan(7, 0, 0),
                EndHour = new TimeSpan(13, 0, 0),
                HourInterval = 1,
                CurrentDate = new Date(new DateTime(2021, 1, 4), new TimeSpan(6, 0, 0)),
                DaysOfWeek = new[] { DayOfWeek.Monday, DayOfWeek.Friday },
                WeekInterval = 2
            };

            Scheduler.GetNextDateOutput(conf).Should().Be("Occurs every 2 weeks. Schedule will be used on 04/01/2021 at 07:00:00 starting on 07:00:00 ending on 13:00:00.");
        }

        [Fact]
        private void Next_date_should_be_on_two_weeks_when_current_day_does_not_match_with_rest_of_days_of_this_week()
        {
            var conf = new Configuration
            {
                TypeOfSchedule = Mode.Recurring,
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Recurring,
                StartHour = new TimeSpan(7, 0, 0),
                EndHour = new TimeSpan(13, 0, 0),
                HourInterval = 1,
                CurrentDate = new Date(new DateTime(2021, 1, 6), new TimeSpan(6, 0, 0)),
                DaysOfWeek = new[] { DayOfWeek.Tuesday },
                WeekInterval = 2
            };

            Scheduler.GetNextDateOutput(conf).Should().Be("Occurs every 2 weeks. Schedule will be used on 19/01/2021 at 07:00:00 starting on 07:00:00 ending on 13:00:00.");
        }

        [Fact]
        private void Schedule_recurring_weeklyFrecuency_should_have_minute_interval_determined()
        {
            DateTime dateTime = new DateTime(2021, 1, 2);
            var conf = new Configuration
            {
                TypeOfSchedule = Mode.Recurring,
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Once,
                CurrentDate = new Date(dateTime, new TimeSpan(9, 0, 0))
            };

            Action getNextDate = () => Scheduler.GetNextDateOutput(conf);
            getNextDate.Should().Throw<ArgumentNullException>(ExceptionMessages.WeeklyIntervalNull);
        }
    }
}
