using POC.Contracts.ScreenProfile;
using POC.Infrastructure.Models;

namespace POC.Infrastructure.Extensions;

public static class ScreenProfileExtensions
{
    //to dto
    public static ScreenProfileDto? ToScreenProfileDto(this ScreenProfile screenProfile)
    {
        return screenProfile == null? null : new ScreenProfileDto
        {
            Id = screenProfile.Id,
            Name = screenProfile.Name,
            Screens = screenProfile.Screens.Select(s => s.ToScreenDto()).ToList()
        };
    }
}