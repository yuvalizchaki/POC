using MediatR;
using POC.Contracts.ScreenProfile;

namespace POC.App.Queries;

public class GetAllScreenProfilesQuery : IRequest<List<ScreenProfileDto>>
{
}