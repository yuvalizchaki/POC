using MediatR;
using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Models;

namespace POC.App.Queries.GetAllInventoryItems;

public class GetAllInventoryItemsQuery : IRequest<List<InventoryItemDto>>
{
    public int profileId { get; set; }
    public int companyId { get; set; }

    public GetAllInventoryItemsQuery(int profileId, int companyId)
    {
        this.profileId = profileId;
        this.companyId = companyId;
    }
}