using System.Reflection;
using System.Text.RegularExpressions;
using MediatR;
using POC.App.Queries.GetAllOrders;
using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Adapters;
using POC.Infrastructure.Common.utils;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.IRepositories;
using POC.Infrastructure.Models;
using POC.Infrastructure.Repositories;

namespace POC.App.Queries.GetAllInventoryItems;

public class GetAllInventoryItemsQueryHandler(
    ScreenProfileRepository screenProfileRepository,
    IOrderRepository orderRepository)
    : IRequestHandler<GetAllInventoryItemsQuery, List<InventoryItemDto>>
{

    //NEW WAY BY AGGREGATING THE CACHE ORDERS INTO INVENTORY ITEMS
    public async Task<List<InventoryItemDto>> Handle(GetAllInventoryItemsQuery request,
        CancellationToken cancellationToken)
    {
        var profileId = request.profileId;
        var companyId = request.companyId;

        var screenProfile = await screenProfileRepository.GetByIdAsync(profileId);
        var filtering = screenProfile?.ScreenProfileFiltering;
        
        if (filtering == null || !filtering.IsProfileInterestedInInventoryItems())
        {
            throw new Exception("This Screen Profile is not interested in inventory items");
        }

        var crmOrders = await orderRepository.GetAllOrdersAsync(companyId);

        var orders = crmOrders.ToOrderDtoList();

        var filteredOrders = orders.Where(o => filtering.IsOrderMatch(o)).ToList();

        //orders have orderItems that should be aggregated according to inventory filters
        var inventoryItems = filteredOrders
            .SelectMany(order =>
            {
                var relevantInventoryItems = order.CrmOrder.OrderItems.Where(orderItem => filtering.IsInventoryMatch(orderItem));
                var inventoryItems = relevantInventoryItems.Select(orderItem =>
                    new InventoryItemDto
                    {
                        CrmInventoryItem = orderItem,
                        TransportType = order.TransportType
                    }
                );
                return inventoryItems;
            }
            )
            .ToList();

        if (screenProfile?.ScreenProfileFiltering.InventorySorting != null)
        {
            inventoryItems = SortingUtility.SortItemsByFieldNames(inventoryItems, screenProfile.ScreenProfileFiltering.InventorySorting).ToList();
        }
        
        return inventoryItems
            .GroupBy(item => new { item.CrmInventoryItem.Id, item.TransportType })
            .Select(group =>
            {
                var firstItem = group.First();
                firstItem.CrmInventoryItem.Amount = group.Sum(item => item.CrmInventoryItem.Amount);
                return firstItem;
            })
            .OrderBy(item => inventoryItems.IndexOf(item))
            .ToList();
    }
}