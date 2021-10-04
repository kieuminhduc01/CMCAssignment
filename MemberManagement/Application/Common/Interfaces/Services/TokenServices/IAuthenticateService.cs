using Application.Dtos.TokenDtos;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services.TokenServices
{
    public interface IAuthenticateService
    {
        Task<AuthenticateGettingDto> GetJWT(LoginRequestDto parLogin);
        Task<AuthenticateGettingDto> VerifyJWT(RefreshTokenDto authenticateRequest);
        Task<bool> RevokeToken(RefreshTokenDto authenticateRequest);
    }
}
