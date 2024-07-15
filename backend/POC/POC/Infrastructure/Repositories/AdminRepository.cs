using Microsoft.EntityFrameworkCore;
using POC.Infrastructure.Models;

namespace POC.Infrastructure.Repositories;

public class AdminRepository(OurDbContext dbContext)
{
    public Task<Admin?> GetByUserNameAndPassAsync(String username, String hashedPassword)
    {
        var admin = dbContext.Admins.FirstOrDefault(s => s.Username == username && s.HashedPassword == hashedPassword);
        return Task.FromResult(admin);
    }

    public IEnumerable<int> GetAdminIds()
    {
        return dbContext.Admins.Select(a =>int.Parse(a.CompanyId)).ToList();
    }
}