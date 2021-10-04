using Application.Common.Interfaces.Repositories.TokenRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.TokenRepositories
{
    public class TokenRepoImp : Repository<RefreshToken>, ITokenRepository
    {
        public TokenRepoImp(ApplicationDBContext context) : base(context)
        {
        }
        public async Task<RefreshToken> GetTokenDetailByTokenCode(string tokenCode)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token.Equals(tokenCode));
        }
        public Task<RefreshToken> GetTokenByTokenCodeAndRefreshTokenCode(string tokenCode, string refreshTokenCode)
        {
            return _context.RefreshTokens.AsQueryable().FirstOrDefaultAsync(a => a.JwtId.Equals(tokenCode) && a.Token.Equals(refreshTokenCode));
        }
        public async Task UpdateRevokedStatusForToken(string tokenRefreshCode)
        {
            var storedRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token.Equals(tokenRefreshCode));
            storedRefreshToken.IsRevoked = true; 
            _context.RefreshTokens.Update(storedRefreshToken);
        }
        Task<RefreshToken> ITokenRepository.GetTokenDetailByTokenCode(string tokenCode)
        {
            throw new System.NotImplementedException();
        }
        Task<RefreshToken> ITokenRepository.GetTokenByTokenCodeAndRefreshTokenCode(string tokenCode, string refreshTokenCode)
        {
            throw new System.NotImplementedException();
        }
        Task ITokenRepository.UpdateRevokedStatusForToken(string tokenRefreshCode)
        {
            throw new System.NotImplementedException();
        }
    }
}

