using POC.Infrastructure.Repositories;
using MediatR;
using POC.App.Queries.GetAllScreens;
using POC.Contracts.Screen;
using POC.Infrastructure.Extensions;


namespace POC.App.Queries.GetAllConnectedScreens;

public class GetAllConnectedScreensHandler(ScreenConnectionRepository screenConnectionRepository, ScreenRepository screenRepository)
    : IRequestHandler<GetAllConnectedScreensQuery, List<ScreenDto>>
{
    public async Task<List<ScreenDto>> Handle(GetAllConnectedScreensQuery request, CancellationToken cancellationToken)
    {
        var connectedScreenIds = await screenConnectionRepository.GetConnectedScreensAsync();
        var allScreens = await screenRepository.GetAllAsync();
        var filteredScreens = allScreens.Where(screen => connectedScreenIds.Contains(screen.Id)).ToList();
        var screenDtos = filteredScreens.Select(s => s.ToScreenDto()).ToList();
    
        return screenDtos;
    }

}