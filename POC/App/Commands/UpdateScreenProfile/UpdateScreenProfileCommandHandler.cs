using MediatR;
using POC.Contracts.Screen;
using POC.Contracts.ScreenProfile;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.Models;
using POC.Infrastructure.Repositories;

namespace POC.App.Commands.UpdateScreenProfile;

public class UpdateScreenProfileCommandHandler(ScreenProfileRepository repository)
    : IRequestHandler<UpdateScreenProfileCommand, ScreenProfileDto>
{
    public async Task<ScreenProfileDto> Handle(UpdateScreenProfileCommand request, CancellationToken cancellationToken)
    {
        var updatedScreenProfile = new ScreenProfile
        {
            Id = request.Id,
            Name = request.ScreenProfile.Name,
            Screens = request.ScreenProfile.Screens.Select(s => new Screen
            {
                Id = s.Id,
                IpAddress = s.Ip,
                ScreenProfileId = s.ScreenProfileId,
                // TODO ADD Other properties to both screen model and screenDto
            }).ToList(),
        };
        await repository.UpdateAsync(updatedScreenProfile);
        
        var updatedScreenProfileDto = new ScreenProfileDto
        {
            Id = updatedScreenProfile.Id,
            Name = updatedScreenProfile.Name,
            Screens = updatedScreenProfile.Screens.Select(s => s.ToScreenDto()).ToList()
        };
        
        return updatedScreenProfileDto;
    }
}