using MediatR;
using POC.Contracts.Screen;
using POC.Infrastructure.Common.Exceptions;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.Repositories;

namespace POC.App.Queries.GetScreen;

public class GetScreenQueryHandler(ScreenRepository repository) : IRequestHandler<GetScreenQuery, ScreenDto>
{
    public async Task<ScreenDto> Handle(GetScreenQuery request, CancellationToken cancellationToken)
    {
        var screen = await repository.GetByIdAsync(request.Id);

        if (screen == null) throw new ScreenNotFoundException();
        
        return screen.ToScreenDto();
    }
}