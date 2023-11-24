using MedicalOffice.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Server.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {

        modelBuilder.Entity<User>()
            .HasOne(p => p.Role)
            .WithMany(x=>x.Users)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
        .HasOne(p => p.Status);


        modelBuilder.Entity<Role>()
          .HasMany(r => r.Users)
          .WithOne(u => u.Role)
          .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DaysReserve>()
          .HasMany(r => r.TimesReserves) 
          .WithOne(x=>x.DaysReserve)
          .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<TimesReserve>()
        .HasOne(r => r.DaysReserve)
        .WithMany(x => x.TimesReserves)
        .OnDelete(DeleteBehavior.Restrict);
    }
 
    public DbSet<Role> Roles { get; set; } 
    public DbSet<Status> Status { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<AboutUs> AboutUs { get; set; }
    public DbSet<ContactUs> ContactUs { get; set; }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<ProvidingService> ProvidingServices { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Settings> Settings { get; set; }
    public DbSet<TimesReserve> TimesReserves { get; set; }
    public DbSet<DaysReserve> DaysReserves { get; set; }

}
