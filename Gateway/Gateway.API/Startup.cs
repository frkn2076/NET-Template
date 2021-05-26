using Infra.LogPublisher;
using Infra.Extensions;
using Infra.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using Infra.Constants;

namespace Gateway.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "GatewayService", Version = "v1" }));

            services.JWTRegistration(PrebuiltVariables.JwtSecretKey, PrebuiltVariables.JwtScheme);

            services.RedisRegistration(PrebuiltVariables.RedisHost, PrebuiltVariables.RedisPort, Convert.ToInt32(PrebuiltVariables.RedisExpireDurationAsMinutes));

            services.AddOcelot();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GatewayService v1"));
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<RequestResponseLoggingMiddleware>((Action<string>)Publisher.Send);

            app.UseRouting();

            app.UseAuthentication();

            app.UseSession();

            app.UseOcelot();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
