using Application.Common.Interfaces.Repositories.MemberRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.MemberRepositories
{
    public class MemberRepoImp : Repository<Member>, IMemberRepository
    {
        public MemberRepoImp(ApplicationDBContext context) : base(context)
        {
        }
        public async Task<Member> GetMemberByUserNameAndPassword(string username, string password)
        {
            return await _context.Members.FirstOrDefaultAsync(a => a.Email.Equals(username) && a.Password.Equals(password));
        }
    }
}
