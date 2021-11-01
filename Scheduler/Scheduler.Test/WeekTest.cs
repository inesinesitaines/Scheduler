using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace Scheduler.Test
{
    public class WeekTest
    {
        [Fact]
        public void Days_of_week_of_the_day_should_be_correct()
        {
            DateTime date = new DateTime(2021, 1, 20);
            DateTime[] daysOfWeek =
                {
                new DateTime(2021, 1, 17),
                new DateTime(2021, 1, 18),
                new DateTime(2021, 1, 19),
                new DateTime(2021, 1, 20),
                new DateTime(2021, 1, 21),
                new DateTime(2021, 1, 22),
                new DateTime(2021, 1, 23)
            };

            Week week = new Week(date);

            DateTime[] days = week.Days;

            for (int i = 0; i < daysOfWeek.Length; i++)
            {
                daysOfWeek[i].Should().Be(days[i]);
            }
        }

        [Fact]
        public void Week_should_return_day_of_day_of_week()
        {
            Week week = new Week(new DateTime(2021, 10, 20));
            week.GetDay(DayOfWeek.Monday).Should().Be(new DateTime(2021, 10, 18));
        }
    }
}
