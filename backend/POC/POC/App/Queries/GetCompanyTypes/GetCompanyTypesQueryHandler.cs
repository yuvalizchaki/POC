using MediatR;
using POC.Infrastructure.Adapters;

namespace POC.App.Queries.GetCompanyTypes;

public class GetCompanyTypesQueryHandler(
    ITypesAdapter typesAdapter
    ) : IRequestHandler<GetCompanyTypesQuery, String>
{
    public Task<string> Handle(GetCompanyTypesQuery request, CancellationToken cancellationToken)
    {
        return typesAdapter.FetchCompanyTypesAsync(request.CompanyId);
    }
}