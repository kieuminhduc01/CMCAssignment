using AuthenticateService.Dtos;

namespace AuthenticateService.Services
{
    public interface IAuthenticateService
    {
        AuthenticateResponse GetJWT(LoginVM parLogin);
        AuthenticateResponse VerifyJWT(RefreshTokenVM authenticateRequest);
        bool RevokeToken(RefreshTokenVM authenticateRequest);
    }
}
