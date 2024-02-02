using MediatR;
using POC.Infrastructure.Repositories;

namespace POC.App.Commands.RemoveScreen;

public class RemoveScreenCommandHandler : IRequestHandler<RemoveScreenCommand>
{
    
    private readonly ScreenRepository _repository;
    
    public RemoveScreenCommandHandler(ScreenRepository repository)
    {
        _repository = repository;
    }
    
    public async Task Handle(RemoveScreenCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
        
    }
}
