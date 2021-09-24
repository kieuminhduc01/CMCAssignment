using Domain.Entities;

namespace Application.Common.Interfaces.Repositories.MemberRepo
{
    public interface IMemberRepo
    {
        Member GetMemberByUserNameAndPassword(string username, string password);
        Member GetMemberByEmail(string email);
        int UpdateMember(Member member);
        int AddNewMember(Member member);
    }
}
