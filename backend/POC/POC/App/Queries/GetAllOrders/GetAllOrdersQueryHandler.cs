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

    // TODO: Implement CRM adapter and related classes
    
    //THIS IS THE OLD WAY BY MAKING A FILTERED REQUEST ON OUR SIDE AND SENDING TO CRM
    public async Task<List<OrderDto>> Handle2(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        int profileId = request.profileId;
        int companyId = request.companyId;
        
        var screenProfile = await screenProfileRepository.GetByIdAsync(profileId);
        var temp = screenProfile.ScreenProfileFiltering;
        
        var query = temp.ToSearchRequest(); //translating into a query request object
        
        return await crmAdapter.GetAllOrdersAsync(companyId, query);
    }
    
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