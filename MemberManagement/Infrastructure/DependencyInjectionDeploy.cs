using Application.Common.Interfaces.Repositories.MemberRepo;
using Application.Common.Interfaces.Repositories.TokenRepo;
using Application.Common.Interfaces.Services.MemberServices;
using Application.Common.Interfaces.Services.TokenServices;
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
            services.AddScoped<IAuthenticateService,AuthenticateServiceImp>();
            services.AddScoped<IMemberService,MemberServiceImp>();

            services.AddScoped<IMemberRepo, MemberRepoImp>();
            services.AddScoped<ITokenRepo, TokenRepoImp>();
        }
    }
}
