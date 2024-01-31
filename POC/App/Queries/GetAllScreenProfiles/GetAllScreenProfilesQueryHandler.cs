using MediatR;
using POC.Contracts.Screen;
using POC.Contracts.ScreenProfile;
using POC.Infrastructure.Repositories;

namespace POC.App.Queries.GetAllScreenProfiles;

public class GetAllScreenProfilesQueryHandler(ScreenProfileRepository repository)
    : IRequestHandler<GetAllScreenProfilesQuery, List<ScreenProfileDto>>
{
    public async Task<List<ScreenProfileDto>> Handle(GetAllScreenProfilesQuery request, CancellationToken cancellationToken)
    {
        var screenProfiles = await repository.GetAllAsync();
        var screenProfileDtos = new List<ScreenProfileDto>();

        foreach (var profile in screenProfiles)
        {
            screenProfileDtos.Add(new ScreenProfileDto
            {
                Id = profile.Id,
                Name = profile.Name,
                Screens = profile.Screens.Select(s => new ScreenDto
                {
                    Id = s.Id,
                    Ip = s.IpAddress,
                }).ToList()
                // Map other properties as necessary
            });
        }

        return screenProfileDtos;
    }
}