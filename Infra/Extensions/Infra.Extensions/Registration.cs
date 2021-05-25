using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Infra.Extensions
{
    public static class Registration
    {
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

        public static void RedisRegistration(this IServiceCollection service, string host, string port, int expireDurationAsMinute)
        {
            service.AddDistributedRedisCache(options =>
            {
                options.InstanceName = "SessionAndCache";
                options.Configuration = $"{host}:{port}";
            });

            service.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(expireDurationAsMinute));
        }


        public static void LocalizerRegistration(this IServiceCollection service, string pgSqlConnection, bool sqlScriptLogging)
        {

        }
    }
}
