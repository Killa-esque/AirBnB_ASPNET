using Microsoft.EntityFrameworkCore;
using AirBnBWebApi.Core.Entities;
using System;

namespace AirBnBWebApi.Infrastructure.Data
{
    public class AirBnBDbContext : DbContext
    {
        public AirBnBDbContext(DbContextOptions<AirBnBDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<KeyToken> KeyTokens { get; set; }
        public DbSet<RefreshTokenUsed> RefreshTokenUseds { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Location> Locations { get; set; }
        // public DbSet<Reservation> Reservations { get; set; }
        // public DbSet<Transaction> Transactions { get; set; }
        // public DbSet<TransactionMethod> TransactionMethods { get; set; }
        // public DbSet<TransactionLog> TransactionLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Transaction
            // modelBuilder.Entity<Transaction>(entity =>
            // {
            //     entity.HasKey(e => e.Id);
            //     entity.HasIndex(e => new { e.UserId, e.MerchantId });
            //     entity.Property(e => e.Amount)
            //         .HasPrecision(18, 2);

            //     // (1-n)
            //     entity.HasOne<TransactionMethod>()
            //         .WithMany()
            //         .HasForeignKey(e => e.TransactionMethodId)
            //         .OnDelete(DeleteBehavior.Restrict);

            //     // (1-1)
            //     entity.HasOne<Reservation>()
            //         .WithOne()
            //         .HasForeignKey<Transaction>(e => e.ReservationId)
            //         .OnDelete(DeleteBehavior.Cascade);
            // });

            // // TransactionLog
            // modelBuilder.Entity<TransactionLog>(entity =>
            // {
            //     entity.HasKey(e => e.Id); // Primary Key

            //     // (1-n)
            //     entity.HasOne<Transaction>()
            //         .WithMany()
            //         .HasForeignKey(e => e.TransactionId)
            //         .OnDelete(DeleteBehavior.Cascade);
            // });

            // // TransactionMethod
            // modelBuilder.Entity<TransactionMethod>(entity =>
            // {
            //     entity.HasKey(e => e.Id);
            //     entity.HasIndex(e => e.UserId);

            //     entity.Property(e => e.CardNumber)
            //         .HasMaxLength(16);
            //     entity.Property(e => e.Cvv)
            //         .HasMaxLength(4);
            // });

            // Review
            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.Id);

                // (1-n)
                entity.HasOne<Property>()
                    .WithMany()
                    .HasForeignKey(e => e.PropertyId)
                    .OnDelete(DeleteBehavior.Restrict);

                // (1-n)
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => new { e.PropertyId, e.UserId });
            });

            // Reservation
            // modelBuilder.Entity<Reservation>(entity =>
            // {
            //     entity.HasKey(e => e.Id);

            //     // Change this line to use NO ACTION instead of CASCADE
            //     // entity.HasOne<Transaction>()
            //     //     .WithOne()
            //     //     .HasForeignKey<Reservation>(e => e.TransactionId)
            //     //     .OnDelete(DeleteBehavior.NoAction);  // Change from DeleteBehavior.Cascade to DeleteBehavior.NoAction

            //     entity.HasOne<Property>()
            //         .WithMany()
            //         .HasForeignKey(e => e.PropertyId)
            //         .OnDelete(DeleteBehavior.Restrict);

            //     entity.HasOne<User>()
            //         .WithMany()
            //         .HasForeignKey(e => e.UserId)
            //         .OnDelete(DeleteBehavior.Restrict);

            //     entity.HasIndex(e => new { e.UserId, e.PropertyId });
            // });

            // Property
            modelBuilder.Entity<Property>(entity =>
            {
                entity.HasKey(e => e.Id);

                // (1-n)
                entity.HasOne<Location>()
                    .WithMany()
                    .HasForeignKey(e => e.LocationId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.PropertyHostId);

                entity.Property(e => e.PropertyName)
                    .HasMaxLength(200);
                entity.Property(e => e.PropertyThumbnailUrl)
                    .HasMaxLength(500);
            });

            // Location
            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Composite Index
                entity.HasIndex(e => new { e.City, e.Country });

                entity.Property(e => e.Address)
                    .HasMaxLength(300);
                entity.Property(e => e.PostalCode)
                    .HasMaxLength(20);
            });

            // Cấu hình bảng User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);  // Đặt Id làm khóa chính

                // Cấu hình quan hệ 1-1 giữa User và KeyToken
                entity.HasOne(u => u.KeyToken)
                      .WithOne(kt => kt.User)
                      .HasForeignKey<KeyToken>(kt => kt.UserId)  // UserId là khóa chính và khóa ngoại
                      .OnDelete(DeleteBehavior.Cascade);  // Xóa User sẽ xóa luôn KeyToken của User đó
            });

            // Cấu hình bảng KeyToken
            modelBuilder.Entity<KeyToken>(entity =>
            {
                entity.HasKey(kt => kt.UserId);  // UserId là khóa chính và khóa ngoại

                // Cấu hình quan hệ 1-n giữa KeyToken và RefreshTokenUsed
                entity.HasMany(kt => kt.RefreshTokensUsed)
                      .WithOne(rt => rt.KeyToken)
                      .HasForeignKey(rt => rt.UserId)  // RefreshTokenUsed sử dụng UserId làm khóa ngoại
                      .OnDelete(DeleteBehavior.Cascade);  // Xóa KeyToken sẽ xóa luôn các RefreshTokenUsed
            });

            // Cấu hình bảng RefreshTokenUsed
            modelBuilder.Entity<RefreshTokenUsed>(entity =>
            {
                entity.HasKey(rt => new { rt.UserId, rt.RefreshToken });  // Composite key giữa UserId và RefreshToken

                // Cấu hình quan hệ n-1 giữa RefreshTokenUsed và KeyToken
                entity.HasOne(rt => rt.KeyToken)
                      .WithMany(kt => kt.RefreshTokensUsed)
                      .HasForeignKey(rt => rt.UserId)  // Sử dụng UserId làm khóa ngoại
                      .OnDelete(DeleteBehavior.Cascade);  // Xóa KeyToken sẽ xóa các RefreshTokenUsed liên quan
            });

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
