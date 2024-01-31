using MediatR;
using POC.Contracts.Screen;

namespace POC.App.Commands.PairScreen;

public class PairScreenCommand(PairScreenDto pairScreenDto) : IRequest<ScreenDto>
{
    public PairScreenDto PairScreenDto { get; set; } = pairScreenDto;
}