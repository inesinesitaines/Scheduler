using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Scheduler;

namespace Scheduler.Test
{
    public class ValidatorTest
    {
        [Fact]
        private void Start_Date_Bigger_Than_End_Date_Should_Throw_Exception()
        {
        DateTime? StartDate = new DateTime(2020, 1, 2);
        DateTime? EndDate = new DateTime(2020, 1, 1);

        Assert.Throws<Exception>(() => Validator.ValidateStartEndDates(StartDate, EndDate));
        }
    }
}
