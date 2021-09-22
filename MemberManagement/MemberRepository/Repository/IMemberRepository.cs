using Data.Entities;

namespace MemberRepository.Repository
{
    public interface IMemberRepository
    {
        Member GetMemberByUserNameAndPassword(string username, string password);
        Member GetMemberByEmail(string email);
        int UpdateMember(Member member);
        int AddNewMember(Member member);
    }
}
