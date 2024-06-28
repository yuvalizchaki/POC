using MediatR;
using POC.App.Queries.GetAllScreens;
using POC.Contracts.CrmDTOs;
using POC.Contracts.Screen;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.Repositories;

namespace POC.App.Queries.GetMetadata;

public class GetMetadataQueryHandler(ScreenProfileRepository screenProfileRepository)
    : IRequestHandler<GetMetadataQuery, MetadataDto>
{

    public async Task<MetadataDto> Handle(GetMetadataQuery request, CancellationToken cancellationToken)
    {
        var screenProfile = await screenProfileRepository.GetByIdAsync(request.Id);
        var metaData = new MetadataDto
        {
            DisplayConfig = screenProfile.ScreenProfileFiltering.DisplayConfig,
            IsInventory = screenProfile.ScreenProfileFiltering.InventoryFiltering != null
        };
        return metaData;
    }
}