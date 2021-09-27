using Application.Common.Interfaces.Services.MemberServices;
using Application.Dtos.MemberDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("mms/member")]
    [ApiController]
    [Authorize]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        [HttpPost()]
        [AllowAnonymous]
        public IActionResult Register(MemberCreatingDto parMember)
        {
            var result = _memberService.Register(parMember);
            return Ok(result);
        }
        [HttpGet("{email}")]
        public IActionResult GetMemberInfo(string email)
        {
            var result = _memberService.GetMemberByEmail(email);
            return Ok(result);
        }
        [HttpPut()]
        public IActionResult GetMemberInfo(MemberUpdatingDto parMember)
        {
            var result = _memberService.Update(parMember);
            return Ok(result);
        }

    }
}
