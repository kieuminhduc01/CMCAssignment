using Application;
using Application.Common.Interfaces.Repositories.MemberRepositories;
using Application.Common.Interfaces.Repositories.TokenRepositories;
using Infrastructure.Data;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDBContext _context;
        private IMemberRepository _members;
        public ITokenRepository Tokens { get; }

        IMemberRepository IUnitOfWork.Members => throw new System.NotImplementedException();

        public UnitOfWork(ApplicationDBContext context, IMemberRepository member)
        {
            _context = context;
            _members = member;
        }
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
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
