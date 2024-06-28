using MediatR;
using POC.Api.Hubs;
using POC.Infrastructure.Common.Exceptions;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.Repositories;

namespace POC.App.Commands.RemoveScreen;

public class RemoveScreenCommandHandler(ScreenRepository repository,
    ILogger<RemoveScreenCommandHandler> logger,
    ScreenHub screenHub,
    ScreenProfileRepository screenProfileRepository //TODO DELETE THIS WHEN WE HAVE A PROPER DB THAT DOES THIS FOR US
    ) : IRequestHandler<RemoveScreenCommand>
{
    public async Task Handle(RemoveScreenCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var screen = await repository.GetByIdAsync(request.Id);
            if (screen == null)
            {
                throw new ScreenNotFoundException($"Screen with ID {request.Id} not found.");
            }

            //await screenProfileRepository.updateScreenDeleteAsync(screen.Id, screen.ScreenProfileId);

            var result = await repository.DeleteAsync(request.Id);
            if (!result)
            {
                throw new Exception("Failed to delete screen.");
            }

            var screenDto = screen.ToScreenDto();

            await screenHub.RemoveScreen(screenDto); //<-- Re-add when we implement the tokens
        }
        catch (Exception ex)
        {
            // Log the exception
            logger.LogError(ex, "Error occurred while handling RemoveScreenCommand for Screen ID {ScreenId}", request.Id);

            // Rethrow or handle the exception as appropriate
            throw;
        }
    }
}
