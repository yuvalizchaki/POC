using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Common.Constants;
using POC.Infrastructure.Common.utils;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.Models;

namespace UnitTests;
using NUnit.Framework;
using System;
using System.Collections.Generic;

[TestFixture]
public class ScreenProfileFilteringExtensionsTests
{
    [Test]
    [TestCase(TimeUnit.Day, Mode.Start, -93, "2024-07-07T00:00:00", "yyyy-MM-ddTHH:mm:ss", "2024-07-07T01:00:00")]
    [TestCase(TimeUnit.Day, Mode.Fixed, 1, "2024-07-07T01:00:00", "yyyy-MM-ddTHH:mm:ss", "2024-07-06T01:00:00")]
    [TestCase(TimeUnit.Day, Mode.End, 2, "2024-07-06T23:59:59", "yyyy-MM-ddTHH:mm:ss", "2024-07-06T23:59:58")]
    [TestCase(TimeUnit.Week, Mode.Start, 0, "2024-06-30T00:00:00", "yyyy-MM-ddTHH:mm:ss", "2024-07-06T10:00:00")]
    [TestCase(TimeUnit.Week, Mode.Fixed, 1, "2024-07-13T01:00:00", "yyyy-MM-ddTHH:mm:ss", "2024-07-06T01:00:00")]
    [TestCase(TimeUnit.Week, Mode.End, 0, "2024-07-06T23:59:59", "yyyy-MM-ddTHH:mm:ss", "2024-07-06T23:01:00")]
    [TestCase(TimeUnit.Month, Mode.Start, 0, "2024-07-01T00:00:00", "yyyy-MM-ddTHH:mm:ss", "2024-07-06T00:00:00")]
    [TestCase(TimeUnit.Month, Mode.Fixed, 1, "2024-08-06T00:01:00", "yyyy-MM-ddTHH:mm:ss", "2024-07-06T00:01:00")]
    [TestCase(TimeUnit.Month, Mode.End, -1, "2024-07-31T23:59:59", "yyyy-MM-ddTHH:mm:ss", "2024-07-06T23:59:58")]
    [TestCase(TimeUnit.Year, Mode.Start, 13, "2024-01-01T00:00:00", "yyyy-MM-ddTHH:mm:ss", "2024-07-06T13:00:00")]
    [TestCase(TimeUnit.Day, Mode.Fixed, 2, "2024-07-08T01:00:00", "yyyy-MM-ddTHH:mm:ss", "2024-07-06T01:00:00")]
    [TestCase(TimeUnit.Day, Mode.Fixed, -2, "2024-07-04T01:00:00", "yyyy-MM-ddTHH:mm:ss", "2024-07-06T01:00:00")]
    [TestCase(TimeUnit.Week, Mode.Fixed, 1, "2024-07-13T01:00:00", "yyyy-MM-ddTHH:mm:ss", "2024-07-06T01:00:00")]
    [TestCase(TimeUnit.Week, Mode.Fixed, -1, "2024-06-29T01:00:00", "yyyy-MM-ddTHH:mm:ss", "2024-07-06T01:00:00")]
    [TestCase(TimeUnit.Month, Mode.Start, 1, "2024-08-01T00:00:00", "yyyy-MM-ddTHH:mm:ss", "2024-08-06T23:00:00")]
    [TestCase(TimeUnit.Month, Mode.Fixed, 1, "2024-08-06T01:00:00", "yyyy-MM-ddTHH:mm:ss", "2024-07-06T01:00:00")]
    [TestCase(TimeUnit.Month, Mode.End, 1, "2024-08-31T23:59:59", "yyyy-MM-ddTHH:mm:ss", "2024-08-06T23:59:59")]
    [TestCase(TimeUnit.Month, Mode.Fixed, -1, "2024-06-06T01:00:00", "yyyy-MM-ddTHH:mm:ss", "2024-07-06T01:00:00")]
    [TestCase(TimeUnit.Year, Mode.Start, 56, "2024-01-01T00:00:00", "yyyy-MM-ddTHH:mm:ss", "2024-07-06T00:00:00")]
    [TestCase(TimeUnit.Year, Mode.Fixed, 0, "2024-07-06T01:00:00", "yyyy-MM-ddTHH:mm:ss", "2024-07-06T01:00:00")]
    [TestCase(TimeUnit.Year, Mode.End, 1, "2024-12-31T23:59:59", "yyyy-MM-ddTHH:mm:ss", "2024-07-06T23:59:59")]
    [TestCase(TimeUnit.Year, Mode.Fixed, -1, "2023-07-06T01:00:00", "yyyy-MM-ddTHH:mm:ss", "2024-07-06T01:00:00")]
    public void ToFormattedDateTime_ValidInputs_ReturnsCorrectDateTime(TimeUnit unit, Mode mode, int amount, string expectedDateTime, string format, string referenceDateStr)
    {
        // Arrange
        var referenceDate = DateTime.ParseExact(referenceDateStr, format, null);
        var timeRangePart = new TimeRangePart { Unit = unit, Mode = mode, Amount = amount };

        // Act
        var resultDateTime = timeRangePart.ToFormattedDateTime(referenceDate, format);

        // Assert
        Assert.That(resultDateTime, Is.EqualTo(expectedDateTime));
    }
    
}
