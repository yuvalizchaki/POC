using Microsoft.EntityFrameworkCore;
using POC.Infrastructure.Models;

namespace POC.Infrastructure;

public class OurDbContext(DbContextOptions<OurDbContext> options) : DbContext(options)
{
    public DbSet<ScreenProfile> ScreenProfiles { get; set; }
    public DbSet<Screen> Screens { get; set; }
    
    public DbSet<Admin> Admins { get; set; }

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
        
        modelBuilder.Entity<ScreenProfile>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.OwnsOne(e => e.ScreenProfileFiltering, spf =>
            {
                spf.OwnsOne(s => s.OrderTimeRange);
                spf.Property(s => s.IsPickup);
                spf.Property(s => s.IsSale);
                spf.Property(s => s.OrderStatusses);
                spf.Property(s => s.EntityIds);
            });
        });
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(a => a.Username);
        });
        //non nullable foreign keys lead to automatic cascade delete
        
        base.OnModelCreating(modelBuilder);
    }
}