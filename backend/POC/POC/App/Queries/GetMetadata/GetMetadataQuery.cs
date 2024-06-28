using MediatR;
using POC.Contracts.CrmDTOs;
using POC.Contracts.Screen;

namespace POC.App.Queries.GetMetadata;

public class GetMetadataQuery(int id) : IRequest<MetadataDto>
{
    public int Id { get; } = id;
}