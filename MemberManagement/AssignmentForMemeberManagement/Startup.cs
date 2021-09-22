using AuthenticateService.Services;
using Data.DBContext;
using FluentValidation;
using FluentValidation.AspNetCore;
using MemberRepository.Repository;
using MemberService.Dtos;
using MemberService.Services;
using MemberService.Validator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenRepository.Repository;

namespace AssignmentForMemeberManagement
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
            AddDependencyInjection(services);
            services.AddDbContext<DataContext>(option => option.UseNpgsql(Configuration.GetConnectionString("DatabaseConnection")));

            services.AddControllers().AddFluentValidation();
            AddAuthentication(services);

            AddValidation(services);
            services.AddControllers();

            services.AddTransient<DataContext>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AssignmentForMemeberManagement", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AssignmentForMemeberManagement v1"));
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

        #region Private function
        private void AddDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IAuthenticateService, AuthenticateServiceImp>();
            services.AddScoped<IMemberService, MemberServiceImp>();
            services.AddScoped<IMemberRepository, MemberRepositoryImp>();
            services.AddScoped<ITokenRepository, TokenRepositoryImp>();
        }
        private void AddAuthentication(IServiceCollection services)
        {
            var tokenValidationParameter = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = Configuration["Jwt:Audience"],
                ValidIssuer = Configuration["Jwt:Issuer"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            };
            services.AddSingleton(tokenValidationParameter);
            services.AddSignalR();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = tokenValidationParameter;
            });
        }
        private void AddValidation(IServiceCollection services)
        {
            services.AddTransient<IValidator<MemberCreateVM>, CreateMemberValidator>();
        }
        #endregion

    }
}
