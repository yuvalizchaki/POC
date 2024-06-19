using MediatR;
using POC.App.Queries.GetAllOrders;
using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Adapters;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.Models;
using POC.Infrastructure.Repositories;

namespace POC.App.Queries.GetAllInventoryItems;

public class GetAllInventoryItemsQueryHandler : IRequestHandler<GetAllInventoryItemsQuery, List<InventoryItemDto>>
{
    private readonly CrmAdapter _crmAdapter;
    private readonly ScreenProfileRepository _screenProfileRepository;
        
        
    public GetAllInventoryItemsQueryHandler(CrmAdapter crmAdapter, ScreenProfileRepository screenProfileRepository)
    {
        _crmAdapter = crmAdapter;
        _screenProfileRepository = screenProfileRepository;
    }
        
        
    // TODO: Implement CRM adapter and related classes
    public async Task<List<InventoryItemDto>> Handle(GetAllInventoryItemsQuery request, CancellationToken cancellationToken)
    {
        int profileId = request.profileId;
        int companyId = request.companyId;
            
        var screenProfile = await _screenProfileRepository.GetByIdAsync(profileId);
        var temp = screenProfile.ScreenProfileFiltering;
            
        var query = temp.ToSearchRequest(); //translating into a query request object
            
        return await _crmAdapter.GetAllInventoryItemsAsync(companyId, query);

    }


}