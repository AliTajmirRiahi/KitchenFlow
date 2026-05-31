using Arta.Domain.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using KitchenFlow.Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KitchenFlow.Infrastructure.Persistence
{
    public class RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : DbContext(options)
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Aplly RowVersion/Timestamp in SQL Server
            // Get all entity types that are in domain
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Check if the entity implements IEntity<>
                // We look for any interface that is generic and matches IEntity<>
                var isEntity = entityType.ClrType.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntity<>));

                if (isEntity)
                {
                    // 1. Configure RowVersion dynamically
                    // Check if the property 'Version' exists on this entity type
                    var versionProperty = entityType.FindProperty("Version");
                    if (versionProperty != null)
                    {
                        modelBuilder.Entity(entityType.ClrType)
                            .Property("Version")
                            .IsRowVersion();

                        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory")
                        {
                            modelBuilder.Entity(entityType.ClrType)
                                        .Property("Version")
                                        .IsRequired(false);
                        }

                    }

                    // 2. Configure Global Query Filter for Soft Delete
                    // We need to define the filter: e => !e.IsDeleted
                    // This is slightly complex with reflection, so we use a helper method approach
                    ConfigureSoftDeleteFilter(modelBuilder, entityType);
                }
            }

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
                entity.Property(oi => oi.UnitPrice).HasPrecision(38, 0).IsRequired();
                entity.Property(oi => oi.Total).HasPrecision(38, 0).IsRequired();
            });
        }
        private void ConfigureSoftDeleteFilter(ModelBuilder modelBuilder, IMutableEntityType entityType)
        {
            // Define the lambda: entity => !entity.IsDeleted
            var parameter = Expression.Parameter(entityType.ClrType, "e");
            var property = Expression.Property(parameter, "IsDeleted");
            var filter = Expression.Lambda(Expression.Not(property), parameter);

            entityType.SetQueryFilter(filter);
        }
        public override int SaveChanges()
        {
            //beforeSave

            foreach (var entry in ChangeTracker.Entries<IEntity<Guid>>())
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        // Intercept Delete and change to Modified for Soft Delete
                        entry.State = EntityState.Modified;
                        entry.Entity.Delete();
                        break;
                }
            }

            var result = base.SaveChanges();
            //afterSave
            return result;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //beforeSave

            foreach (var entry in ChangeTracker.Entries<IEntity<Guid>>())
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        // Intercept Delete and change to Modified for Soft Delete
                        entry.State = EntityState.Modified;
                        entry.Entity.Delete();
                        break;
                }
            }

            var result = base.SaveChangesAsync(cancellationToken);
            //afterSave
            return result;
        }
    }
}

