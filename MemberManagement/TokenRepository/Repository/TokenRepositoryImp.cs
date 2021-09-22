using Data.DBContext;
using Data.Entities;
using System.Linq;

namespace TokenRepository.Repository
{
    public class TokenRepositoryImp : ITokenRepository
    {
        private readonly DataContext _context;
        public TokenRepositoryImp(DataContext context)
        {
            _context = context;
        }

        public int AddNewRefreshToken(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
            return _context.SaveChanges();
        }

        public RefreshToken GetTokenDetailByTokenCode(string tokenCode)
        {
            return _context.RefreshTokens.FirstOrDefault(x => x.Token.Equals(tokenCode));
        }

        public int UpdateRefreshToken(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Update(refreshToken);
            return _context.SaveChanges();
        }
        public RefreshToken GetTokenByTokenCodeAndRefreshTokenCode(string tokenCode, string refreshTokenCode)
        {
            return _context.RefreshTokens.FirstOrDefault(a => a.JwtId.Equals(tokenCode) && a.Token.Equals(refreshTokenCode));
        }
        public int UpdateRevokedStatusForToken(string tokenRefreshCode)
        {
            var storedRefreshToken = _context.RefreshTokens.FirstOrDefault(x => x.Token == tokenRefreshCode);
            storedRefreshToken.IsRevoked = true;
            _context.RefreshTokens.Update(storedRefreshToken);
            return _context.SaveChanges();
        }
    }
}
