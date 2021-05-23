using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infra.Extensions
{
    public static class Migration
    {
        public static void MigrateDatabaseAndTables<T>(this IApplicationBuilder app) where T : DbContext
        {
            try
            {
                using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
                var context = (DbContext)serviceScope.ServiceProvider.GetRequiredService<T>();
                context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                //log
            }
        }
    }
}
