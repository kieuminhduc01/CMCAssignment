using Data.Entities;

namespace TokenRepository.Repository
{
    public interface ITokenRepository
    {
        RefreshToken GetTokenDetailByTokenCode(string tokenCode);
        RefreshToken GetTokenByTokenCodeAndRefreshTokenCode(string tokenCode, string refreshTokenCode);
        int UpdateRevokedStatusForToken(string tokenRefreshCode);
        int AddNewRefreshToken(RefreshToken refreshToken);
        int UpdateRefreshToken(RefreshToken refreshToken);
    }
}
