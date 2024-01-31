using MediatR;
using POC.Contracts.Screen;
using POC.Contracts.ScreenProfile;
using POC.Infrastructure.Models;
using POC.Infrastructure.Repositories;

namespace POC.App.Commands.UpdateScreenProfile;

public class UpdateScreenProfileCommandHandler : IRequestHandler<UpdateScreenProfileCommand, ScreenProfileDto>
{
    
    private readonly ScreenProfileRepository _repository;

    public UpdateScreenProfileCommandHandler(ScreenProfileRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ScreenProfileDto> Handle(UpdateScreenProfileCommand request, CancellationToken cancellationToken)
    {
        var updatedScreenProfile = new ScreenProfile
        {
            Id = request.Id,
            Name = request.ScreenProfile.Name,
            Screens = request.ScreenProfile.Screens.Select(s => new Screen
            {
                Id = s.Id,
                // TODO ADD Other properties to both screen model and screenDto
            }).ToList(),
        };
        await _repository.UpdateAsync(updatedScreenProfile);
        
        var updatedScreenProfileDto = new ScreenProfileDto
        {
            Id = updatedScreenProfile.Id,
            Name = updatedScreenProfile.Name,
            Screens = updatedScreenProfile.Screens.Select(s => new ScreenDto
            {
                Id = s.Id,
                // TODO ADD Other properties to both screen model and screenDto
            }).ToList(),
        };
        
        return updatedScreenProfileDto;
    }
}