using MediatR;
using POC.Contracts.CrmDTOs;
using POC.Contracts.Screen;

namespace POC.App.Queries.GetMetadata;

public class GetMetadataQuery(int companyId, int screenId) : IRequest<MetadataDto>
{
    public int CompanyId { get; set; } = companyId;
    public int ScreenId { get; set; } = screenId;
}