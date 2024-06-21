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
        
        var spDto = request.CreateScreenProfileDto;
        
        var screenProfile = new ScreenProfile
        {
            Name = request.CreateScreenProfileDto.Name,
            CompanyId = request.CreateScreenProfileDto.CompanyId,
            ScreenProfileFiltering = spDto.ScreenProfileFiltering.ToScreenProfileFiltering(),
            // Other properties
        };
        
        await repository.AddAsync(screenProfile);

        return screenProfile.ToScreenProfileDto();
    }
}
