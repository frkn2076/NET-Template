using Microsoft.EntityFrameworkCore;
using RegisterDataAccess.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RegisterDataAccess
{
    public class AppDBContext : DbContext
    {
        private readonly string AppName = Environment.GetEnvironmentVariable("AppName");

        public DbSet<Register> Register { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => base.OnConfiguring(optionsBuilder);

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is Audit && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((Audit)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                    ((Audit)entityEntry.Entity).CreatedBy = AppName;
                }
                else
                {
                    Entry((Audit)entityEntry.Entity).Property(p => p.CreatedAt).IsModified = false;
                    Entry((Audit)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
                }

                ((Audit)entityEntry.Entity).ModifiedAt = DateTime.UtcNow;
                ((Audit)entityEntry.Entity).ModifiedBy = AppName;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
