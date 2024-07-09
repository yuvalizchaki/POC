using MediatR;
using POC.Api.Hubs;
using POC.Contracts.ScreenProfile;
using POC.Infrastructure.Common.Exceptions;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.Repositories;

namespace POC.App.Commands.DeleteScreenProfile;

public class DeleteScreenProfileCommandHandler(ScreenProfileRepository repository,
    AdminHub adminHub
) : IRequestHandler<DeleteScreenProfileCommand>
{
    public async Task Handle(DeleteScreenProfileCommand request, CancellationToken cancellationToken)
    {
        var screenProfile = await repository.GetByIdAsync(request.Id);
        
        var result = await repository.DeleteAsync(request.Id);
        if (!result) throw new ScreenProfileNotFoundException();
        
        await adminHub.RemoveScreens(screenProfile.Screens.Select(s => s.ToScreenDto()).ToArray());
    }
}