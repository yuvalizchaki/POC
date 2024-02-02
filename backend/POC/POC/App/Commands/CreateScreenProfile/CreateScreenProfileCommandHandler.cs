using MediatR;
using POC.Contracts.ScreenProfile;
using POC.Infrastructure.Models;
using POC.Infrastructure.Repositories;

namespace POC.App.Commands.CreateScreenProfile;

public class CreateScreenProfileCommandHandler : IRequestHandler<CreateScreenProfileCommand, ScreenProfileDto>
{
    private readonly ScreenProfileRepository _repository;

    public CreateScreenProfileCommandHandler(ScreenProfileRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ScreenProfileDto> Handle(CreateScreenProfileCommand request, CancellationToken cancellationToken)
    {
        // Convert Dto to ScreenProfile model
        var screenProfile = new ScreenProfile
        {
            Name = request.CreateScreenProfileDto.Name,
            // Other properties
        };
        // Add to repository
        await _repository.AddAsync(screenProfile);
        // Convert added ScreenProfile back to Dto (assuming you have a method or logic to do so)
        var screenProfileDto = new ScreenProfileDto
        {
            Id = screenProfile.Id,
            Name = screenProfile.Name,
            // Other properties
        };

        return screenProfileDto;
    }
}
