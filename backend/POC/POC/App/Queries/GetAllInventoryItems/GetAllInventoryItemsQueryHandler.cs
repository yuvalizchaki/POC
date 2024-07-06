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
    : IRequestHandler<GetAllInventoryItemsQuery, List<InventoryItemDto>>
{

    //NEW WAY BY AGGREGATING THE CACHE ORDERS INTO INVENTORY ITEMS
    public async Task<List<InventoryItemDto>> Handle(GetAllInventoryItemsQuery request,
        CancellationToken cancellationToken)
    {
        int profileId = request.profileId;
        int companyId = request.companyId;

        var screenProfile = await screenProfileRepository.GetByIdAsync(profileId);
        var filtering = screenProfile.ScreenProfileFiltering;

        var orders = await orderRepository.GetAllOrdersAsync(companyId);

        var filteredOrders = orders.Where(o => filtering.IsMatch(o)).ToList();

        //orders have orderItems that should be aggregated according to inventory filters
        var inventoryItems = filteredOrders
            .SelectMany(order => order.OrderItems.Where(orderItem => filtering.IsInventoryMatch(orderItem)))
            .ToList();

        if (screenProfile.ScreenProfileFiltering.InventorySorting != null)
        {
            inventoryItems = SortingUtility.SortItemsByFieldNames(inventoryItems, screenProfile.ScreenProfileFiltering.InventorySorting).ToList();
        }
        
        return inventoryItems;
    }
}