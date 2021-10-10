using System;
using FluentAssertions;
using Xunit;

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
    }
}
