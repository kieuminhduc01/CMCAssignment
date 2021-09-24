using Application.Common.Function;
using Application.Dtos.MemberDtos;
using FluentValidation;

namespace Application.Common.Validators.MemberValidators
{
    public class MemberCreatingValidator: AbstractValidator<MemberCreatingDto>
    {
        public MemberCreatingValidator()
        {
            RuleFor(a => a.Name).NotNull().NotEmpty();
            RuleFor(a => a.Email).NotNull().NotEmpty().Must(a => a.EmailValidate());
            RuleFor(a => a.Password).NotNull().NotEmpty();
            RuleFor(a => a.MobileNumber).NotNull().NotEmpty().Must(a => a.PhoneNumberValidate());
            RuleFor(a => a.EmailOptIn).NotNull().NotEmpty().Must(a => a.EmailValidate());
            RuleFor(a => a.Gender).NotNull().NotEmpty();
            RuleFor(a => a.Dob).NotNull().NotEmpty();
        }
        
    }
}
