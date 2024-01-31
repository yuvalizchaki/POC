using MediatR;
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
        var screenProfileDto = new ScreenProfileDto
        {
            Id = screenProfile.Id,
            Name = screenProfile.Name,
            // Other properties
        };
        
        return screenProfileDto;
    }
}