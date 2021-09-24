using Application.Common.Interfaces.Services.TokenServices;
using Application.Dtos.TokenDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("mms/authentication")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        public AuthenticationController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(LoginRequestDto parLogin)
        {
            var result = _authenticateService.GetJWT(parLogin);
            return Ok(result);
        }
        [HttpPut("refresh")]
        [AllowAnonymous]
        public IActionResult RefreshToken(RefreshTokenDto authenticateRequest)
        {
            var result = _authenticateService.VerifyJWT(authenticateRequest);
            return Ok(result);
        }
        [HttpPut("logout")]
        public IActionResult RevokeToken(RefreshTokenDto authenticateRequest)
        {
            var result = _authenticateService.RevokeToken(authenticateRequest);
            return Ok(result);
        }
    }
}


