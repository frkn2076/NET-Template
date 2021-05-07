using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Register.DataAccess;
using System;

namespace Register.Service
{
    public static class Extensions
    {
        public static void MigrateDatabaseAndTables(this IApplicationBuilder app)
        {
            try
            {
                using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
                var context = serviceScope?.ServiceProvider.GetRequiredService<AppDBContext>();
                context.Database.Migrate();
                RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)context.Database.GetService<IDatabaseCreator>();
                databaseCreator.CreateTables();
            }
            catch (Exception ex)
            {
                //log
            }
        }
    }
}
