using POC.Contracts.Screen;
using POC.Infrastructure.Models;

namespace POC.Infrastructure.Extensions;

public static class ScreenExtensions
{
    //to dto
    public static ScreenDto ToScreenDto(this Screen screen)
    {
        return new ScreenDto()
        {
            Id = screen.Id,
            Ip = screen.IpAddress,
            ScreenProfileId = screen.ScreenProfileId
        };
    }
}