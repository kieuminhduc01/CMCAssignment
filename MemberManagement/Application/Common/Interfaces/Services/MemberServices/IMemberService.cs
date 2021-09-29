using Application.Common.HTTPResponse;
using Application.Dtos.MemberDtos;

namespace Application.Common.Interfaces.Services.MemberServices
{
    public interface IMemberService
    {
        ResponseModel<int> Register(MemberCreatingDto memberCreateVM);
        ResponseModel<MemberGettingDto> GetMemberByEmail(string email);
        ResponseModel<int> Update(MemberUpdatingDto memberUpdateVM);
        ResponseModel<int> DeletingMethodForTesingUnitOfWork(MemberUpdatingDto memberUpdateVM);
    }
}
