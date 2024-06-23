using System.Security.Cryptography;
using System.Text;
using MediatR;
using POC.Api.Hubs;
using POC.App.Commands.PairScreen;
using POC.Contracts.Screen;
using POC.Infrastructure.Common.Exceptions;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.IRepositories;
using POC.Infrastructure.Models;
using POC.Infrastructure.Repositories;
using POC.Services;

public class PairScreenCommandHandler : IRequestHandler<PairScreenCommand, ScreenDto>
{
    private readonly GuestHub _hub;
    private readonly ScreenProfileRepository _screenProfileRepository;
    private readonly ScreenRepository _screenRepository;
    private readonly IGuestConnectionRepository _guestConnectionRepository;

    public PairScreenCommandHandler(GuestHub hub, ScreenProfileRepository screenProfileRepository, 
                                    ScreenRepository screenRepository, IGuestConnectionRepository guestConnectionRepository)
    {
        _hub = hub;
        _screenProfileRepository = screenProfileRepository;
        _screenRepository = screenRepository;
        _guestConnectionRepository = guestConnectionRepository;
    }

    public async Task<ScreenDto> Handle(PairScreenCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the screen profile
        var screenProfile = await _screenProfileRepository.GetByIdAsync(request.PairScreenDto.ScreenProfileId);
        if (screenProfile == null)
            throw new ScreenProfileNotFoundException();

        // Check if the pairing code exists in the cache
        var connectionId = await _guestConnectionRepository.GetConnectionIdByCodeAsync(request.PairScreenDto.PairingCode, cancellationToken);
        if (string.IsNullOrEmpty(connectionId))
            throw new Exception("PairingCodeNotFoundException");
        
        // Create a new screen object
        var screen = new Screen
        {
            ScreenProfileId = request.PairScreenDto.ScreenProfileId,
            ScreenProfile = screenProfile
        };
        
        // Generate a token for the screen
        String token = AuthService.GenerateScreenToken(screen, screenProfile.CompanyId);
        
        // Hash the token   
        byte[] result = SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(token));
        screen.HashToken = Encoding.Default.GetString(result);
        
        // Add the screen to the repository
        await _screenRepository.AddAsync(screen);
        
        // Send the screen profile to the smart TV screen
        await _hub.SendMessageAddScreen(request.PairScreenDto.PairingCode, token);

        // Associate the screen with the screen profile
        screenProfile.Screens.Add(screen);
        await _screenProfileRepository.UpdateAsync(screenProfile);
        
        // Return the screen DTO with the not hashed token
        return screen.ToScreenDto();
    }
}
