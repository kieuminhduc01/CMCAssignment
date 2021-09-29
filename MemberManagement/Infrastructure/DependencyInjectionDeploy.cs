using Application;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Repositories.MemberRepositories;
using Application.Common.Interfaces.Repositories.TokenRepositories;
using Application.Common.Interfaces.Services.MemberServices;
using Application.Common.Interfaces.Services.TokenServices;
using Domain.Entities;
using Infrastructure.Repositories;
using Infrastructure.Repositories.MemberRepositories;
using Infrastructure.Repositories.TokenRepositories;
using Infrastructure.Services.AuthenticateServices;
using Infrastructure.Services.MemberServices;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjectionDeploy
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenRepository, TokenRepoImp>();
            services.AddScoped<IMemberRepository, MemberRepoImp>();
            services.AddScoped(typeof(IRepository<RefreshToken>), typeof(Repository<RefreshToken>));
            services.AddScoped(typeof(IRepository<Member>), typeof(Repository<Member>));
            services.AddScoped<IAuthenticateService,AuthenticateServiceImp>();
            services.AddScoped<IMemberService,MemberServiceImp>();
        }
    }
}
