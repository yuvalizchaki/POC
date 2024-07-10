using MediatR;
using POC.App.Queries.GetAllScreens;
using POC.Contracts.CrmDTOs;
using POC.Contracts.Screen;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.Repositories;

namespace POC.App.Queries.GetMetadata;

public class GetMetadataQueryHandler(
    ScreenRepository screenRepository
    )
    : IRequestHandler<GetMetadataQuery, MetadataDto>
{

    public async Task<MetadataDto> Handle(GetMetadataQuery request, CancellationToken cancellationToken)
    {
        var screen = await screenRepository.GetByIdAsync(request.ScreenId);
        var screenProfile = screen.ScreenProfile;
        var metaData = new MetadataDto
        {
            DisplayConfig = screenProfile.ScreenProfileFiltering.DisplayConfig,
            IsInventory = screenProfile.ScreenProfileFiltering.InventoryFiltering != null,
            Name = screen.Name,
            ScreenId = screen.Id
        };
        return metaData;
    }
}