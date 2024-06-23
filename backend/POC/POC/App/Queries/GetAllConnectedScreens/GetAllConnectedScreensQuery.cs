using MediatR;
using POC.Contracts.Screen;

namespace POC.App.Queries.GetAllConnectedScreens;

public class GetAllConnectedScreensQuery : IRequest<List<ScreenDto>>
{
    
}