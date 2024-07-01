using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Models;

namespace POC.Infrastructure.Common.utils;

//UTIL CLASS THAT HANDLES THE FILTERING HAVING THE ORDER LIST AND THE PROFILE ASKING FOR THAT LIST
public static class OrdersFiltering
{
    private static string _format = "yyyy-MM-ddTHH:mm";
    
    //TODO notice that it already assumes here the company id of orders.
    public static List<OrderDto> FilterOrderDtoList(IEnumerable<OrderDto> orders, ScreenProfileFiltering screenProfileFiltering)
    {
        var filteredOrders = orders.Where(order => OrderMatchesFilter(order, screenProfileFiltering.OrderFiltering)).ToList();
        return filteredOrders;
    }
    
    public static bool OrderMatchesFilter(OrderDto order, OrderFiltering orderFiltering)
    {
        return  (IsBetween(order.StartDate, orderFiltering.From) || IsBetween(order.EndDate, orderFiltering.From)) &&
                (orderFiltering.OrderStatuses == null || orderFiltering.OrderStatuses.Contains(order.Status)) &&
                (orderFiltering.IsPickup == null || orderFiltering.IsPickup == order.IsPickup) &&
                (orderFiltering.EntityIds == null || orderFiltering.EntityIds.Contains(order.DepartmentId));
    }
    
    private static bool IsBetween(DateTime date, TimeRangePart timeRangePart)
    {
        var (start, end) = timeRangePart.ToFormattedDateTime(DateTime.Now, _format);
        var startDate = DateTime.ParseExact(start, _format, null);
        var endDate = DateTime.ParseExact(end, _format, null);
        return date >= startDate && date <= endDate;
    }
    
}