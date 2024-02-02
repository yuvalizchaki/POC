using MediatR;
using POC.Contracts.ScreenProfile;

namespace POC.App.Queries.GetScreenProfile;

public class GetScreenProfileQuery(int id) : IRequest<ScreenProfileDto>
{
    public int Id { get; set; } = id;
}