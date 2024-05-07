using MediatR;
using POC.Contracts.ScreenProfile;
using POC.Infrastructure.Common.Exceptions;
using POC.Infrastructure.Repositories;

namespace POC.App.Commands.DeleteScreenProfile;

public class DeleteScreenProfileCommandHandler(ScreenProfileRepository repository,
    ScreenRepository screenRepository //TODO DELETE THIS WHEN WE HAVE A PROPER DB THAT DOES THIS FOR US
) : IRequestHandler<DeleteScreenProfileCommand>
{
    public async Task Handle(DeleteScreenProfileCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.DeleteAsync(request.Id);
        if (!result) throw new ScreenProfileNotFoundException();
        
        //TODO DELETE THIS WHEN WE HAVE A PROPER DB THAT DOES THIS FOR US
        //await screenRepository.updateScreenProfileDeleteAsync(request.Id);
        //
    }
}