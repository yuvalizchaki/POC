using MediatR;
using POC.Contracts.ScreenProfile;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.Models;
using POC.Infrastructure.Repositories;

namespace POC.App.Commands.CreateScreenProfile;

public class CreateScreenProfileCommandHandler(ScreenProfileRepository repository)
    : IRequestHandler<CreateScreenProfileCommand, ScreenProfileDto>
{
    public async Task<ScreenProfileDto> Handle(CreateScreenProfileCommand request, CancellationToken cancellationToken)
    {

        var screenProfile = new ScreenProfile
        {
            Name = request.CreateScreenProfileDto.Name,
            // Other properties
        };
        
        await repository.AddAsync(screenProfile);

        return screenProfile.ToScreenProfileDto();
    }
}
