using Infra.Resource.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Infra.Extensions
{
    public static class Extension
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

        public static void JWTRegistration(this IServiceCollection service, string key, string scheme)
        {
            var signingKey = Encoding.ASCII.GetBytes(key);

            service.AddAuthentication(o => o.DefaultAuthenticateScheme = scheme);

            service.AddAuthentication()
                .AddJwtBearer(scheme, x =>
                {
                    x.SaveToken = true;
                    x.RequireHttpsMetadata = false;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(signingKey),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        public static string RemoveText(this string text, string key) => text.Replace(key, string.Empty);

    }
}
