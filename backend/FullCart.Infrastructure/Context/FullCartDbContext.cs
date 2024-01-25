using FullCart.Domain.Common;
using FullCart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Infrastructure.Context;

public class FullCartDbContext : DbContext
{

    public FullCartDbContext(DbContextOptions<FullCartDbContext> options):base(options)
    {
            
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }
    public virtual DbSet<Inventory> Inventory { get; set; }
    public virtual DbSet<InventoryStatus> InventoryStatus { get; set; }
    public virtual DbSet<Cart> Cart {  get; set; }
    public virtual DbSet<Order> Order { get; set; } 



    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entityEntry = ChangeTracker.Entries<BaseEntity>().Where(e =>
        e.State == EntityState.Added ||
        e.State == EntityState.Modified ||
        e.State == EntityState.Deleted)
        .Select(entityEntry =>
        {
            entityEntry.Entity.DateCreated = DateTimeOffset.UtcNow;
            entityEntry.Entity.DateUpdated = DateTimeOffset.UtcNow;
            entityEntry.Entity.DateDeleted = DateTimeOffset.UtcNow;
            return entityEntry;
        });

        return base.SaveChangesAsync(cancellationToken);
    }

}
