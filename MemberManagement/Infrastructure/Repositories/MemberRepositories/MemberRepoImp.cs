using Application.Common.Interfaces.Repositories.MemberRepositories;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Data;
using System.Linq;

namespace Infrastructure.Repositories.MemberRepositories
{
    public class MemberRepoImp:Repository<Member>,IMemberRepository
    {
        private readonly ApplicationDBContext _context;
        public MemberRepoImp(ApplicationDBContext context):base(context)
        {
            _context = context;
        }
        public Member GetMemberByUserNameAndPassword(string username, string password)
        {
            return _context.Members.FirstOrDefault(a => a.Email.Equals(username) && a.Password.Equals(Encoding.MD5Hash(password)));
        }
       
    }
}
