using MediatR;

namespace POC.App.Commands.RemoveScreen;

public class RemoveScreenCommand : IRequest
{
    //id
    public int Id { get; set; }
    public RemoveScreenCommand(int id)
    {
        Id = id;
    }
}