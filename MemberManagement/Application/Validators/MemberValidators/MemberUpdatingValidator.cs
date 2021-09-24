using Application.Common.Function;
using Application.Dtos.MemberDtos;
using FluentValidation;

namespace Application.Common.Validators.MemberValidators
{
    public class MemberUpdatingValidator : AbstractValidator<MemberUpdatingDto>
    {
        public MemberUpdatingValidator()
        {
            RuleFor(a => a.Name).NotNull().NotEmpty();
            RuleFor(a => a.MobileNumber).NotNull().NotEmpty().Must(a=>a.PhoneNumberValidate());
            RuleFor(a => a.EmailOptIn).NotNull().NotEmpty().Must(a => a.EmailValidate());
            RuleFor(a => a.Gender).NotNull().NotEmpty();
            RuleFor(a => a.Dob).NotNull().NotEmpty();
        }
    }
}
