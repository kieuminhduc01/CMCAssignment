using Application;
using Application.Common.Interfaces.Repositories.MemberRepositories;
using Application.Common.Interfaces.Repositories.TokenRepositories;
using Infrastructure.Data;
using System;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        public IMemberRepository Members { get; }
        public ITokenRepository Tokens { get; }


        public UnitOfWork(ApplicationDBContext context,IMemberRepository memberRepository,ITokenRepository tokenRepository)
        {
            this._context = context;
            this.Tokens = tokenRepository;
            this.Members = memberRepository;
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);  
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
