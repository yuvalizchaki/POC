using MediatR;

namespace POC.App.Queries.GetTagsTypes;

public class GetTagsTypesQuery(
    int companyId
    ) : IRequest<String>
{
    public int CompanyId { get; set; } = companyId;
}