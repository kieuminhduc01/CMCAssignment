using Application;
using Application.Common.Interfaces.Repositories.MemberRepositories;
using Application.Common.Interfaces.Repositories.TokenRepositories;
using Infrastructure.Data;
namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDBContext _context;
        private IMemberRepository _members;
        public ITokenRepository Tokens { get; }

        public UnitOfWork(ApplicationDBContext context, IMemberRepository member)
        {
            _context = context;
            _members = member;
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public IMemberRepository Members()
        {
            return _members;
        }
    }
}
