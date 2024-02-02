using MediatR;
using POC.Contracts.Screen;

namespace POC.App.Queries.GetScreen;

public class GetScreenQuery(int id) : IRequest<ScreenDto>
{
    public int Id { get; set; } = id;
}