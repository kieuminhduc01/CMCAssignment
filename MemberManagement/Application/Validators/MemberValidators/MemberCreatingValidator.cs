using Application.Common.Function;
using Application.Common.HTTPResponse;
using Application.Common.Interfaces.Repositories.MemberRepo;
using Application.Dtos.MemberDtos;
using FluentValidation;

namespace Application.Common.Validators.MemberValidators
{
    public class MemberCreatingValidator: AbstractValidator<MemberCreatingDto>
    {
        private readonly IMemberRepo _memberRepo;
        public MemberCreatingValidator(IMemberRepo memberRepo)
        {
            _memberRepo = memberRepo;

            RuleFor(a => a.Name).NotNull().NotEmpty();

            RuleFor(a => a.Email).NotNull().NotEmpty()
                .Must(a => a.EmailValidate()).WithMessage(ResponseMessage.EmailInvalid)
                .Must(a => !IsEmailExist(a)).WithMessage(ResponseMessage.EmailExist);

            RuleFor(a => a.Password).NotNull().NotEmpty();
            RuleFor(a => a.MobileNumber).NotNull().NotEmpty().Must(a => a.PhoneNumberValidate())
                .WithMessage(ResponseMessage.PhoneNumberInvalid);
            RuleFor(a => a.EmailOpt).NotNull().NotEmpty().Must(a => a.EmailValidate())
                .WithMessage(ResponseMessage.EmailInvalid);
            RuleFor(a => a.Gender).Must(a=>a.GenderValidate()).WithMessage(ResponseMessage.GenderInvalid);
            RuleFor(a => a.Dob).NotNull().NotEmpty();
        }
        private bool IsEmailExist(string email)
        {
            var member = _memberRepo.GetMemberByEmail(email);
            if (member == null)
            {
                return false;
            }
            return true;
        }
        
    }
}
