using System.Runtime.InteropServices.JavaScript;
using POC.Infrastructure.Common.Constants;
using POC.Infrastructure.Common.utils;
using POC.Infrastructure.Models;

namespace UnitTests;

[TestFixture]
public class DateRangeUtilityTests
{

    // [Test]
    // [TestCase("2023-07-01T00:00:00", "2023-07-02T00:00:00", "2023-07-01T00:00:00", 1, 1,TimeInclude.Both, true)]
    // [TestCase("2023-07-01T00:00:00", "2023-07-02T00:00:00", "2023-07-01T00:00:00", 1, 1,TimeInclude.Incoming, true)]
    // [TestCase("2023-07-01T00:00:00", "2023-07-02T00:00:00", "2023-06-30T00:00:00", 1, 1,TimeInclude.Outgoing, false)]
    // [TestCase("2023-07-01T00:00:00", "2023-07-02T00:00:00", "2023-06-30T00:00:00", 2, 0,TimeInclude.Both, false)]
    // public void IsBetween_GivenDates_ReturnsExpectedResult(string startDateStr, string endDateStr, string referenceDateStr, int amountFrom, int amountTo, TimeInclude include, bool expectedResult)
    // {
    //     // Arrange
    //     var orderStartDate = DateTime.Parse(startDateStr);
    //     var orderEndDate = DateTime.Parse(endDateStr);
    //     var referenceDate = DateTime.Parse(referenceDateStr);
    //
    //     var timeEncapsulated = new TimeEncapsulated
    //     {
    //         From = new TimeRangePart { Unit = TimeUnit.Day, Mode = Mode.Start, Amount = amountFrom },
    //         To = new TimeRangePart { Unit = TimeUnit.Day, Mode = Mode.End, Amount = amountTo },
    //         Include = include
    //     };
    //
    //     // Act
    //     var result = DateRangeUtility.IsBetween(orderStartDate, orderEndDate, timeEncapsulated, referenceDate);
    //
    //     // Assert
    //     Assert.That(result, Is.EqualTo(expectedResult));
    // }
    
}
