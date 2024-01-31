using MediatR;
using POC.Contracts.ScreenProfile;

namespace POC.App.Queries.GetAllScreenProfiles;

public class GetAllScreenProfilesQuery : IRequest<List<ScreenProfileDto>>
{
}