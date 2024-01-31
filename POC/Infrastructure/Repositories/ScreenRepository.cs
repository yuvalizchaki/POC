namespace POC.Infrastructure.Repositories;

using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ScreenRepository
{
    private readonly List<Screen> _screens = new();

    public Task<List<Screen>> GetAllAsync()
    {
        return Task.FromResult(_screens.ToList());
    }

    public Task<Screen> GetByIdAsync(int id)
    {
        var screen = _screens.FirstOrDefault(s => s.Id == id);
        return Task.FromResult(screen);
    }
    
    public Task AddAsync(Screen screen)
    {
        screen.Id = GetNextId();
        _screens.Add(screen);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Screen screen)
    {
        var existingScreen = _screens.FirstOrDefault(s => s.Id == screen.Id);
        if (existingScreen != null)
        {
            _screens.Remove(existingScreen);
            _screens.Add(screen);
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var screen = _screens.FirstOrDefault(s => s.Id == id);
        if (screen != null)
        {
            _screens.Remove(screen);
        }
        return Task.CompletedTask;
    }
    
    //TODO DELETE THIS WHEN WE HAVE A PROPER DB THAT DOES THIS FOR US
    private int GetNextId()
    {
        return _screens.Any() ? _screens.Max(s => s.Id) + 1 : 1;
    }
}
