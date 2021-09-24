using Application.Common.Function;
using Application.Common.HTTPResponse;
using Application.Dtos.MemberDtos;
using FluentValidation;

namespace Application.Common.Validators.MemberValidators
{
    public class MemberUpdatingValidator : AbstractValidator<MemberUpdatingDto>
    {
        public MemberUpdatingValidator()
        {
            RuleFor(a => a.Name).NotNull().NotEmpty();
            RuleFor(a => a.MobileNumber).NotNull().NotEmpty().Must(a=>a.PhoneNumberValidate())
                .WithMessage(ResponseMessage.PhoneNumberInvalid);
            RuleFor(a => a.EmailOpt).NotNull().NotEmpty().Must(a => a.EmailValidate())
                .WithMessage(ResponseMessage.EmailInvalid) ;
            RuleFor(a => a.Gender).Must(a => a.GenderValidate())
                .WithMessage(ResponseMessage.GenderInvalid);
            RuleFor(a => a.Dob).NotNull().NotEmpty();
        }
    }
}
