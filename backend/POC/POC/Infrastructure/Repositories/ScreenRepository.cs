using Microsoft.EntityFrameworkCore;
using POC.Contracts.Screen;

namespace POC.Infrastructure.Repositories;

using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ScreenRepository(OurDbContext dbContext, IHttpContextAccessor httpContextAccessor)
{
    private string GetCompanyId()
    {
        return httpContextAccessor.HttpContext?.User?.FindFirst("CompanyId")?.Value;
    }
    public Task<List<Screen>> GetAllAsync()
    {
        var companyId = int.Parse(GetCompanyId());
        return dbContext.Screens.Where(s => s.ScreenProfile.CompanyId == companyId).ToListAsync();
        //return Task.FromResult(_screens.ToList());
    }

    public Task<Screen?> GetByIdAsync(int id)
    {
        var companyId = int.Parse(GetCompanyId());
        var screen = dbContext.Screens
            .Include(s => s.ScreenProfile)
            .FirstOrDefault(s => s.Id == id && s.ScreenProfile.CompanyId == companyId);
        return Task.FromResult(screen);
    }
    
    public Task<IEnumerable<Screen>> GetScreensByIdsAsync(IEnumerable<int> ids)
    {
        var companyId = int.Parse(GetCompanyId());
        var screens = dbContext.Screens
            .Where(s => ids.Contains(s.Id) && s.ScreenProfile.CompanyId == companyId);
        return Task.FromResult(screens as IEnumerable<Screen>);
    }
    
    public Task AddAsync(Screen screen)
    {
        var companyId = int.Parse(GetCompanyId());
        if (companyId != screen.ScreenProfile.CompanyId)
            throw new UnauthorizedAccessException("Unauthorized to add screen");
        //todo see how the db creates the id
        dbContext.Screens.Add(screen);
        dbContext.SaveChanges();
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Screen screen)
    {
        var companyId = int.Parse(GetCompanyId());
        var existingScreen = dbContext.Screens.FirstOrDefault(s => s.Id == screen.Id && s.ScreenProfile.CompanyId == companyId);
        if (existingScreen == null) return Task.CompletedTask;
        dbContext.Screens.Update(screen);
        dbContext.SaveChanges();
        return Task.CompletedTask;
    }

    public Task<bool> DeleteAsync(int id)
    {
        var companyId = int.Parse(GetCompanyId());
        var screen = dbContext.Screens.FirstOrDefault(s => s.Id == id && s.ScreenProfile.CompanyId == companyId);
        if (screen == null) return Task.FromResult(false);
        dbContext.Screens.Remove(screen);
        dbContext.SaveChanges();
        return Task.FromResult(true);
    }
    
}