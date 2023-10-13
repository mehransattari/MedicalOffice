using MedicalOffice.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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


    }
 
    public DbSet<Role> Roles { get; set; } 
    public DbSet<Status> Status { get; set; }
    public DbSet<User> Users { get; set; }



}
