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
                spf.OwnsOne(s => s.OrderFiltering, of =>
                {
                    of.OwnsOne(o => o.From);
                    of.OwnsOne(o => o.To);
                    of.Property(s => s.IsSale)
                        .HasColumnType("boolean")
                        .IsRequired(false);
                        
                    of.Property(s => s.IsPickup)
                        .HasColumnType("boolean")
                        .IsRequired(false);
                    of.Property(s => s.OrderStatuses)
                        .IsRequired(false);
                    of.Property(s => s.EntityIds)
                        .IsRequired(false);
                }); 
                spf.OwnsOne(s => s.InventoryFiltering);
                spf.OwnsOne(s => s.DisplayConfig);
                
            });
            entity.HasMany(e => e.Screens);
        });
        //non nullable foreign keys lead to automatic cascade delete
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(a => a.Username);
        });
        base.OnModelCreating(modelBuilder);
    }
}