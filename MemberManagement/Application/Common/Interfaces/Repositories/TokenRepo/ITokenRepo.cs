using Domain.Entities;

namespace Application.Common.Interfaces.Repositories.TokenRepo
{
    public interface ITokenRepo
    {
        RefreshToken GetTokenDetailByTokenCode(string tokenCode);
        RefreshToken GetTokenByTokenCodeAndRefreshTokenCode(string tokenCode, string refreshTokenCode);
        int UpdateRevokedStatusForToken(string tokenRefreshCode);
        int AddNewRefreshToken(RefreshToken refreshToken);
        int UpdateRefreshToken(RefreshToken refreshToken);
    }
}
