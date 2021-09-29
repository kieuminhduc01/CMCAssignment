using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories.TokenRepositories
{
    public interface ITokenRepository : IRepository<RefreshToken>
    {
        Task<RefreshToken> GetTokenDetailByTokenCode(string tokenCode);
        Task<RefreshToken> GetTokenByTokenCodeAndRefreshTokenCode(string tokenCode, string refreshTokenCode);
        Task UpdateRevokedStatusForToken(string tokenRefreshCode);
    }
}
