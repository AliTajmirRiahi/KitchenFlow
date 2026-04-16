using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.Persistence
{
    public class RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : DbContext(options)
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration for Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.CustomerId).IsRequired();
                entity.Property(o => o.TableId).IsRequired();
                entity.Property(o => o.Status).IsRequired();

                entity.HasMany(o => o.Items)
                      .WithOne()
                      .HasForeignKey("OrderId")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuration for OrderItem
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(oi => oi.Id);
                entity.Property(oi => oi.Quantity).IsRequired();
                entity.Property(oi => oi.UnitPrice).HasPrecision(38,0).IsRequired();
                entity.Property(oi => oi.Total).HasPrecision(38,0).IsRequired();
            });
        }

        public override int SaveChanges()
        {
            //beforeSave
            var result = base.SaveChanges();
            //afterSave
            return result;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //beforeSave
            var result = base.SaveChangesAsync(cancellationToken);
            //afterSave
            return result;
        }
    }
}

