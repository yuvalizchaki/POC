using MediatR;
using POC.Contracts.Screen;
using POC.Infrastructure.Repositories;

namespace POC.App.Queries.GetAllScreens;

public class GetAllScreensQueryHandler(ScreenRepository screenRepository)
    : IRequestHandler<GetAllScreensQuery, List<ScreenDto>>
{
    private readonly ScreenRepository _screenRepository = screenRepository;

    public async Task<List<ScreenDto>> Handle(GetAllScreensQuery request, CancellationToken cancellationToken)
    {
        var screens = await _screenRepository.GetAllAsync();
        var screenDtos = screens.Select(s => new ScreenDto
        {
            Id = s.Id,
            Ip = s.IpAddress
            // TODO ADD Other properties to both screen model and screenDto
        }).ToList();
        return screenDtos;
    }
}