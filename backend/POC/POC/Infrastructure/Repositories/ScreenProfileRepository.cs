namespace POC.Infrastructure.Repositories;

using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ScreenProfileRepository(
    ProjectDbContext dbContext //should be injected here
    )
{
    private readonly List<ScreenProfile> _screenProfiles = new();

    public Task<List<ScreenProfile>> GetAllAsync()
    {
        return Task.FromResult(_screenProfiles.ToList());
    }

    public Task<ScreenProfile?> GetByIdAsync(int id)
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

    public Task<bool> UpdateAsync(ScreenProfile profile)
    {
        var existingProfile = _screenProfiles.FirstOrDefault(p => p.Id == profile.Id);
        
        if (existingProfile == null) return Task.FromResult(false);
        
        _screenProfiles.Remove(existingProfile);
        _screenProfiles.Add(profile);
        
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var profile = _screenProfiles.FirstOrDefault(p => p.Id == id);
        
        if (profile == null) return Task.FromResult(false);
        
        _screenProfiles.Remove(profile);
        
        return Task.FromResult(true);
    }
    
    //TODO this is 100% not best practice, and not something we should use, but for the POC it's fine, lemme know if u think otherwise
    private int GetNextId()
    {
        return _screenProfiles.Count != 0 ? _screenProfiles.Max(p => p.Id) + 1 : 1;
    }
    
    //TODO DELETE THIS WHEN WE HAVE A PROPER DB THAT DOES THIS FOR US
    public Task updateScreenDeleteAsync(int screenId, int screenProfileId)
    {
        //delete the screen for the screenProfile
        var screenProfile = _screenProfiles.FirstOrDefault(p => p.Id == screenProfileId);

        var screen = screenProfile?.Screens.FirstOrDefault(s => s.Id == screenId);
        
        if (screen == null) return Task.CompletedTask;
        
        screenProfile?.Screens.Remove(screen);
        
        return Task.CompletedTask;
    }

}
