using MediatR;
using POC.Contracts.Screen;
using POC.Contracts.ScreenProfile;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.Repositories;

namespace POC.App.Queries.GetAllScreenProfiles;

public class GetAllScreenProfilesQueryHandler(ScreenProfileRepository repository)
    : IRequestHandler<GetAllScreenProfilesQuery, List<ScreenProfileDto>>
{
    public async Task<List<ScreenProfileDto>> Handle(GetAllScreenProfilesQuery request, CancellationToken cancellationToken)
    {
        var screenProfiles = await repository.GetAllAsync();
        return screenProfiles.Select(s => s.ToScreenProfileDto()).ToList();
    }
}