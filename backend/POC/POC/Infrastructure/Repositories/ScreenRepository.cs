using Microsoft.EntityFrameworkCore;
using POC.Contracts.Screen;

namespace POC.Infrastructure.Repositories;

using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ScreenRepository
{
    private readonly OurDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ScreenRepository(OurDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }
    private string GetCompanyId()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst("CompanyId")?.Value;
    }
    public Task<List<Screen>> GetAllAsync()
    {
        var companyId = int.Parse(GetCompanyId());
        return _dbContext.Screens.Where(s => s.ScreenProfile.CompanyId == companyId).ToListAsync();
        //return Task.FromResult(_screens.ToList());
    }

    public Task<Screen?> GetByIdAsync(int id)
    {
        var companyId = int.Parse(GetCompanyId());
        var screen = _dbContext.Screens.FirstOrDefault(s => s.Id == id && s.ScreenProfile.CompanyId == companyId);
        return Task.FromResult(screen);
    }
    
    public Task AddAsync(Screen screen)
    {
        var companyId = int.Parse(GetCompanyId());
        if (companyId != screen.ScreenProfile.CompanyId)
            throw new UnauthorizedAccessException("Unauthorized to add screen");
        //todo see how the db creates the id
        _dbContext.Screens.Add(screen);
        _dbContext.SaveChanges();
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Screen screen)
    {
        var companyId = int.Parse(GetCompanyId());
        var existingScreen = _dbContext.Screens.FirstOrDefault(s => s.Id == screen.Id && s.ScreenProfile.CompanyId == companyId);
        if (existingScreen == null) return Task.CompletedTask;
        _dbContext.Screens.Update(screen);
        _dbContext.SaveChanges();
        return Task.CompletedTask;
    }

    public Task<bool> DeleteAsync(int id)
    {
        var companyId = int.Parse(GetCompanyId());
        var screen = _dbContext.Screens.FirstOrDefault(s => s.Id == id && s.ScreenProfile.CompanyId == companyId);
        if (screen == null) return Task.FromResult(false);
        _dbContext.Screens.Remove(screen);
        _dbContext.SaveChanges();
        return Task.FromResult(true);
    }
    
    
    //TODO DELETE THIS WHEN WE HAVE A PROPER DB THAT DOES THIS FOR US
    // public Task updateScreenProfileDeleteAsync(int screenProfileId)
    // {
    //     //delete all screens with screenProfileId
    //     var screens = _screens.Where(s => s.ScreenProfileId == screenProfileId).ToList();
    //     foreach (var screen in screens)
    //     {
    //         _screens.Remove(screen);
    //     }
    //     return Task.CompletedTask;
    // }
    //
    // //TODO DELETE THIS WHEN WE HAVE A PROPER DB THAT DOES THIS FOR US
    // private int GetNextId()
    // {
    //     return _screens.Any() ? _screens.Max(s => s.Id) + 1 : 1;
    // }

    // public Task<Screen?> GetScreenByIp(string ipAddress)
    // {
    //     var screen = _screens.FirstOrDefault(s => s.IpAddress == ipAddress);
    //     return Task.FromResult(screen);
    // }
    
}