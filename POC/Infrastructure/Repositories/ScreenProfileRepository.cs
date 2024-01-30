namespace POC.Infrastructure.Repositories;

using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ScreenProfileRepository
{
    private readonly List<ScreenProfile> _screenProfiles = new();

    public Task<List<ScreenProfile>> GetAllAsync()
    {
        return Task.FromResult(_screenProfiles.ToList());
    }

    public Task<ScreenProfile> GetByIdAsync(int id)
    {
        var profile = _screenProfiles.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(profile);
    }

    public Task AddAsync(ScreenProfile profile)
    {
        _screenProfiles.Add(profile);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(ScreenProfile profile)
    {
        var existingProfile = _screenProfiles.FirstOrDefault(p => p.Id == profile.Id);
        if (existingProfile != null)
        {
            _screenProfiles.Remove(existingProfile);
            _screenProfiles.Add(profile);
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var profile = _screenProfiles.FirstOrDefault(p => p.Id == id);
        if (profile != null)
        {
            _screenProfiles.Remove(profile);
        }
        return Task.CompletedTask;
    }

    // Additional methods as needed, such as for managing relationships with Screens
}
