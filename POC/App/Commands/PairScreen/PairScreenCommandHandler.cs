using MediatR;
using POC.Api.Hubs;
using POC.Contracts.Screen;
using POC.Infrastructure.Models;
using POC.Infrastructure.Repositories;
using POC.Api.Hubs;

namespace POC.App.Commands.PairScreen;

public class PairScreenCommandHandler(
    GuestHub hub,
    ScreenProfileRepository screenProfileRepository,
    ScreenRepository screenRepository)
    : IRequestHandler<PairScreenCommand, ScreenDto>
{
    
    //private readonly GuestHub _hub = hub;


    public async Task<ScreenDto> Handle(PairScreenCommand request, CancellationToken cancellationToken)
    {
        //does the screen with this ip exist in the guest hub?
        var exists = await hub.IsIpConnected(request.PairScreenDto.IpAddress);
        //exists = await _hub.IsScreenConnected(request.PairScreenDto.IpAddress);
        if (exists)
        {
            var screenProfile = await screenProfileRepository.GetByIdAsync(request.PairScreenDto.ScreenProfileId);
            var screen = new Screen
            {
                IpAddress = request.PairScreenDto.IpAddress,
                ScreenProfileId = request.PairScreenDto.ScreenProfileId,
                ScreenProfile = screenProfile
            };
            await hub.OnConnect();
            await screenRepository.AddAsync(screen);
            screenProfile.Screens.Add(screen);
            await screenProfileRepository.UpdateAsync(screenProfile);
            //await _hub.NotifyScreenPaired(screen);
            await hub.SendMessageToIp(request.PairScreenDto.IpAddress, "Screen paired successfully!");
            var screenDto = new ScreenDto
            {
                Id = screen.Id,
                Ip = screen.IpAddress,
            };
            return screenDto;
        }
        return null;
    }
}