using System;
using FluentAssertions;
using Xunit;
using Scheduler;

namespace Scheduler.Test
{
    public class CalculatorTest
    {
        [Theory]
        [InlineData("2020-1-1", "2020-1-1", "2020-1-1", true)]
        [InlineData("2020-1-2", "2020-1-1", "2020-1-3", true)]
        [InlineData("2020-1-2", "2020-1-3", "2020-1-4", false)]
        [InlineData("2020-1-5", "2020-1-1", "2020-1-4", false)]
        [InlineData("2020-1-1", "", "", true)]
        [InlineData("2020-1-1", "", "2020-1-2", true)]
        [InlineData("2020-1-2", "2020-1-1", "", true)]
        [InlineData("2020-1-2", "", "2020-1-1", false)]
        [InlineData("2020-1-1", "2020-1-2", "", false)]
        public void Next_Date_Should_Be_Between_Limits(string referenceDateStr, string startDateStr, string endDateStr, bool result)
        {
            DateTime ReferenceDate = DateTime.Parse(referenceDateStr);
            DateTime? StartDate = string.IsNullOrEmpty(startDateStr) ? null : DateTime.Parse(startDateStr);
            DateTime? EndDate = string.IsNullOrEmpty(endDateStr) ? null : DateTime.Parse(endDateStr);

            Assert.Equal(Calculator.DateInLimits(ReferenceDate, StartDate, EndDate), result);
        }

        [Theory]
        [InlineData(Type.Once, "2021-1-1", 1, "2021-1-1")]
        [InlineData(Type.Once, "2021-1-1", 2, "2021-1-1")]
        [InlineData(Type.Recurring, "2021-1-4", 1, "2021-1-5")]
        public void Input_Values_Return_Next_Date(Type type, string referenceDate, int numberOfDays, string nextDate)
        {
            DateTime ReferenceDate = DateTime.Parse(referenceDate);
            DateTime NextDate = DateTime.Parse(nextDate);

            Calculator.GetNextDate(type, ReferenceDate, numberOfDays).Should().Be(NextDate);
        }
    }
}
