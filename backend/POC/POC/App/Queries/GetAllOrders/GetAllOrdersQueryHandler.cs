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
    IOrderAdapter crmAdapter,
    ScreenProfileRepository screenProfileRepository,
    IOrderRepository orderRepository)
    : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
{

    //THIS IS THE NEW WAY BY CACHING THE ORDERS ON OUR SIDE AND FILTERING ON OUR SIDE
    public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var profileId = request.profileId;
        var companyId = request.companyId;

        var screenProfile = await screenProfileRepository.GetByIdAsync(profileId);
        var filtering = screenProfile?.ScreenProfileFiltering;

        if (filtering == null || !filtering.IsProfileInterestedInOrders())
        {
            throw new Exception("This Screen Profile is not interested in orders");
        }
        
        var crmOrders = await orderRepository.GetAllOrdersAsync(companyId);

        var orders = crmOrders.ToOrderDtoList();
        
        var filteredOrders = orders.Where(o => filtering.IsOrderMatch(o)).ToList();
        
        return filteredOrders;
    }

}