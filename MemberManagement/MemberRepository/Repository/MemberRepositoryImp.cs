using Common.HashCode;
using Data.DBContext;
using Data.Entities;
using System.Linq;

namespace MemberRepository.Repository
{
    public class MemberRepositoryImp : IMemberRepository
    {
        private readonly DataContext _context;
        public MemberRepositoryImp(DataContext context)
        {
            _context = context;
        }
        public Member GetMemberByUserNameAndPassword(string username, string password)
        {
            return _context.Members.FirstOrDefault(a => a.Email.Equals(username) && a.Password.Equals(Encoding.MD5Hash(password)));
        }
        public Member GetMemberByEmail(string email)
        {
            return _context.Members.FirstOrDefault(a => a.Email.Equals(email));
        }

        public int AddNewMember(Member prMember)
        {
            _context.Members.Add(prMember);
            return _context.SaveChanges();
        }

        public int UpdateMember(Member prMember)
        {
            var currentMember=_context.Members.FirstOrDefault(a => a.Email.Equals(prMember.Email));
            if (currentMember != null)
            {
                currentMember.Name = prMember.Name;
                currentMember.MobileNumber = prMember.MobileNumber;
                currentMember.Gender = prMember.Gender;
                currentMember.Dob = prMember.Dob;
                currentMember.EmailOpt = prMember.EmailOpt;
                return _context.SaveChanges();
            }
            return 0;
        }
    }
}
