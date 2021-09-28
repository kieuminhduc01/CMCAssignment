using Application.Common.HTTPResponse;
using Application.Dtos.MemberDtos;
using Domain.Entities;

namespace Application.Common.Interfaces.Repositories.MemberRepositories
{
    public interface IMemberRepository:IRepository<Member>
    {
        ResponseModel<MemberGettingDto> GetMemberByEmail(string email);
        ResponseModel<int> Register(MemberCreatingDto memberCreateVM);
        ResponseModel<int> Update(MemberUpdatingDto memberUpdateVM);
    }
}
