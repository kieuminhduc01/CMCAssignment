using Application.Dtos.TokenDtos;

namespace Application.Common.Interfaces.Services.TokenServices
{
    public interface IAuthenticateService
    {
        AuthenticateGettingDto GetJWT(LoginRequestDto parLogin);
        AuthenticateGettingDto VerifyJWT(RefreshTokenDto authenticateRequest);
        bool RevokeToken(RefreshTokenDto authenticateRequest);
    }
}
