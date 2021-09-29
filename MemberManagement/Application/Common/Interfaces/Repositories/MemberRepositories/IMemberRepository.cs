using Application.Common.HTTPResponse;
using Application.Dtos.MemberDtos;
using Domain.Entities;

namespace Application.Common.Interfaces.Repositories.MemberRepositories
{
    public interface IMemberRepository:IRepository<Member>
    {
        Member GetMemberByUserNameAndPassword(string username, string password);
    }
}
