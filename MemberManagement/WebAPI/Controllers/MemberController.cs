using Application.Business.Commands.AddMemberCommand;
using Application.Common.Interfaces.Services.MemberServices;
using Application.Dtos.MemberDtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("mms/member")]
    [ApiController]
    [Authorize]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly IMediator _mediator;
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> Register(MemberCreatingDto parMember)
        {
            //var result = _memberService.Register(parMember);
            var result = await _mediator.Send(new AddMemberCommand
            {
                Email ="",
                // TODO
            });
            return Ok(result);
        }
        [HttpGet("{email}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMemberInfo(string email)
        {
            var result = await _memberService.GetMemberByEmail(email);
            return Ok(result);
        }
        [HttpPut()]
        public async Task<IActionResult> GetMemberInfo(MemberUpdatingDto parMember)
        {
            var result = await _memberService.Update(parMember);
            return Ok(result);
        }
        [HttpDelete("/test/unit-of-work")]
        public async Task<IActionResult> TestUnitOfWork(MemberUpdatingDto parMember)
        {
            var result = await _memberService.DeletingMethodForTesingUnitOfWork(parMember);
            return Ok(result);
        }
        [HttpDelete()]
        public async Task<IActionResult> DeleteMember(MemberUpdatingDto parMember)
        {
            var result = await _memberService.DeletingMethodForTesingUnitOfWork(parMember);
            return Ok(result);
        }
    }
}
