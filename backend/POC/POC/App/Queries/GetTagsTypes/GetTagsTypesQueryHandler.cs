using MediatR;
using POC.Infrastructure.Adapters;

namespace POC.App.Queries.GetTagsTypes;

public class GetTagsTypesQueryHandler(
    ITypesAdapter typesAdapter
    )
    : IRequestHandler<GetTagsTypesQuery, String>
{
    public Task<string> Handle(GetTagsTypesQuery request, CancellationToken cancellationToken)
    {
        return typesAdapter.FetchTagsTypesAsync();
    }
}