using FundManagement.Authentication;
using FundManagement.Common.Utils;
using FundManagement.DataAccess;
using FundManagement.DataAccess.Infrastructure;
using FundManagement.DataAccess.Repository;
using FundManagement.Service;
using FundManagement.Service.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FundManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register Service
            //services.AddSingleton<AppDbContext>(x => new AppDbContext(Configuration.GetConnectionString(AppConstants.CONNECTION_STRING)));

            services.AddSingleton<IDbFactory, DbFactory>(x => new DbFactory(Configuration.GetConnectionString(AppConstants.CONNECTION_STRING)));
            services.AddSingleton<IUnitOfWork, UnitOfWork>();

            // Repositories
            services.AddScoped<IConsumeRepository, ConsumeRepository>();
            services.AddScoped<IDonationRepository, DonationRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ITeamRepository , TeamRepository>();

            // Services
            services.AddScoped<IConsumeService, ConsumeService>();
            services.AddScoped<IDonationService, DonationService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITeamService, TeamService>();  

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(Configuration[AppConstants.JWT.KEY]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration[AppConstants.JWT.ISSUER],
                    ValidAudience = Configuration[AppConstants.JWT.AUDIENCE],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IJWTManagerService, JWTManagerService>(); 

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FundManagement", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FundManagement v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
             
            app.UseAuthentication();  
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
