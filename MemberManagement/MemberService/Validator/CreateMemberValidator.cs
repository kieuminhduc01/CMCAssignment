using Common.Validator;
using FluentValidation;
using MemberService.Dtos;

namespace MemberService.Validator
{
    public class CreateMemberValidator: AbstractValidator<MemberCreateVM>
    {
        public CreateMemberValidator()
        {
            RuleFor(a => a.Name).NotNull().NotEmpty();
            RuleFor(a => a.Email).NotNull().NotEmpty().Must(a => CommonValidator.EmailValidate(a));
            RuleFor(a => a.Password).NotNull().NotEmpty();
            RuleFor(a => a.MobileNumber).NotNull().NotEmpty().Must(a=>CommonValidator.PhoneNumberValidate(a));
            RuleFor(a => a.EmailOptIn).NotNull().NotEmpty().Must(a => CommonValidator.EmailValidate(a));
            RuleFor(a => a.Gender).NotNull().NotEmpty();
            RuleFor(a => a.Dob).NotNull().NotEmpty();
        }
    }
}
