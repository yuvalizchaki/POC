using MediatR;
using POC.Contracts.Screen;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.Repositories;

namespace POC.App.Queries.GetScreen;

public class GetScreenQueryHandler : IRequestHandler<GetScreenQuery, ScreenDto>
{
    private readonly ScreenRepository _repository;
    
    public GetScreenQueryHandler(ScreenRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ScreenDto> Handle(GetScreenQuery request, CancellationToken cancellationToken)
    {
        var screen = await _repository.GetByIdAsync(request.Id);
        return screen.ToScreenDto();
    }
}