using MediatR;
using POC.Api.Hubs;
using POC.Contracts.Screen;
using POC.Infrastructure.Models;
using POC.Infrastructure.Repositories;
using POC.Api.Hubs;
using POC.Contracts;
using POC.Infrastructure.Common.Exceptions;
using POC.Infrastructure.Extensions;

namespace POC.App.Commands.PairScreen;

public class PairScreenCommandHandler(
    GuestHub hub,
    ScreenProfileRepository screenProfileRepository,
    ScreenRepository screenRepository)
    : IRequestHandler<PairScreenCommand, ScreenDto>
{

    public async Task<ScreenDto> Handle(PairScreenCommand request, CancellationToken cancellationToken)
    {
        var screens = await screenRepository.GetAllAsync();
        var s = screens.FirstOrDefault(s => s.IpAddress == request.PairScreenDto.IpAddress);
        if (s != null) throw new ScreenAlreadyPairedException();
        
        var exists = await hub.IsIpConnected(request.PairScreenDto.IpAddress);
        if (!exists) throw new IpNotInGuestHubException();
        
        var screenProfile = await screenProfileRepository.GetByIdAsync(request.PairScreenDto.ScreenProfileId);
        if (screenProfile == null) throw new ScreenProfileNotFoundException();
        
        var screen = new Screen
        {
            IpAddress = request.PairScreenDto.IpAddress,
            ScreenProfileId = request.PairScreenDto.ScreenProfileId,
            ScreenProfile = screenProfile
        };
        await screenRepository.AddAsync(screen);
        
        screenProfile.Screens.Add(screen);
        await screenProfileRepository.UpdateAsync(screenProfile);
        
        var screenDto = screen.ToScreenDto();
        await hub.SendMessageAddScreen(request.PairScreenDto.IpAddress, screenDto);
        
        return screenDto;
    }
}