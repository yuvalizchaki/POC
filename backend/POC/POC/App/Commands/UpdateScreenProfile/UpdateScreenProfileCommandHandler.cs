using MediatR;
using POC.Api.Hubs;
using POC.Contracts.Screen;
using POC.Contracts.ScreenProfile;
using POC.Infrastructure.Common.Exceptions;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.Models;
using POC.Infrastructure.Repositories;

namespace POC.App.Commands.UpdateScreenProfile;

public class UpdateScreenProfileCommandHandler(
    ScreenProfileRepository repository,
    ScreenHub screenHub)
    : IRequestHandler<UpdateScreenProfileCommand, ScreenProfileDto>
{
    public async Task<ScreenProfileDto> Handle(UpdateScreenProfileCommand request, CancellationToken cancellationToken)
    {
        var screenToUpdate = await repository.GetByIdAsync(request.Id);
        
        if (screenToUpdate == null) throw new ScreenProfileNotFoundException();
        
        screenToUpdate.Name = request.ScreenProfile.Name;
        screenToUpdate.ScreenProfileFiltering = request.ScreenProfile.ScreenProfileFiltering.ToScreenProfileFiltering();
        
        await repository.UpdateAsync(screenToUpdate);
        
        await screenHub.NotifyUpdateProfile(screenToUpdate.Screens.Select(s => s.Id).ToArray());
        
        return screenToUpdate.ToScreenProfileDto();
    }
}