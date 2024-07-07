using POC.Infrastructure.Common.Constants;
using POC.Infrastructure.Common.utils;

namespace UnitTests;

[TestFixture]
public class DateAdjusterTests
{
    
    [Test]
    [TestCase("2023-07-05T15:30:00", TimeUnit.Day, "2023-07-05T00:00:00")]
    [TestCase("2023-07-05T15:30:00", TimeUnit.Week, "2023-07-02T00:00:00")]
    [TestCase("2023-07-15T15:30:00", TimeUnit.Month, "2023-07-01T00:00:00")]
    [TestCase("2023-07-15T15:30:00", TimeUnit.Year, "2023-01-01T00:00:00")]
    public void AdjustToStart_ReturnsCorrectStart(string referenceDateString, TimeUnit unit, string expectedDateString)
    {
        // Arrange
        var referenceDate = DateTime.Parse(referenceDateString);
        var expectedDate = DateTime.Parse(expectedDateString);

        // Act
        var result = DateAdjusterUtility.AdjustToStart(referenceDate, unit);

        // Assert
        Assert.That(result, Is.EqualTo(expectedDate));
    }

    [Test]
    [TestCase("2023-07-05T15:30:00", TimeUnit.Day, "2023-07-06T00:00:00")]
    [TestCase("2023-07-05T15:30:00", TimeUnit.Week, "2023-07-09T00:00:00")]
    [TestCase("2023-07-15T15:30:00", TimeUnit.Month, "2023-08-01T00:00:00")]
    [TestCase("2023-07-15T15:30:00", TimeUnit.Year, "2024-01-01T00:00:00")]
    public void AdjustToEnd_ReturnsCorrectEnd(string referenceDateString, TimeUnit unit, string expectedDateString)
    {
        // Arrange
        var referenceDate = DateTime.Parse(referenceDateString);
        var expectedDate = DateTime.Parse(expectedDateString).AddTicks(-1);

        // Act
        var result = DateAdjusterUtility.AdjustToEnd(referenceDate, unit);

        // Assert
        Assert.That(result, Is.EqualTo(expectedDate));
    }
    
}
