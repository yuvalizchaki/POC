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
        profile.Id = GetNextId();
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
    
    //TODO this is 100% not best practice, and not something we should use, but for the POC it's fine, lemme know if u think otherwise
    private int GetNextId()
    {
        return _screenProfiles.Any() ? _screenProfiles.Max(p => p.Id) + 1 : 1;
    }

    // Additional methods as needed, such as for managing relationships with Screens
}
