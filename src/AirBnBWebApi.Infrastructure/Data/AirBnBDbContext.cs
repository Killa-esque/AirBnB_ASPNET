using Microsoft.EntityFrameworkCore;
using AirBnBWebApi.Core.Entities;
using System;

namespace AirBnBWebApi.Infrastructure.Data
{
  public class AirBnBDbContext : DbContext
  {
    public AirBnBDbContext(DbContextOptions<AirBnBDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Location> Locations { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // Property-Location relationship
      modelBuilder.Entity<Property>()
          .HasOne(p => p.Location)
          .WithMany(l => l.Properties)
          .HasForeignKey(p => p.LocationId);

      // Property-Host relationship
      modelBuilder.Entity<Property>()
          .HasOne(p => p.Host)
          .WithMany(u => u.Properties)
          .HasForeignKey(p => p.HostId);

      // Reservation-User relationship
      modelBuilder.Entity<Reservation>()
          .HasOne(r => r.User)
          .WithMany(u => u.Reservations)
          .HasForeignKey(r => r.UserId);

      // Reservation-Property relationship
      modelBuilder.Entity<Reservation>()
          .HasOne(r => r.Property)
          .WithMany(p => p.Reservations)
          .HasForeignKey(r => r.PropertyId);

      // Review-Property relationship
      modelBuilder.Entity<Review>()
          .HasOne(rv => rv.Property)
          .WithMany(p => p.Reviews)
          .HasForeignKey(rv => rv.PropertyId);

      // Review-User relationship
      modelBuilder.Entity<Review>()
          .HasOne(rv => rv.User)
          .WithMany(u => u.Reviews)
          .HasForeignKey(rv => rv.UserId);

      // Payment-Reservation relationship
      modelBuilder.Entity<Payment>()
          .HasOne(p => p.Reservation)
          .WithOne(r => r.Payment)
          .HasForeignKey<Payment>(p => p.ReservationId);

      modelBuilder.Entity<Property>()
        .Property(p => p.PricePerNight)
        .HasPrecision(18, 2);

      modelBuilder.Entity<Payment>()
        .Property(p => p.Amount)
        .HasPrecision(18, 2);

      base.OnModelCreating(modelBuilder);

    }

    public override int SaveChanges()
    {
      foreach (var entry in ChangeTracker.Entries())
      {
        if (entry.Entity is BaseEntity trackable)
        {
          if (entry.State == EntityState.Added)
          {
            trackable.CreatedAt = DateTime.Now;
          }

          if (entry.State == EntityState.Modified)
          {
            trackable.UpdatedAt = DateTime.Now;
          }
        }
      }
      return base.SaveChanges();
    }
  }
}
