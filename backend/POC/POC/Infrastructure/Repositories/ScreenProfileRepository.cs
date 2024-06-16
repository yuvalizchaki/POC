using Microsoft.EntityFrameworkCore;

namespace POC.Infrastructure.Repositories;

using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ScreenProfileRepository(
    OurDbContext dbContext
    )
{
    // private readonly List<ScreenProfile> _screenProfiles = new();

    public Task<List<ScreenProfile>> GetAllAsync()
    {
        return dbContext.ScreenProfiles.Include(sp => sp.Screens).ToListAsync();
        // return Task.FromResult(_screenProfiles.ToList());
    }

    public Task<ScreenProfile?> GetByIdAsync(int id)
    {
        var screenProfile = dbContext.ScreenProfiles.Include(sp => sp.Screens).FirstOrDefault(sp => sp.Id == id);
        // var screenProfile = dbContext.ScreenProfiles.FirstOrDefault(sp => sp.Id == id);

        return Task.FromResult(screenProfile);
    }

    public Task AddAsync(ScreenProfile profile)
    {
        //todo see how the db creates the id
        dbContext.ScreenProfiles.Add(profile);
        dbContext.SaveChanges();
        
        return Task.CompletedTask;
    }

    public Task<bool> UpdateAsync(ScreenProfile profile)
    {
        var existingProfile = dbContext.ScreenProfiles.FirstOrDefault(sp => sp.Id == profile.Id);
        
        if (existingProfile == null) return Task.FromResult(false);
        
        dbContext.ScreenProfiles.Update(profile);
        dbContext.SaveChanges();
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var profile = dbContext.ScreenProfiles.FirstOrDefault(sp => sp.Id == id);
        
        if (profile == null) return Task.FromResult(false);
        
        dbContext.ScreenProfiles.Remove(profile);
        dbContext.SaveChanges();
        return Task.FromResult(true);
    }
    
    //TODO this is 100% not best practice, and not something we should use, but for the POC it's fine, lemme know if u think otherwise
    // private int GetNextId()
    // {
    //     return _screenProfiles.Count != 0 ? _screenProfiles.Max(p => p.Id) + 1 : 1;
    // }
    
    
    //TODO DELETE THIS WHEN WE HAVE A PROPER DB THAT DOES THIS FOR US
    // public Task updateScreenDeleteAsync(int screenId, int screenProfileId)
    // {
    //     //delete the screen for the screenProfile
    //     var screenProfile = _screenProfiles.FirstOrDefault(p => p.Id == screenProfileId);
    //
    //     var screen = screenProfile?.Screens.FirstOrDefault(s => s.Id == screenId);
    //     
    //     if (screen == null) return Task.CompletedTask;
    //     
    //     screenProfile?.Screens.Remove(screen);
    //     
    //     return Task.CompletedTask;
    // }

}
