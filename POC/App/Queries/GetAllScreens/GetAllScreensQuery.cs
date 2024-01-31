using MediatR;
using POC.Contracts.Screen;

namespace POC.App.Queries.GetAllScreens;

public class GetAllScreensQuery : IRequest<List<ScreenDto>>
{
    
}