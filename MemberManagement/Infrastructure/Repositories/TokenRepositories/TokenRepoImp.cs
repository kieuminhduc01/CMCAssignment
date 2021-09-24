using Application.Common.Interfaces.Repositories.TokenRepo;
using Domain.Data;
using Domain.Entities;
using System.Linq;

namespace Infrastructure.Repositories.TokenRepositories
{
    public class TokenRepoImp : ITokenRepo
    {
        private readonly DataContext _context;
        public TokenRepoImp(DataContext context)
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

