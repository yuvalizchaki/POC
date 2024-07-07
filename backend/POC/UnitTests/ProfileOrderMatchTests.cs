using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Common.Constants;
using POC.Infrastructure.Common.utils;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.Models;

namespace UnitTests;


[TestFixture]
public class ProfileOrderMatchTests
{
    [Test]
    public void IsMatch_OrderMatchesFiltering_ReturnsTrue()
    {
        // Arrange
        var screenProfileFiltering = new ScreenProfileFiltering
        {
            OrderFiltering = new OrderFiltering
            {
                TimeRanges = new TimeEncapsulated {
                    From = new TimeRangePart()
                        {Unit = TimeUnit.Day, Mode = Mode.Start, Amount = 0 },
                    To = new TimeRangePart()
                        {Unit = TimeUnit.Day, Mode = Mode.End, Amount = 0 }
                },
                OrderStatuses = new List<OrderStatus> { OrderStatus.Completed },
                IsPickup = true,
                EntityIds = new List<int> { 1, 2, 3 }
            }
        };
        var order = new OrderDto
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddHours(1),
            Status = OrderStatus.Completed,
            IsPickup = true,
            DepartmentId = 2
        };

        // Act
        var result = screenProfileFiltering.IsOrderMatch(order);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void IsMatch_OrderDoesNotMatchFiltering_ReturnsFalse()
    {
        // Arrange
        var screenProfileFiltering = new ScreenProfileFiltering
        {
            OrderFiltering = new OrderFiltering
            {
                TimeRanges = new TimeEncapsulated {
                    From = new TimeRangePart()
                        {Unit = TimeUnit.Day, Mode = Mode.Start, Amount = 0 },
                    To = new TimeRangePart()
                        {Unit = TimeUnit.Day, Mode = Mode.End, Amount = 0 }
                },
                OrderStatuses = new List<OrderStatus> { OrderStatus.Completed },
                IsPickup = true,
                EntityIds = new List<int> { 1, 2, 3 }
            }
        };
        var order = new OrderDto
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddHours(1),
            Status = OrderStatus.Completed,
            IsPickup = false,
            DepartmentId = 4
        };

        // Act
        var result = screenProfileFiltering.IsOrderMatch(order);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void IsInventoryMatch_InventoryItemMatchesFiltering_ReturnsTrue()
    {
        // Arrange
        var screenProfileFiltering = new ScreenProfileFiltering
        {
            InventoryFiltering = new InventoryFiltering
            {
                EntityIds = new List<int> { 1, 2, 3 }
            }
        };
        var orderItem = new InventoryItemDto
        {
            DepartmentId = 2
        };

        // Act
        var result = screenProfileFiltering.IsInventoryMatch(orderItem);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void IsInventoryMatch_InventoryItemDoesNotMatchFiltering_ReturnsFalse()
    {
        // Arrange
        var screenProfileFiltering = new ScreenProfileFiltering
        {
            InventoryFiltering = new InventoryFiltering
            {
                EntityIds = new List<int> { 1, 2, 3 }
            }
        };
        var orderItem = new InventoryItemDto
        {
            DepartmentId = 4
        };

        // Act
        var result = screenProfileFiltering.IsInventoryMatch(orderItem);

        // Assert
        Assert.IsFalse(result);
    }
    
}