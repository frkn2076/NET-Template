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
using System.Reflection;

namespace Register.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "RegisterService", Version = "v1" }));

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            var pgsqlConnecton = Helper.GetPGSQLConnectionString();
            services.AddDbContextPool<AppDBContext>(options => options.UseNpgsql(pgsqlConnecton, sql => sql.MigrationsAssembly(migrationsAssembly)));

            services.AddScoped<IRegisterRepo, RegisterRepo>();
            services.AddScoped<IAuthenticationRepo, AuthenticationRepo>();
            services.AddScoped<IBusinessManager, BusinessManager>();
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RegisterService v1"));
            }

            app.MigrateDatabaseAndTables();

            Mapper.MapsterInit();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
