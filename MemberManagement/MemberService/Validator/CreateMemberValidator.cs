using Common.Validator;
using FluentValidation;
using MemberService.Dtos;

namespace MemberService.Validator
{
    public class CreateMemberValidator: AbstractValidator<MemberCreateVM>
    {
        public CreateMemberValidator()
        {
            RuleFor(a => a.Email).NotNull().NotEmpty().Must(a => CommonValidator.EmailValidate(a));
            RuleFor(a => a.Name).NotNull().NotEmpty();
        }
    }
}
