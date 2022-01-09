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
            conf.Frecuency = Frecuency.Once;
            var Date = new Date(new DateTime(2021, 1, 3), new TimeSpan(12, 0, 0));
            conf.CurrentDate = Date;
            conf.DailyMode = Mode.Once;
            conf.DateOnce = new Date(new DateTime(2021, 1, 3), new TimeSpan(12, 0, 0));

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs once. Schedule will be used on 03/01/2021 at 12:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(conf.DateOnce);
        }

        [Fact]
        private void Once_type_schedule_should_return_datetime_config_as_next_date_if_it_is_after_current_hour()
        {
            var conf = new Configuration();
            conf.Frecuency = Frecuency.Once;
            var Date = new Date(new DateTime(2021, 1, 3), new TimeSpan(12, 0, 0));
            conf.CurrentDate = Date;
            conf.DailyMode = Mode.Once;
            conf.DateOnce = new Date(new DateTime(2021, 1, 3), new TimeSpan(13, 0, 0));

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs once. Schedule will be used on 03/01/2021 at 13:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(conf.DateOnce);
        }

        [Fact]
        private void Once_type_schedule_should_return_datetime_config_as_next_date_if_it_is_after_current_day()
        {
            var conf = new Configuration();
            conf.Frecuency = Frecuency.Once;
            var Date = new Date(new DateTime(2021, 1, 3), new TimeSpan(12, 0, 0));
            conf.CurrentDate = Date;
            conf.DailyMode = Mode.Once;
            conf.DateOnce = new Date(new DateTime(2021, 1, 4), new TimeSpan(11, 0, 0));

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs once. Schedule will be used on 04/01/2021 at 11:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(conf.DateOnce);
        }

        [Fact]
        private void Once_type_schedule_should_return_empty_string_if_next_date_is_before_current_day()
        {
            var conf = new Configuration();
            conf.Frecuency = Frecuency.Once;
            var Date = new Date(new DateTime(2021, 1, 3), new TimeSpan(12, 0, 0));
            conf.CurrentDate = Date;
            conf.DailyMode = Mode.Once;
            conf.DateOnce = new Date(new DateTime(2021, 1, 2), new TimeSpan(11, 0, 0));

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Should().BeNull();
        }

        [Fact]
        private void Once_type_schedule_should_return_empty_string_if_next_date_is_before_start_day()
        {
            var conf = new Configuration();
            conf.Frecuency = Frecuency.Once;
            var Date = new Date(new DateTime(2021, 1, 3), new TimeSpan(12, 0, 0));
            conf.CurrentDate = Date;
            conf.DailyMode = Mode.Once;
            conf.DateOnce = new Date(new DateTime(2021, 1, 4), new TimeSpan(12, 0, 0));
            conf.StartDate = new DateTime(2021, 1, 5);

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Should().BeNull();
        }

        [Fact]
        private void Once_type_schedule_should_return_empty_string_if_next_date_is_after_end_day()
        {
            var Date = new Date(new DateTime(2021, 1, 3), new TimeSpan(12, 0, 0));
            var conf = new Configuration
            {
                Frecuency = Frecuency.Once,
                CurrentDate = Date,
                DailyMode = Mode.Once,
                DateOnce = new Date(new DateTime(2021, 1, 5), new TimeSpan(12, 0, 0)),
                EndDate = new DateTime(2021, 1, 4)
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Should().BeNull();
        }

        [Fact]
        private void Once_type_schedule_should_return_datetime_config_as_next_date_if_it_is_between_limit_dates()
        {
            var conf = new Configuration();
            conf.Frecuency = Frecuency.Once;
            var Date = new Date(new DateTime(2021, 1, 3), new TimeSpan(12, 0, 0));
            conf.CurrentDate = Date;
            conf.DailyMode = Mode.Once;
            conf.DateOnce = new Date(new DateTime(2021, 1, 4), new TimeSpan(12, 0, 0));
            conf.StartDate = new DateTime(2021, 1, 2);
            conf.EndDate = new DateTime(2021, 1, 5);

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs once. Schedule will be used on 04/01/2021 at 12:00:00 starting on 02/01/2021 ending on 05/01/2021.");
            dateDescription.Date.Should().BeEquivalentTo(conf.DateOnce);
        }

        [Fact]
        private void Once_type_schedule_should_return_datetime_config_as_next_date_if_it_is_after_start_day()
        {
            var conf = new Configuration();
            conf.Frecuency = Frecuency.Once;
            var Date = new Date(new DateTime(2021, 1, 3), new TimeSpan(12, 0, 0));
            conf.CurrentDate = Date;
            conf.DailyMode = Mode.Once;
            conf.DateOnce = new Date(new DateTime(2021, 1, 4), new TimeSpan(12, 0, 0));
            conf.StartDate = new DateTime(2021, 1, 2);

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs once. Schedule will be used on 04/01/2021 at 12:00:00 starting on 02/01/2021.");
            dateDescription.Date.Should().BeEquivalentTo(conf.DateOnce);
        }

        [Fact]
        private void Once_type_schedule_should_return_datetime_config_as_next_date_if_it_is_before_end_day()
        {
            var conf = new Configuration();
            conf.Frecuency = Frecuency.Once;
            var Date = new Date(new DateTime(2021, 1, 3), new TimeSpan(12, 0, 0));
            conf.CurrentDate = Date;
            conf.DailyMode = Mode.Once;
            conf.DateOnce = new Date(new DateTime(2021, 1, 4), new TimeSpan(12, 0, 0));
            conf.EndDate = new DateTime(2021, 1, 5);

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs once. Schedule will be used on 04/01/2021 at 12:00:00 ending on 05/01/2021.");
            dateDescription.Date.Should().BeEquivalentTo(conf.DateOnce);
        }

        [Fact]
        private void Once_type_schedule_should_throw_exception_if_DateOnce_is_null()
        {
            var conf = new Configuration();
            conf.Frecuency = Frecuency.Once;
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
            conf.Frecuency = Frecuency.Once;
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
            conf.Frecuency = Frecuency.Daily;
            conf.CurrentDate = new Date(dateTime, new TimeSpan(7, 0, 0));
            conf.NumberOfDays = 3;
            conf.HourOnce = new TimeSpan(8, 0, 0);

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 3 days. Schedule will be used on 02/01/2021 at 08:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(dateTime, conf.HourOnce));
        }

        [Fact]
        private void Schedule_dailyFrecuency_nextDate_should_be_current_plus_days_of_interval_when_current_hour_bigger_than_HourOnce()
        {
            DateTime dateTime = new DateTime(2021, 1, 2);
            var conf = new Configuration();

            conf.Frecuency = Frecuency.Daily;
            conf.CurrentDate = new Date(dateTime, new TimeSpan(9, 0, 0));
            conf.NumberOfDays = 3;
            conf.HourOnce = new TimeSpan(8, 0, 0);

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 3 days. Schedule will be used on 05/01/2021 at 08:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 1, 5), conf.HourOnce));
        }

        [Fact]
        private void Schedule_dailyFrecuency_everyday_nextDate_should_be_next_day_when_current_hour_bigger_than_HourOnce()
        {
            DateTime dateTime = new DateTime(2021, 1, 2);
            var conf = new Configuration();
            conf.Frecuency = Frecuency.Daily;
            conf.CurrentDate = new Date(dateTime, new TimeSpan(9, 0, 0));
            conf.NumberOfDays = 1;
            conf.HourOnce = new TimeSpan(8, 0, 0);

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs everyday. Schedule will be used on 03/01/2021 at 08:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 1, 3), conf.HourOnce));
        }

        [Fact]
        private void Schedule_dailyFrecuency_nextDate_should_be_startDay_when_startDay_is_after_currentDate()
        {
            DateTime dateTime = new DateTime(2021, 1, 2);
            var conf = new Configuration();
            conf.Frecuency = Frecuency.Daily;
            conf.CurrentDate = new Date(dateTime, new TimeSpan(9, 0, 0));
            conf.NumberOfDays = 3;
            conf.HourOnce = new TimeSpan(8, 0, 0);
            conf.StartDate = new DateTime(2021, 1, 10);

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 3 days. Schedule will be used on 10/01/2021 at 08:00:00 starting on 10/01/2021.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 1, 10), conf.HourOnce));
        }

        [Fact]
        private void Schedule_dailyFrecuency_nextDate_should_be_currentDay_when_currentHour_is_between_startHour_and_endHour()
        {
            DateTime dateTime = new DateTime(2021, 1, 10);
            var conf = new Configuration();
            conf.Frecuency = Frecuency.Daily;
            conf.DailyMode = Mode.Recurring;
            conf.CurrentDate = new Date(dateTime, new TimeSpan(9, 0, 0));
            conf.NumberOfDays = 3;
            conf.StartHour = new TimeSpan(8, 0, 0);
            conf.EndHour = new TimeSpan(14, 0, 0);
            conf.DailyFrecuency = DailyFrecuency.Minute;
            conf.MinuteInterval = 40;

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 3 days. Schedule will be used on 10/01/2021 at 09:20:00 starting on 08:00:00 ending on 14:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(dateTime, new TimeSpan(9,20,0)));
        }


        [Fact]
        private void Schedule_dailyFrecuency_nextDate_should_be_current_plus_days_of_interval_when_current_hour_bigger_than_endHour()
        {
            var dateTime = new DateTime(2021, 1, 10);
            var conf = new Configuration
            {
                Frecuency = Frecuency.Daily,
                DailyMode = Mode.Recurring,
                CurrentDate = new Date(dateTime, new TimeSpan(19, 0, 0)),
                NumberOfDays = 3,
                StartHour = new TimeSpan(8, 0, 0),
                EndHour = new TimeSpan(14, 0, 0),
                DailyFrecuency = DailyFrecuency.Minute,
                MinuteInterval = 40
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 3 days. Schedule will be used on 13/01/2021 at 08:00:00 starting on 08:00:00 ending on 14:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021,1,13), conf.StartHour));
        }

        [Fact]
        private void Schedule_dailyFrecuency_nextDate_should_be_current_plus_days_of_interval_when_current_hour_bigger_than_last_execution_in_currentDay()
        {
            var dateTime = new DateTime(2021, 1, 10);
            var conf = new Configuration
            {
                Frecuency = Frecuency.Daily,
                DailyMode = Mode.Recurring,
                CurrentDate = new Date(dateTime, new TimeSpan(13, 40, 0)),
                NumberOfDays = 3,
                StartHour = new TimeSpan(8, 0, 0),
                EndHour = new TimeSpan(13, 50, 0),
                DailyFrecuency = DailyFrecuency.Minute,
                MinuteInterval = 40
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 3 days. Schedule will be used on 13/01/2021 at 08:00:00 starting on 08:00:00 ending on 13:50:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 1, 13), conf.StartHour));
        }

        [Fact]
        private void Schedule_dailyFrecuency_check_next_seven_dates()
        {
            var dateTime = new DateTime(2021, 1, 10);
            var conf = new Configuration
            {
                Frecuency = Frecuency.Daily,
                DailyMode = Mode.Recurring,
                CurrentDate = new Date(dateTime, new TimeSpan(13, 30, 0)),
                NumberOfDays = 3,
                StartHour = new TimeSpan(8, 0, 0),
                EndHour = new TimeSpan(14, 0, 0),
                DailyFrecuency = DailyFrecuency.Hour,
                HourInterval = 3,
                EndDate = new DateTime(2021, 1, 17)
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 3 days. Schedule will be used on 10/01/2021 at 14:00:00 ending on 17/01/2021 starting on 08:00:00 ending on 14:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 1, 10), conf.EndHour));

            conf.CurrentDate = dateDescription.Date;
            dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 3 days. Schedule will be used on 13/01/2021 at 08:00:00 ending on 17/01/2021 starting on 08:00:00 ending on 14:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 1, 13), conf.StartHour));

            conf.CurrentDate = dateDescription.Date;
            dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 3 days. Schedule will be used on 13/01/2021 at 11:00:00 ending on 17/01/2021 starting on 08:00:00 ending on 14:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 1, 13), new TimeSpan(11,0,0)));

            conf.CurrentDate = dateDescription.Date;
            dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 3 days. Schedule will be used on 13/01/2021 at 14:00:00 ending on 17/01/2021 starting on 08:00:00 ending on 14:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 1, 13), conf.EndHour));

            conf.CurrentDate = dateDescription.Date;
            dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 3 days. Schedule will be used on 16/01/2021 at 08:00:00 ending on 17/01/2021 starting on 08:00:00 ending on 14:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 1, 16), conf.StartHour));

            conf.CurrentDate = dateDescription.Date;
            dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 3 days. Schedule will be used on 16/01/2021 at 11:00:00 ending on 17/01/2021 starting on 08:00:00 ending on 14:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 1, 16), new TimeSpan(11, 0, 0)));

            conf.CurrentDate = dateDescription.Date;
            dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 3 days. Schedule will be used on 16/01/2021 at 14:00:00 ending on 17/01/2021 starting on 08:00:00 ending on 14:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 1, 16), conf.EndHour));

            conf.CurrentDate = dateDescription.Date;
            dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Should().BeNull();
        }

        [Fact]
        private void Schedule_recurring_dailyFrecuency_numberOfDays_not_determined_should_throw_exception()
        {
            DateTime dateTime = new DateTime(2021, 1, 2);
            var conf = new Configuration
            {
                Frecuency = Frecuency.Daily,
                DailyMode = Mode.Once,
                HourOnce = new TimeSpan(8,0,0),
                CurrentDate = new Date(dateTime, new TimeSpan(9, 0, 0)),
            };

            Action getNextDate = () => Scheduler.GetNextDateOutput(conf);
            getNextDate.Should().Throw<ArgumentNullException>(ExceptionMessages.DailyIntervalNull);
        }

        [Fact]
        private void Schedule_recurring_dailyFrecuency_hour_should_have_hour_interval_determined()
        {
            DateTime dateTime = new DateTime(2021, 1, 2);
            var conf = new Configuration
            {
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
        private void Next_date_should_be_current_when_day_of_week_match_and_start_hour_when_current_hour_before_start()
        {
            var conf = new Configuration
            {
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Recurring,
                StartHour = new TimeSpan(7, 0, 0),
                EndHour = new TimeSpan(13, 0, 0),
                HourInterval = 1,
                CurrentDate = new Date(new DateTime(2021, 1, 5), new TimeSpan(6, 0, 0)),
                DaysOfWeek = new[] { DayOfWeek.Tuesday, DayOfWeek.Friday },
                WeekInterval = 2
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 weeks. Schedule will be used on 05/01/2021 at 07:00:00 starting on 07:00:00 ending on 13:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(conf.CurrentDate.Day, conf.StartHour));
        }

        [Fact]
        private void Next_date_should_be_current_when_current_hour_is_before_last_date_of_the_day()
        {
            var conf = new Configuration
            {
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Recurring,
                StartHour = new TimeSpan(7, 0, 0),
                EndHour = new TimeSpan(13, 0, 0),
                HourInterval = 1,
                CurrentDate = new Date(new DateTime(2021, 1, 5), new TimeSpan(8, 0, 0)),
                DaysOfWeek = new[] { DayOfWeek.Tuesday, DayOfWeek.Friday },
                WeekInterval = 1
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every week. Schedule will be used on 05/01/2021 at 09:00:00 starting on 07:00:00 ending on 13:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(conf.CurrentDate.Day, new TimeSpan(9, 0, 0)));
        }

        [Fact]
        private void Next_date_should_be_following_day_when_current_day_does_not_match_with_day_of_week()
        {
            var conf = new Configuration
            {
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Recurring,
                StartHour = new TimeSpan(7, 0, 0),
                EndHour = new TimeSpan(13, 0, 0),
                HourInterval = 1,
                CurrentDate = new Date(new DateTime(2021, 1, 4), new TimeSpan(6, 0, 0)),
                DaysOfWeek = new[] { DayOfWeek.Tuesday, DayOfWeek.Friday },
                WeekInterval = 2
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 weeks. Schedule will be used on 05/01/2021 at 07:00:00 starting on 07:00:00 ending on 13:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021,1,5), conf.StartHour));
        }

        [Fact]
        private void Next_date_should_be_currentDay_when_currentHour_is_before_hourOnce_and_match_daysOfWeek()
        {
            var conf = new Configuration
            {
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Once,
                HourOnce = new TimeSpan(13, 0, 0),
                CurrentDate = new Date(new DateTime(2021, 1, 5), new TimeSpan(6, 0, 0)),
                DaysOfWeek = new[] { DayOfWeek.Tuesday, DayOfWeek.Friday },
                WeekInterval = 2
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 weeks. Schedule will be used on 05/01/2021 at 13:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(conf.CurrentDate.Day, conf.HourOnce));
        }

        [Fact]
        private void Next_date_should_be_startDate_when_currentDay_is_before_and_matches_with_daysOfWeek()
        {
            var conf = new Configuration
            {
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Once,
                StartDate = new DateTime(2021, 1, 8),
                HourOnce = new TimeSpan(13, 0, 0),
                CurrentDate = new Date(new DateTime(2021, 1, 5), new TimeSpan(14, 0, 0)),
                DaysOfWeek = new[] { DayOfWeek.Tuesday, DayOfWeek.Friday },
                WeekInterval = 2
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 weeks. Schedule will be used on 08/01/2021 at 13:00:00 starting on 08/01/2021.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(conf.StartDate.Value, conf.HourOnce));
        }

        [Fact]
        private void Next_date_should_be_matching_day_after_current_when_current_does_not_match()
        {
            var conf = new Configuration
            {
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Once,
                HourOnce = new TimeSpan(13, 0, 0),
                CurrentDate = new Date(new DateTime(2021, 1, 6), new TimeSpan(14, 0, 0)),
                DaysOfWeek = new[] { DayOfWeek.Tuesday, DayOfWeek.Friday },
                WeekInterval = 2
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 weeks. Schedule will be used on 08/01/2021 at 13:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021,1,8), conf.HourOnce));
        }

        [Fact]
        private void Next_date_should_in_following_week_when_no_day_of_this_week_matches()
        {
            var conf = new Configuration
            {
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Once,
                HourOnce = new TimeSpan(13, 0, 0),
                CurrentDate = new Date(new DateTime(2021, 1, 6), new TimeSpan(14, 0, 0)),
                DaysOfWeek = new[] { DayOfWeek.Tuesday},
                WeekInterval = 2
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 weeks. Schedule will be used on 19/01/2021 at 13:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 1, 19), conf.HourOnce));
        }

        [Fact]
        private void Next_date_should_be_current_day_and_start_hour_when_current_hour_is_before_start_hour()
        {
            var conf = new Configuration
            {
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Recurring,
                StartHour = new TimeSpan(7, 0, 0),
                EndHour = new TimeSpan(13, 0, 0),
                HourInterval = 1,
                CurrentDate = new Date(new DateTime(2021, 1, 4), new TimeSpan(6, 0, 0)),
                DaysOfWeek = new[] { DayOfWeek.Monday, DayOfWeek.Friday },
                WeekInterval = 2
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 weeks. Schedule will be used on 04/01/2021 at 07:00:00 starting on 07:00:00 ending on 13:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(conf.CurrentDate.Day, conf.StartHour));
        }

        [Fact]
        private void Next_date_should_be_followingDay_after_startDay_when_startDay_is_after_currentDay()
        {
            var conf = new Configuration
            {
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Recurring,
                StartDate = new DateTime(2021, 1, 6),
                StartHour = new TimeSpan(7, 0, 0),
                EndHour = new TimeSpan(13, 0, 0),
                HourInterval = 1,
                CurrentDate = new Date(new DateTime(2021, 1, 4), new TimeSpan(6, 0, 0)),
                DaysOfWeek = new[] { DayOfWeek.Monday, DayOfWeek.Friday },
                WeekInterval = 2
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 weeks. Schedule will be used on 08/01/2021 at 07:00:00 starting on 06/01/2021 starting on 07:00:00 ending on 13:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021,1,8), conf.StartHour));
        }

        [Fact]
        private void Next_date_should_not_be_current_day_if_currentHour_is_after_last_execution_hour()
        {
            var conf = new Configuration
            {
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Recurring,
                StartHour = new TimeSpan(7, 0, 0),
                EndHour = new TimeSpan(7, 1, 0),
                DailyFrecuency = DailyFrecuency.Second,
                SecondInterval = 35,
                CurrentDate = new Date(new DateTime(2021, 1, 4), new TimeSpan(7, 0, 40)),
                DaysOfWeek = new[] { DayOfWeek.Monday },
                WeekInterval = 2
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 weeks. Schedule will be used on 18/01/2021 at 07:00:00 starting on 07:00:00 ending on 07:01:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 1, 18), conf.StartHour));
        }

        [Fact]
        private void Next_date_should_be_on_two_weeks_when_current_day_does_not_match_with_rest_of_days_of_this_week()
        {
            var conf = new Configuration
            {
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Recurring,
                StartHour = new TimeSpan(7, 0, 0),
                EndHour = new TimeSpan(13, 0, 0),
                HourInterval = 1,
                CurrentDate = new Date(new DateTime(2021, 1, 6), new TimeSpan(6, 0, 0)),
                DaysOfWeek = new[] { DayOfWeek.Tuesday },
                WeekInterval = 2
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 weeks. Schedule will be used on 19/01/2021 at 07:00:00 starting on 07:00:00 ending on 13:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 1, 19), conf.StartHour));
        }

        private void Schedule_weeklyFrecuency_check_next_seven_dates()
        {
            var conf = new Configuration
            {
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Recurring,
                StartHour = new TimeSpan(7, 0, 0),
                EndHour = new TimeSpan(13, 0, 0),
                HourInterval = 4,
                CurrentDate = new Date(new DateTime(2021, 1, 6), new TimeSpan(6, 0, 0)),
                DaysOfWeek = new[] { DayOfWeek.Tuesday, DayOfWeek.Friday },
                WeekInterval = 2
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 weeks. Schedule will be used on 19/01/2021 at 07:00:00 starting on 07:00:00 ending on 13:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 1, 19), conf.StartHour));

            conf.CurrentDate = dateDescription.Date;
            dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 weeks. Schedule will be used on 19/01/2021 at 11:00:00 starting on 07:00:00 ending on 13:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 1, 19), new TimeSpan(11,0,0)));

            conf.CurrentDate = dateDescription.Date;
            dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 weeks. Schedule will be used on 22/01/2021 at 07:00:00 starting on 07:00:00 ending on 13:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 1, 22), conf.StartHour));

            conf.CurrentDate = dateDescription.Date;
            dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 weeks. Schedule will be used on 22/01/2021 at 11:00:00 starting on 07:00:00 ending on 13:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 1, 22), new TimeSpan(11, 0, 0)));

            conf.CurrentDate = dateDescription.Date;
            dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 weeks. Schedule will be used on 02/02/2021 at 07:00:00 starting on 07:00:00 ending on 13:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 2, 2), conf.StartHour));

            conf.CurrentDate = dateDescription.Date;
            dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 weeks. Schedule will be used on 02/02/2021 at 11:00:00 starting on 07:00:00 ending on 13:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 2, 2), new TimeSpan(11, 0, 0)));

            conf.CurrentDate = dateDescription.Date;
            dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 weeks. Schedule will be used on 05/01/2021 at 07:00:00 starting on 07:00:00 ending on 13:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 2, 5), conf.StartHour));

            conf.CurrentDate = dateDescription.Date;
            dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 weeks. Schedule will be used on 05/01/2021 at 11:00:00 starting on 07:00:00 ending on 13:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 2, 5), new TimeSpan(11, 0, 0)));
        }

        [Fact]
        private void Schedule_recurring_weeklyFrecuency_should_have_minute_interval_determined()
        {
            DateTime dateTime = new DateTime(2021, 1, 2);
            var conf = new Configuration
            {
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Once,
                CurrentDate = new Date(dateTime, new TimeSpan(9, 0, 0))
            };

            Action getNextDate = () => Scheduler.GetNextDateOutput(conf);
            getNextDate.Should().Throw<ArgumentNullException>(ExceptionMessages.WeeklyIntervalNull);
        }

        [Fact]
        private void Hour_frecuency_interval_should_be_less_than_a_day()
        {
            DateTime dateTime = new DateTime(2021, 1, 2);
            var conf = new Configuration
            {
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Recurring,
                DailyFrecuency = DailyFrecuency.Hour,
                HourInterval = 24,
                WeekInterval = 3,
                CurrentDate = new Date(dateTime, new TimeSpan(9, 0, 0)),
                DaysOfWeek = new DayOfWeek[]{DayOfWeek.Saturday}
            };

            Action getNextDate = () => Scheduler.GetNextDateOutput(conf);
            getNextDate.Should().Throw<ArgumentOutOfRangeException>(ExceptionMessages.HourIntervalOutOfRange);
        }

        [Fact]
        private void Hour_frecuency_interval_should_be_more_than_zero()
        {
            DateTime dateTime = new DateTime(2021, 1, 2);
            var conf = new Configuration
            {
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Recurring,
                DailyFrecuency = DailyFrecuency.Hour,
                HourInterval = 0,
                WeekInterval = 3,
                CurrentDate = new Date(dateTime, new TimeSpan(9, 0, 0))
            };

            Action getNextDate = () => Scheduler.GetNextDateOutput(conf);
            getNextDate.Should().Throw<ArgumentOutOfRangeException>(ExceptionMessages.HourIntervalOutOfRange);
        }

        [Fact]
        private void Minute_frecuency_interval_should_be_less_than_a_day()
        {
            DateTime dateTime = new DateTime(2021, 1, 2);
            var conf = new Configuration
            {
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Recurring,
                DailyFrecuency = DailyFrecuency.Minute,
                MinuteInterval = 1440,
                WeekInterval = 3,
                CurrentDate = new Date(dateTime, new TimeSpan(9, 0, 0))
            };

            Action getNextDate = () => Scheduler.GetNextDateOutput(conf);
            getNextDate.Should().Throw<ArgumentOutOfRangeException>(ExceptionMessages.HourIntervalOutOfRange);
        }

        [Fact]
        private void Minute_frecuency_interval_should_be_more_than_zero()
        {
            DateTime dateTime = new DateTime(2021, 1, 2);
            var conf = new Configuration
            {
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Recurring,
                DailyFrecuency = DailyFrecuency.Minute,
                MinuteInterval = 0,
                WeekInterval = 3,
                CurrentDate = new Date(dateTime, new TimeSpan(9, 0, 0))
            };

            Action getNextDate = () => Scheduler.GetNextDateOutput(conf);
            getNextDate.Should().Throw<ArgumentOutOfRangeException>(ExceptionMessages.HourIntervalOutOfRange);
        }

        [Fact]
        private void Second_frecuency_interval_should_be_less_than_a_day()
        {
            DateTime dateTime = new DateTime(2021, 1, 2);
            var conf = new Configuration
            {
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Recurring,
                DailyFrecuency = DailyFrecuency.Second,
                SecondInterval = 86400,
                WeekInterval = 3,
                CurrentDate = new Date(dateTime, new TimeSpan(9, 0, 0))
            };

            Action getNextDate = () => Scheduler.GetNextDateOutput(conf);
            getNextDate.Should().Throw<ArgumentOutOfRangeException>(ExceptionMessages.HourIntervalOutOfRange);
        }

        [Fact]
        private void Second_frecuency_interval_should_be_more_than_zero()
        {
            DateTime dateTime = new DateTime(2021, 1, 2);
            var conf = new Configuration
            {
                Frecuency = Frecuency.Weekly,
                DailyMode = Mode.Recurring,
                DailyFrecuency = DailyFrecuency.Second,
                SecondInterval = 0,
                WeekInterval = 3,
                CurrentDate = new Date(dateTime, new TimeSpan(9, 0, 0))
            };

            Action getNextDate = () => Scheduler.GetNextDateOutput(conf);
            getNextDate.Should().Throw<ArgumentOutOfRangeException>(ExceptionMessages.HourIntervalOutOfRange);
        }

        [Fact]
        private void Monthly_frecuency_should_return_month_day_given_the_day_number()
        {
            var conf = new Configuration
            {
                Frecuency = Frecuency.Monthly,
                DailyMode = Mode.Recurring,
                MonthlyFrecuencyType = TypeOfMonthlyFrecuency.DayOfMonth,
                StartHour = new TimeSpan(7, 0, 0),
                EndHour = new TimeSpan(13, 0, 0),
                HourInterval = 1,
                CurrentDate = new Date(new DateTime(2021, 1, 6), new TimeSpan(6, 0, 0)),
                MonthInterval = 2,
                DayOfMonth = 8
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 months. Schedule will be used on 08/01/2021 at 07:00:00 starting on 07:00:00 ending on 13:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 1, 8), conf.StartHour));
        }

        [Fact]
        private void Monthly_frecuency_should_return_month_day_of_next_month_given_the_day_number_when_current_day_bigger_than_day_number()
        {
            var conf = new Configuration
            {
                Frecuency = Frecuency.Monthly,
                DailyMode = Mode.Recurring,
                MonthlyFrecuencyType = TypeOfMonthlyFrecuency.DayOfMonth,
                StartHour = new TimeSpan(7, 0, 0),
                EndHour = new TimeSpan(13, 0, 0),
                HourInterval = 1,
                CurrentDate = new Date(new DateTime(2021, 1, 6), new TimeSpan(6, 0, 0)),
                MonthInterval = 2,
                DayOfMonth = 5
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 months. Schedule will be used on 05/02/2021 at 07:00:00 starting on 07:00:00 ending on 13:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 2, 5), conf.StartHour));
        }


        [Fact]
        private void Monthly_frecuency_should_return_third_thursday_of_actual_month()
        {
            var conf = new Configuration
            {
                Frecuency = Frecuency.Monthly,
                DailyMode = Mode.Recurring,
                MonthlyFrecuencyType = TypeOfMonthlyFrecuency.DayOfWeek,
                MonthlyFrecuency = MonthlyFrecuency.Third,
                StartHour = new TimeSpan(7, 0, 0),
                EndHour = new TimeSpan(13, 0, 0),
                HourInterval = 1,
                CurrentDate = new Date(new DateTime(2021, 1, 8), new TimeSpan(6, 0, 0)),
                MonthInterval = 2,
                DaysOfWeekMonthly = DaysOfWeekMonthly.Thursday
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 months. Schedule will be used on 21/01/2021 at 07:00:00 starting on 07:00:00 ending on 13:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 1, 21), conf.StartHour));
        }

        [Fact]
        private void Monthly_frecuency_should_return_forth_thursday_of_month()
        {
            var conf = new Configuration
            {
                Frecuency = Frecuency.Monthly,
                DailyMode = Mode.Recurring,
                MonthlyFrecuencyType = TypeOfMonthlyFrecuency.DayOfWeek,
                MonthlyFrecuency = MonthlyFrecuency.Fourth,
                StartHour = new TimeSpan(7, 0, 0),
                EndHour = new TimeSpan(13, 0, 0),
                HourInterval = 1,
                CurrentDate = new Date(new DateTime(2021, 1, 7), new TimeSpan(8, 0, 0)),
                MonthInterval = 2,
                DaysOfWeekMonthly = DaysOfWeekMonthly.Thursday
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 months. Schedule will be used on 28/01/2021 at 07:00:00 starting on 07:00:00 ending on 13:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021,1,28), conf.StartHour));
        }

        [Fact]
        private void Monthly_frecuency_forth_thursday_of_january_2021_equals_last_thursday_january_2021()
        {
            var conf = new Configuration
            {
                Frecuency = Frecuency.Monthly,
                DailyMode = Mode.Recurring,
                MonthlyFrecuencyType = TypeOfMonthlyFrecuency.DayOfWeek,
                MonthlyFrecuency = MonthlyFrecuency.Last,
                StartHour = new TimeSpan(7, 0, 0),
                EndHour = new TimeSpan(13, 0, 0),
                HourInterval = 1,
                CurrentDate = new Date(new DateTime(2021, 1, 7), new TimeSpan(8, 0, 0)),
                MonthInterval = 2,
                DaysOfWeekMonthly = DaysOfWeekMonthly.Thursday
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 months. Schedule will be used on 28/01/2021 at 07:00:00 starting on 07:00:00 ending on 13:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 1, 28), conf.StartHour));
        }

        [Fact]
        private void Monthly_frecuency_should_return_current_day_when_matches_with_second_thursday_if_current_hour_is_before_hour_once()
        {
            var conf = new Configuration
            {
                Frecuency = Frecuency.Monthly,
                DailyMode = Mode.Once,
                MonthlyFrecuencyType = TypeOfMonthlyFrecuency.DayOfWeek,
                MonthlyFrecuency = MonthlyFrecuency.First,
                HourOnce = new TimeSpan(9, 0, 0),
                HourInterval = 1,
                CurrentDate = new Date(new DateTime(2021, 1, 7), new TimeSpan(8, 0, 0)),
                MonthInterval = 2,
                DaysOfWeekMonthly = DaysOfWeekMonthly.Thursday
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 months. Schedule will be used on 07/01/2021 at 09:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(conf.CurrentDate.Day, new TimeSpan(9, 0, 0)));
        }

        [Fact]
        private void Monthly_frecuency_should_return_next_week_day_that_matches_with_monday()
        {

        }

        [Fact]
        private void Monthly_frecuency_should_return_next_week_day_that_matches_with_tuesday()
        {

        }

        [Fact]
        private void Monthly_frecuency_should_return_next_week_day_that_matches_with_wednesday()
        {

        }

        [Fact]
        private void Monthly_frecuency_should_return_next_week_day_that_matches_with_friday()
        {

        }

        [Fact]
        private void Monthly_frecuency_should_return_next_week_day_that_matches_with_saturday()
        {

        }

        [Fact]
        private void Monthly_frecuency_should_return_next_week_day_that_matches_with_sunday()
        {

        }

        [Fact]
        private void Monthly_frecuency_should_return_next_week_day_that_matches_with_day()
        {
            var conf = new Configuration
            {
                Frecuency = Frecuency.Monthly,
                DailyMode = Mode.Once,
                MonthlyFrecuencyType = TypeOfMonthlyFrecuency.DayOfWeek,
                MonthlyFrecuency = MonthlyFrecuency.First,
                HourOnce = new TimeSpan(9, 0, 0),
                CurrentDate = new Date(new DateTime(2021, 1, 7), new TimeSpan(8, 0, 0)),
                MonthInterval = 2,
                DaysOfWeekMonthly = DaysOfWeekMonthly.Day
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 months. Schedule will be used on 01/02/2021 at 09:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 2, 1), new TimeSpan(9, 0, 0)));

            conf.CurrentDate = dateDescription.Date;
            dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 months. Schedule will be used on 01/04/2021 at 09:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 4, 1), new TimeSpan(9, 0, 0)));

            conf.CurrentDate = dateDescription.Date;
            dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 months. Schedule will be used on 01/06/2021 at 09:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 6, 1), new TimeSpan(9, 0, 0)));

            conf.CurrentDate = dateDescription.Date;
            dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 months. Schedule will be used on 01/08/2021 at 09:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 8, 1), new TimeSpan(9, 0, 0)));
        }

        [Fact]
        private void Monthly_frecuency_should_return_next_week_day_that_matches_with_week_day()
        {
            var conf = new Configuration
            {
                Frecuency = Frecuency.Monthly,
                DailyMode = Mode.Once,
                MonthlyFrecuency = MonthlyFrecuency.Second,
                MonthlyFrecuencyType = TypeOfMonthlyFrecuency.DayOfWeek,
                HourOnce = new TimeSpan(9, 0, 0),
                HourInterval = 1,
                CurrentDate = new Date(new DateTime(2021, 1, 7), new TimeSpan(8, 0, 0)),
                MonthInterval = 3,
                DaysOfWeekMonthly = DaysOfWeekMonthly.Weekday
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 3 months. Schedule will be used on 02/02/2021 at 09:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021,2,2), new TimeSpan(9, 0, 0)));

            conf.CurrentDate = dateDescription.Date;
            dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 3 months. Schedule will be used on 04/05/2021 at 09:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 5, 4), new TimeSpan(9, 0, 0)));

            conf.CurrentDate = dateDescription.Date;
            dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 3 months. Schedule will be used on 03/08/2021 at 09:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 8, 3), new TimeSpan(9, 0, 0)));

            conf.CurrentDate = dateDescription.Date;
            dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 3 months. Schedule will be used on 02/11/2021 at 09:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021, 11, 2), new TimeSpan(9, 0, 0)));

            conf.CurrentDate = dateDescription.Date;
            dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 3 months. Schedule will be used on 02/02/2022 at 09:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2022, 2, 2), new TimeSpan(9, 0, 0)));
        }

        [Fact]
        private void Monthly_frecuency_should_return_third_weekend_day_of_month()
        {
            var conf = new Configuration
            {
                Frecuency = Frecuency.Monthly,
                DailyMode = Mode.Once,
                MonthlyFrecuencyType = TypeOfMonthlyFrecuency.DayOfWeek,
                MonthlyFrecuency = MonthlyFrecuency.Third,
                HourOnce = new TimeSpan(9, 0, 0),
                HourInterval = 1,
                CurrentDate = new Date(new DateTime(2021, 1, 26), new TimeSpan(8, 0, 0)),
                MonthInterval = 2,
                DaysOfWeekMonthly = DaysOfWeekMonthly.WeekendDay
            };

            DateDescription dateDescription = Scheduler.GetNextDateOutput(conf);

            dateDescription.Description.Should().Be("Occurs every 2 months. Schedule will be used on 13/02/2021 at 09:00:00.");
            dateDescription.Date.Should().BeEquivalentTo(new Date(new DateTime(2021,2,13), new TimeSpan(9, 0, 0)));
        }
    }
}
