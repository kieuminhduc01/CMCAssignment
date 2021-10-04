using Application.Common.Interfaces.Repositories.MemberRepositories;
using Application.Common.Interfaces.Repositories.TokenRepositories;
using System;
using System.Threading.Tasks;

namespace Application
{
    public interface IUnitOfWork : IDisposable
    {
        IMemberRepository Members { get; }
        ITokenRepository Tokens {  get; }
        Task<int> Complete();
    }
}
