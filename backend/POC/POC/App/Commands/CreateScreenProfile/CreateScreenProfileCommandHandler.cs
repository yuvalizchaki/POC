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
            CompanyId = request.CreateScreenProfileDto.CompanyId,
            ScreenProfileFiltering = new ScreenProfileFiltering
            {
                OrderTimeRange = new OrderTimeRange
                {
                    StartDate = request.CreateScreenProfileDto.ScreenProfileFiltering.OrderTimeRange.StartDate,
                    EndDate = request.CreateScreenProfileDto.ScreenProfileFiltering.OrderTimeRange.EndDate
                },
                OrderStatusses = request.CreateScreenProfileDto.ScreenProfileFiltering.OrderStatusses,
                IsPickup = request.CreateScreenProfileDto.ScreenProfileFiltering.IsPickup,
                IsSale = request.CreateScreenProfileDto.ScreenProfileFiltering.IsSale,
                EntityIds = request.CreateScreenProfileDto.ScreenProfileFiltering.EntityIds
            },
            
            // Other properties
        };
        
        await repository.AddAsync(screenProfile);

        return screenProfile.ToScreenProfileDto();
    }
}
