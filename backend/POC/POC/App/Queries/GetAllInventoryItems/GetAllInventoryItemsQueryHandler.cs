using System.Reflection;
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
    CrmAdapter crmAdapter,
    ScreenProfileRepository screenProfileRepository,
    IOrderRepository orderRepository)
    : IRequestHandler<GetAllInventoryItemsQuery, List<InventoryItemsResponse>>
{

    //NEW WAY BY AGGREGATING THE CACHE ORDERS INTO INVENTORY ITEMS
    public async Task<List<InventoryItemsResponse>> Handle(GetAllInventoryItemsQuery request,
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

        var orders = await orderRepository.GetAllOrdersAsync(companyId);

        var filteredOrders = orders.Where(o => filtering.IsOrderMatch(o)).ToList();

        //orders have orderItems that should be aggregated according to inventory filters
        var inventoryItems = filteredOrders
            .SelectMany(order => order.OrderItems.Where(orderItem => filtering.IsInventoryMatch(orderItem)))
            .ToList();

        if (screenProfile?.ScreenProfileFiltering.InventorySorting != null)
        {
            inventoryItems = SortingUtility.SortItemsByFieldNames(inventoryItems, screenProfile.ScreenProfileFiltering.InventorySorting).ToList();
        }
        
        return inventoryItems
            .GroupBy(item => item.Id)
            .Select(group => new InventoryItemsResponse
            {
                InventoryItem = group.First(),
                Quantity = group.Count()
            })
            .OrderBy(item => inventoryItems.IndexOf(item.InventoryItem))
            .ToList();
    }
}