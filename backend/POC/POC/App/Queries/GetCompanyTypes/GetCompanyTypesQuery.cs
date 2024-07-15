using MediatR;

namespace POC.App.Queries.GetCompanyTypes;

public class GetCompanyTypesQuery(
    int companyId
    ) : IRequest<String>
{
    public int CompanyId { get; set; } = companyId;
}