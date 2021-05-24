using Infra.Constants;
using Infra.Extensions;
using Infra.Helper;
using Infra.Localizer;
using Infra.Resource.DataAccess;
using Infra.Resource.Implementation;
using Infra.Resource.Repository;
using Infra.Resource.Repository.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Register.Business.Hub;
using Register.Business.Hub.Implementation;
using Register.DataAccess;
using Register.Repository;
using Register.Repository.Implementation;
using System;

namespace Register.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "RegisterAPI", Version = "v1" }));

            var registerDBConnecton = Helper.GetPostgreDatabaseConnection(PrebuiltVariables.PostgreRegisterDB);
            services.AddDbContextPool<RegisterDBContext>(options => options.UseNpgsql(registerDBConnecton).EnableSensitiveDataLogging());

            var localizerDBConnecton = Helper.GetPostgreDatabaseConnection(PrebuiltVariables.PostgreLocalizerDB);
            services.AddDbContextPool<LocalizerDBContext>(options => options.UseNpgsql(localizerDBConnecton).EnableSensitiveDataLogging());

            services.AddScoped<IRegisterRepo, RegisterRepo>();
            services.AddScoped<IAuthenticationRepo, AuthenticationRepo>();
            services.AddScoped<IBusinessManager, BusinessManager>();
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            services.AddScoped<ILocalizationRepo, LocalizationRepo>();
            services.AddScoped<ILocalizer, Localizer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RegisterAPI v1"));
            }

            app.MigrateDatabaseAndTables<RegisterDBContext>();
            app.MigrateDatabaseAndTables<LocalizerDBContext>();

            Mapper.MapsterInit();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
