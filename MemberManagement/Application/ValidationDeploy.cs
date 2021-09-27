using Application.Common.Validators.MemberValidators;
using Application.Dtos.MemberDtos;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ValidationDeploy
    {
        public static void AddValidation(this IServiceCollection services)
        {
            services.AddTransient<IValidator<MemberCreatingDto>, MemberCreatingValidator>();
            services.AddTransient<IValidator<MemberUpdatingDto>, MemberUpdatingValidator>();
        }
    }
}
