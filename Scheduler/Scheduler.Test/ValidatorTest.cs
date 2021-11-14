using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Scheduler;
using FluentAssertions;
using Scheduler.Auxiliar;

namespace Scheduler.Test
{
    public class ValidatorTest
    {
        [Fact]
        private void Start_Date_Bigger_Than_End_Date_Should_Throw_Exception()
        {
            DateTime? StartDate = new DateTime(2020, 1, 2);
            DateTime? EndDate = new DateTime(2020, 1, 1);

            Action act = () => Validator.ValidateLimitDates(StartDate, EndDate);

            act.Should().Throw<InvalidOperationException>(ExceptionMessages.StartDateBiggerThanEndDate);
        }

        [Fact]
        private void Start_Hour_Bigger_Than_End_Hour_Should_Throw_Exception()
        {
            TimeSpan? StartHour = new TimeSpan(8, 0, 0);
            TimeSpan? EndHour = new TimeSpan(7, 0, 0);

            Action act = () => Validator.ValidateLimitHours(StartHour, EndHour);

            act.Should().Throw<InvalidOperationException>(ExceptionMessages.StartHourBiggerThanEndHour);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        private void Not_positive_number_in_positive_number_field_should_throw_exception(int NotIntNumber)
        {
            Action act = () => Validator.ValidatePositiveNumber(NotIntNumber);

            act.Should().Throw<Exception>();
        }
    }
}
