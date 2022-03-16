﻿using Sword.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace Sword.EntityFrameworkCore
{
    public class SwordDbContext : DbContext, ITransaction
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SwordDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public new DbSet<TEntity> Set<TEntity>() where TEntity : class => base.Set<TEntity>();
        protected override void OnModelCreating(ModelBuilder modelBuilder) // Add-Migration InitialMigration -Verbose、Update-Database -Verbose
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            BeforeSaveChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }
        private void BeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            foreach (var entry in ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted))
            {
                var changeTime = DateTime.Now;
                var changor = Guid.Empty;
                if (_httpContextAccessor.HttpContext.User?.Identity != null)
                {
                    var subject = (_httpContextAccessor.HttpContext.User?.Identity as ClaimsIdentity).FindFirst(x => x.Type == JwtRegisteredClaimNames.Sub);
                    if (subject != null) _ = Guid.TryParse(subject.Value, out changor);
                }
                if (entry.Entity is BaseEntity extra)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            extra.CreateTime = changeTime;
                            extra.Creator = changor;
                            break;
                        case EntityState.Modified:
                            extra.UpdateTime = changeTime;
                            extra.Updator = changor;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            extra.DeleteTime = changeTime;
                            extra.Deletor = changor;
                            break;
                    }
                }
            }
        }
    }
}
