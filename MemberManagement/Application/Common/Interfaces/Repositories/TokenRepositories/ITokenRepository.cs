using Domain.Entities;

namespace Application.Common.Interfaces.Repositories.TokenRepositories
{
    public interface ITokenRepository:IRepository<RefreshToken>
    {
        RefreshToken GetTokenDetailByTokenCode(string tokenCode);
        RefreshToken GetTokenByTokenCodeAndRefreshTokenCode(string tokenCode, string refreshTokenCode);
        void UpdateRevokedStatusForToken(string tokenRefreshCode);
    }
}
