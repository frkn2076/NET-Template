using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Register.Business.Hub;
using Register.Business.Hub.Implementation;
using Register.DataAccess;
using Register.Repository;
using Register.Repository.Implementation;
using System;
using System.Text;

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

            var host = Environment.GetEnvironmentVariable("PostgreRegisterHost");
            var port = Environment.GetEnvironmentVariable("PostgreRegisterPort");
            var database = Environment.GetEnvironmentVariable("PostgreRegisterDB");
            var user = Environment.GetEnvironmentVariable("PostgreRegisterUser");
            var password = Environment.GetEnvironmentVariable("PostgreRegisterPassword");
            var timeout = Environment.GetEnvironmentVariable("PostgreRegisterTimeout");
            var pgsqlConnecton = $"Server={host};Port={port};Userid={user};Password={password};Timeout={timeout};Database={database}";

            services.AddDbContextPool<AppDBContext>(options => options.UseNpgsql(pgsqlConnecton));

            services.AddScoped<IRegisterRepo, RegisterRepo>();
            services.AddScoped<IBusinessManager, BusinessManager>();

            var jwtSecretKey = Environment.GetEnvironmentVariable("JwtSecretKey");
            var key = Encoding.ASCII.GetBytes(jwtSecretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
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

            Mapper.MapsterInit();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
