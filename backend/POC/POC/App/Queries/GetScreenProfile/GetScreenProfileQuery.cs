using MediatR;
using POC.Contracts.ScreenProfile;

namespace POC.App.Queries.GetScreenProfile;

public class GetScreenProfileQuery : IRequest<ScreenProfileDto>
{
    public int Id { get; set; }
    
    public GetScreenProfileQuery(int id)
    {
        Id = id;
    }

}