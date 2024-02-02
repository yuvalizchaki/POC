using MediatR;
using POC.Contracts.Screen;
using POC.Contracts.ScreenProfile;
using POC.Infrastructure.Common.Exceptions;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.Repositories;

namespace POC.App.Queries.GetScreenProfile;

public class GetScreenProfileQueryHandler(ScreenProfileRepository repository)
    : IRequestHandler<GetScreenProfileQuery, ScreenProfileDto>
{
    public async Task<ScreenProfileDto> Handle(GetScreenProfileQuery request, CancellationToken cancellationToken)
    {
        var screenProfile = await repository.GetByIdAsync(request.Id);
        
        return screenProfile != null ? 
            screenProfile.ToScreenProfileDto() : throw new ScreenProfileNotFoundException();
    }
}