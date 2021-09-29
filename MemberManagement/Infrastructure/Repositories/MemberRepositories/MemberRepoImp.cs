using Application.Common.Interfaces.Repositories.MemberRepositories;
using Domain.Entities;
using Infrastructure.Common;
    using Infrastructure.Data;

namespace Infrastructure.Repositories.MemberRepositories
{
    public class MemberRepoImp : Repository<Member>, IMemberRepository
    {
        public MemberRepoImp(ApplicationDBContext context) : base(context)
        {
        }
        public async Member GetMemberByUserNameAndPassword(string username, string password)
        {
           return await _context
        }

    }
}
