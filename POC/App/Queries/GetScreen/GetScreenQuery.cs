using MediatR;
using POC.Contracts.Screen;

namespace POC.App.Queries.GetScreen;

public class GetScreenQuery : IRequest<ScreenDto>
{
    public int Id { get; set; }
    
    public GetScreenQuery(int id)
    {
        Id = id;
    }
    
}