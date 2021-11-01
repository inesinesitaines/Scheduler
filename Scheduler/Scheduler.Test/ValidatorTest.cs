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
            act.Should().Throw<Exception>();
        }

        [Fact]
        private void Not_valid_string_format_datetime_should_throw_exception()
        {
            string DateTime = "Thu Jul 18 17:39:53 +0000 2013";
            Action act = () => Validator.ValidateDateTime(DateTime);
            act.Should().Throw<Exception>();
        }

        [Fact]
        private void Empty_string_in_datetime_field_should_return_null()
        {
            DateTime? nullDate = Validator.ValidateDateTimeNullable(string.Empty);
            nullDate.Should().BeNull();
        }

        [Theory]
        [InlineData("5.6")]
        [InlineData("one")]
        private void String_not_int_in_int_number_field_should_throw_exception(string notIntNumber)
        {
            Action act = () => Validator.ValidateIntNumber(notIntNumber);
            act.Should().Throw<Exception>();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        private void Not_positive_number_in_positive_number_field_should_throw_exception(int NotIntNumber)
        {
            Action act = () => Validator.ValidatePositiveNumber(NotIntNumber);

            act.Should().Throw<Exception>();
        }

        [Theory]
        [InlineData("Once", Mode.Once)]
        [InlineData("Recurring", Mode.Recurring)]
        private void Valid_type_of_schedule_returns_mode_enum(string TypeOfSchedule, Mode TypeEnum)
        {
            var mode = Validator.ValidateType(TypeOfSchedule);

            mode.Should().Be(TypeEnum);
        }

        [Fact]
        private void Not_valid_type_of_schedule_should_throw_exception()
        {
            string NotValidTypeOfSchedule = "Schedule Mode";

            Action act = () => Validator.ValidateType(NotValidTypeOfSchedule);

            act.Should().Throw<Exception>();
        }

        [Theory]
        [InlineData("Daily", Frecuency.Daily)]
        [InlineData("Weekly", Frecuency.Weekly)]
        private void Valid_frecuency_returns_frecuency_enum(string imputFrecuency, Frecuency frecuency)
        {
            var Frecuency = Validator.ValidateFrecuency(imputFrecuency);

            Frecuency.Should().Be(frecuency);
        }

        [Fact]
        private void Not_valid_frecuency_should_throw_exception()
        {
            string NotValidFrecuency = "Once in a year";

            Action act = () => Validator.ValidateFrecuency(NotValidFrecuency);

            act.Should().Throw<Exception>();
        }

        [Fact]
        private void Not_valid_timespan_should_throw_exception()
        {
            string NotValidHour = "hour";

            Action act = () => Validator.ValidateTimeSpan(NotValidHour);

            act.Should().Throw<Exception>();
        }
    }
}
