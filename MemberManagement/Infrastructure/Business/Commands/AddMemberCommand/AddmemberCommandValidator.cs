using Application.Common.HTTPResponse;
using FluentValidation;
using Application.Common.Function;

namespace Infrastructure.Business.Commands.AddMemberCommand
{
    public class AddmemberCommandValidator : AbstractValidator<AddMemberCommand>
    {
        public AddmemberCommandValidator()
        {
            RuleFor(a => a.Name).NotNull().NotEmpty();

            RuleFor(a => a.Email).NotNull().NotEmpty()
                .Must(a => a.EmailValidate()).WithMessage(ResponseMessage.EmailInvalid);

            RuleFor(a => a.Password).NotNull().NotEmpty();
            RuleFor(a => a.MobileNumber).NotNull().NotEmpty().Must(a => a.PhoneNumberValidate())
                .WithMessage(ResponseMessage.PhoneNumberInvalid);
            RuleFor(a => a.EmailOpt).NotNull().NotEmpty().Must(a => a.EmailValidate())
                .WithMessage(ResponseMessage.EmailInvalid);
            RuleFor(a => a.Gender).Must(a => a.GenderValidate()).WithMessage(ResponseMessage.GenderInvalid);
            RuleFor(a => a.Dob).NotNull().NotEmpty();
        }
    }
}
