using AuthenticateService.Dtos;
using AuthenticateService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentForMemeberManagement.Controllers
{
    [Route("mms/token")]
    [ApiController]
    [Authorize]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        public TokenController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(LoginVM parLogin)
        {
            var result = _authenticateService.GetJWT(parLogin);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
        [HttpPut("refresh")]
        [AllowAnonymous]
        public IActionResult RefreshToken(RefreshTokenVM authenticateRequest)
        {
            var result = _authenticateService.VerifyJWT(authenticateRequest);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
        [HttpPut("logout")]
        public IActionResult RevokeToken(RefreshTokenVM authenticateRequest)
        {
            var result = _authenticateService.RevokeToken(authenticateRequest);
            if (result)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
