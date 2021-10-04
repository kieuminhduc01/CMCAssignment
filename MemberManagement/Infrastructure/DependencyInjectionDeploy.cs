using Application;
using Application.Common.Interfaces.Repositories.MemberRepositories;
using Application.Common.Interfaces.Repositories.TokenRepositories;
using Application.Common.Interfaces.Services.MemberServices;
using Application.Common.Interfaces.Services.TokenServices;
using Infrastructure.Data;
using Infrastructure.Repositories.MemberRepositories;
using Infrastructure.Repositories.TokenRepositories;
using Infrastructure.Services.AuthenticateServices;
using Infrastructure.Services.MemberServices;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjectionDeploy
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDBContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenRepository, TokenRepoImp>();
            services.AddScoped<IMemberRepository, MemberRepoImp>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticateService, AuthenticateServiceImp>();
            services.AddScoped<IMemberService, MemberServiceImp>();
        }
    }
}
