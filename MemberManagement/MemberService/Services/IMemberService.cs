using Common.HttpResponse;
using MemberService.Dtos;

namespace MemberService.Services
{
    public interface IMemberService
    {
        ResponseModel<int> Register(MemberCreateVM memberCreateVM);
        ResponseModel<MemberGetVM> GetMemberByEmail(string email);
        ResponseModel<int> Update(MemberUpdateVM memberUpdateVM);
    }
}
