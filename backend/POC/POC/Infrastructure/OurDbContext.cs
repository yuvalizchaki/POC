using Microsoft.EntityFrameworkCore;
using POC.Infrastructure.Models;

namespace POC.Infrastructure;

public class OurDbContext(DbContextOptions<OurDbContext> options) : DbContext(options)
{
    public DbSet<ScreenProfile> ScreenProfiles { get; set; }
    public DbSet<Screen> Screens { get; set; }
    
    public DbSet<Order> Orders { get; set; }
    // public DbSet<InventoryItem> InventoryItems { get; set; }
    // public DbSet<ServiceItem> ServiceItems { get; set; }
    // public DbSet<PeopleOrderItem> PeopleOrderItems { get; set; }
    // public DbSet<OneTimeOrderItem> OneTimeOrderItems { get; set; }
    
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
        
        
        // modelBuilder.Entity<InventoryItem>()
        //     .HasOne(i => i.Order)
        //     .WithMany(o => o.InventoryItems)
        //     .HasForeignKey(i => i.OrderId);
        //
        // modelBuilder.Entity<ServiceItem>()
        //     .HasOne(s => s.Order)
        //     .WithMany(o => o.ServiceItems)
        //     .HasForeignKey(s => s.OrderId);
        //
        // modelBuilder.Entity<PeopleOrderItem>()
        //     .HasOne(p => p.Order)
        //     .WithMany(o => o.PeopleOrderItems)
        //     .HasForeignKey(p => p.OrderId);
        //
        // modelBuilder.Entity<OneTimeOrderItem>()
        //     .HasOne(o => o.Order)
        //     .WithMany(o => o.OneTimeOrderItems)
        //     .HasForeignKey(o => o.OrderId);
        
        base.OnModelCreating(modelBuilder);
    }
}