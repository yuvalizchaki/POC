using Microsoft.EntityFrameworkCore;

namespace POC.Infrastructure;

public class OurDbContext(DbContextOptions<OurDbContext> options) : DbContext(options)
{
    // DbSet properties for your entities
    //public DbSet<YourEntity> YourEntities { get; set; }

    // Add DbSet properties for other entities...
}