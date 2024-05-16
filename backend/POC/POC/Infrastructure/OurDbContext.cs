using Microsoft.EntityFrameworkCore;
using POC.Infrastructure.Models;

namespace POC.Infrastructure;

public class OurDbContext(DbContextOptions<OurDbContext> options) : DbContext(options)
{
    public DbSet<ScreenProfile> ScreenProfiles { get; set; }
    public DbSet<Screen> Screens { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OurDbContext).Assembly);
        // Configure relationships between entities
        
        modelBuilder.Entity<Screen>()
            .HasOne(s => s.ScreenProfile)             // Each Screen has one ScreenProfile
            .WithMany(sp => sp.Screens)                // Each ScreenProfile has many Screens
            .HasForeignKey(s => s.ScreenProfileId);   // Foreign key property in Screen
        
        modelBuilder.Entity<ScreenProfile>()
            .HasMany(sp => sp.Screens)                 // Each ScreenProfile has many Screens
            .WithOne(s => s.ScreenProfile)             // Each Screen has one ScreenProfile
            .HasForeignKey(s => s.ScreenProfileId);   // Foreign key property in Screen
        
        //non nullable foreign keys lead to automatic cascade delete
        
        base.OnModelCreating(modelBuilder);
    }
}