using Application.Common.Interfaces.Repositories.TokenRepositories;
using Domain.Entities;
using Infrastructure.Data;
using System.Linq;

namespace Infrastructure.Repositories.TokenRepositories
{
    public class TokenRepoImp : Repository<RefreshToken>,ITokenRepository
    {
        private readonly ApplicationDBContext _context;
        public TokenRepoImp(ApplicationDBContext context):base(context)
        {
            _context = context;
        }

        public RefreshToken GetTokenDetailByTokenCode(string tokenCode)
        {
            return _context.RefreshTokens.FirstOrDefault(x => x.Token.Equals(tokenCode));
        }
        public RefreshToken GetTokenByTokenCodeAndRefreshTokenCode(string tokenCode, string refreshTokenCode)
        {
            return _context.RefreshTokens.FirstOrDefault(a => a.JwtId.Equals(tokenCode) && a.Token.Equals(refreshTokenCode));
        }
        public void UpdateRevokedStatusForToken(string tokenRefreshCode)
        {
            var storedRefreshToken = _context.RefreshTokens.FirstOrDefault(x => x.Token == tokenRefreshCode);
            storedRefreshToken.IsRevoked = true;
            _context.RefreshTokens.Update(storedRefreshToken);
        }
    }
}

