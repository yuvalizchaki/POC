using MediatR;
using Newtonsoft.Json;
using POC.App.Queries.GetAllScreenProfiles;
using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Adapters;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.IRepositories;
using POC.Infrastructure.Repositories;

namespace POC.App.Queries.GetAllOrders;

public class GetAllOrdersQueryHandler(
    CrmAdapter crmAdapter,
    ScreenProfileRepository screenProfileRepository,
    IOrderRepository orderRepository)
    : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
{
    
    //THIS IS THE NEW WAY BY CACHING THE ORDERS ON OUR SIDE AND FILTERING ON OUR SIDE
    public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        int profileId = request.profileId;
        int companyId = request.companyId;
        
        var screenProfile = await screenProfileRepository.GetByIdAsync(profileId);
        var filtering = screenProfile.ScreenProfileFiltering;
        
        var orders = await orderRepository.GetAllOrdersAsync(companyId);
        
        var filteredOrders = orders.Where(o => filtering.IsMatch(o)).ToList();
        
        return filteredOrders;
    }


}