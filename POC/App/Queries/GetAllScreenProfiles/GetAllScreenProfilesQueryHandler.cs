using MediatR;
using POC.Contracts.ScreenProfile;
using POC.Infrastructure.Repositories;

namespace POC.App.Queries.GetAllScreenProfiles;

public class GetAllScreenProfilesQueryHandler : IRequestHandler<GetAllScreenProfilesQuery, List<ScreenProfileDto>>
{
    private readonly ScreenProfileRepository _repository;

    public GetAllScreenProfilesQueryHandler(ScreenProfileRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ScreenProfileDto>> Handle(GetAllScreenProfilesQuery request, CancellationToken cancellationToken)
    {
        var screenProfiles = await _repository.GetAllAsync();
        var screenProfileDtos = new List<ScreenProfileDto>();

        foreach (var profile in screenProfiles)
        {
            screenProfileDtos.Add(new ScreenProfileDto
            {
                Id = profile.Id,
                Name = profile.Name,
                // Map other properties as necessary
            });
        }

        return screenProfileDtos;
    }
}