using Infra.Constants;
using Infra.Core.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Register.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Register.DataAccess
{
    public class RegisterDBContext : DbContext
    {
        
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Authentication> Authentications { get; set; }

        public RegisterDBContext(DbContextOptions<RegisterDBContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => base.OnConfiguring(optionsBuilder);

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is Audit && (e.State == EntityState.Added || e.State == EntityState.Modified));

            ModifyAudit(entries);

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is Audit && (e.State == EntityState.Added || e.State == EntityState.Modified));

            ModifyAudit(entries);

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ModifyAudit(IEnumerable<EntityEntry> entries)
        {
            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((Audit)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                    ((Audit)entityEntry.Entity).CreatedBy = PrebuiltVariables.AppName;
                }
                else
                {
                    Entry((Audit)entityEntry.Entity).Property(p => p.CreatedAt).IsModified = false;
                    Entry((Audit)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
                }

                ((Audit)entityEntry.Entity).ModifiedAt = DateTime.UtcNow;
                ((Audit)entityEntry.Entity).ModifiedBy = PrebuiltVariables.AppName;
            }
        }
    }
}
