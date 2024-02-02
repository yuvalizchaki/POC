using MediatR;

namespace POC.App.Commands.RemoveScreen;

public class RemoveScreenCommand(int id) : IRequest
{
    //id
    public int Id { get; set; } = id;
}