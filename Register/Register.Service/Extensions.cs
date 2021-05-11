﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
                context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                //log
            }
        }
    }
}
