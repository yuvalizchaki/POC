using MediatR;
using POC.Contracts.ScreenProfile;
using POC.Infrastructure.Repositories;

namespace POC.App.Commands.DeleteScreenProfile;

public class DeleteScreenProfileCommandHandler : IRequestHandler<DeleteScreenProfileCommand, DeletedScreenProfileResponse>
{
    
    private readonly ScreenProfileRepository _repository;

    public DeleteScreenProfileCommandHandler(ScreenProfileRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<DeletedScreenProfileResponse> Handle(DeleteScreenProfileCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
        var response = new DeletedScreenProfileResponse
        {
            Id = request.Id,
            message = "Screen Profile Deleted Successfully"
        };

        return response;
    }
}