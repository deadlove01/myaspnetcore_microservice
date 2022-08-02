﻿using Microsoft.EntityFrameworkCore;
using Order.Domain.Common;

namespace Order.Infrastructure.Persistence;

public class OrderContext : DbContext
{
    public OrderContext(DbContextOptions<OrderContext> options) : base(options)
    {
    }

    public DbSet<Domain.Entities.Order> Orders { get; set; }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = "ravi";
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = "ravi";
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}