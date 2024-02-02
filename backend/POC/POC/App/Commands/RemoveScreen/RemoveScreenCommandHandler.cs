using MediatR;
using POC.Infrastructure.Common.Exceptions;
using POC.Infrastructure.Repositories;

namespace POC.App.Commands.RemoveScreen;

public class RemoveScreenCommandHandler(ScreenRepository repository,
    ScreenProfileRepository screenProfileRepository //TODO DELETE THIS WHEN WE HAVE A PROPER DB THAT DOES THIS FOR US
    ) : IRequestHandler<RemoveScreenCommand>
{
    public async Task Handle(RemoveScreenCommand request, CancellationToken cancellationToken)
    {
        //TODO DELETE THIS WHEN WE HAVE A PROPER DB THAT DOES THIS FOR US
        var screen = await repository.GetByIdAsync(request.Id);
        if (screen == null) throw new ScreenNotFoundException();
        await screenProfileRepository.updateScreenDeleteAsync(screen.Id, screen.ScreenProfileId);
        //
        
        var result = await repository.DeleteAsync(request.Id);
        
        if (!result) throw new ScreenNotFoundException();
    }
}
