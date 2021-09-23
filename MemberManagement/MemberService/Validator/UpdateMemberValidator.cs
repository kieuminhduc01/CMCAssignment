using Common.Validator;
using FluentValidation;
using MemberService.Dtos;

namespace MemberService.Validator
{
    public class UpdateMemberValidator: AbstractValidator<MemberUpdateVM>
    {
        public UpdateMemberValidator()
        {
            RuleFor(a => a.Name).NotNull().NotEmpty();
            RuleFor(a => a.MobileNumber).NotNull().NotEmpty();
            RuleFor(a => a.EmailOptIn).NotNull().NotEmpty().Must(a=>CommonValidator.EmailValidate(a));
            RuleFor(a => a.Gender).NotNull().NotEmpty();
            RuleFor(a => a.Dob).NotNull().NotEmpty();
        }
    }
}
