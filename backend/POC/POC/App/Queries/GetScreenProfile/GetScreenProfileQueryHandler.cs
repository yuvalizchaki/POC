using MediatR;
using POC.Contracts.Screen;
using POC.Contracts.ScreenProfile;
using POC.Infrastructure.Repositories;

namespace POC.App.Queries.GetScreenProfile;

public class GetScreenProfileQueryHandler : IRequestHandler<GetScreenProfileQuery, ScreenProfileDto>
{
    
    private readonly ScreenProfileRepository _repository;

    public GetScreenProfileQueryHandler(ScreenProfileRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ScreenProfileDto> Handle(GetScreenProfileQuery request, CancellationToken cancellationToken)
    {
        var screenProfile = await _repository.GetByIdAsync(request.Id);
        return screenProfile == null ? null : 
        new ScreenProfileDto
        {
            Id = screenProfile.Id,
            Name = screenProfile.Name,
            Screens = screenProfile.Screens.Select(s => new ScreenDto
            {
                Id = s.Id,
                Ip = s.IpAddress,
            }).ToList()
            // Other properties
        };
    }
}