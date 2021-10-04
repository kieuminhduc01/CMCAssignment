using Application.Common.Interfaces.Repositories.MemberRepositories;
using Application.Common.Interfaces.Repositories.TokenRepositories;
using System;

namespace Application
{
    public interface IUnitOfWork : IDisposable
    {
        IMemberRepository Members();
        ITokenRepository Tokens {  get; }
        int Complete();
    }
}
