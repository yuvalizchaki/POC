using MediatR;
using Newtonsoft.Json;
using POC.App.Queries.GetAllScreenProfiles;
using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Adapters;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.Repositories;

namespace POC.App.Queries.GetAllOrders;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
{
    private readonly CrmAdapter _crmAdapter;
    private readonly ScreenProfileRepository _screenProfileRepository;
    
    
    public GetAllOrdersQueryHandler(CrmAdapter crmAdapter, ScreenProfileRepository screenProfileRepository)
    {
        _crmAdapter = crmAdapter;
        _screenProfileRepository = screenProfileRepository;
    }
    
    
    // TODO: Implement CRM adapter and related classes
    public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        int profileId = 5;
        int companyId = 1;
        
        var screenProfile = await _screenProfileRepository.GetByIdAsync(profileId);
        var temp = screenProfile.ScreenProfileFiltering;
        
        var query = temp.ToSearchRequest(); //translating into a query request object
        
        return await _crmAdapter.GetAllOrdersAsync(companyId, query);

    }


}