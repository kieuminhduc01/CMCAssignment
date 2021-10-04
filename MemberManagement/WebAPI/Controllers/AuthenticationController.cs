using Application.Common.Interfaces.Services.TokenServices;
using Application.Dtos.TokenDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Login(LoginRequestDto parLogin)
        {
            var result =await _authenticateService.GetJWT(parLogin);
            return Ok(result);
        }
        [HttpPut("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(RefreshTokenDto authenticateRequest)
        {
            var result =await _authenticateService.VerifyJWT(authenticateRequest);
            return Ok(result);
        }
        [HttpPut("logout")]
        public async Task<IActionResult> RevokeToken(RefreshTokenDto authenticateRequest)
        {
            var result =await _authenticateService.RevokeToken(authenticateRequest);
            return Ok(result);
        }
    }
}


