using Application.Dtos.MemberDtos;
using Infrastructure.Business.Commands.AddMemberCommand;
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

        private readonly IMediator _mediator;
        public MemberController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> Register(AddMemberCommand parMember)
        {
            var result = await _mediator.Send(parMember);
            return Ok(result);
        }
        [HttpGet("{email}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMemberInfo(string email)
        {
            var result = await _mediator.Send(email);
            return Ok(result);
        }
        //[HttpPut()]
        //public async Task<IActionResult> GetMemberInfo(MemberUpdatingDto parMember)
        //{
        //    var result = await _mediator.Update(parMember);
        //    return Ok(result);
        //}
        //[HttpDelete("/test/unit-of-work")]
        //public async Task<IActionResult> TestUnitOfWork(MemberUpdatingDto parMember)
        //{
        //    var result = await _mediator.DeletingMethodForTesingUnitOfWork(parMember);
        //    return Ok(result);
        //}
        //[HttpDelete()]
        //public async Task<IActionResult> DeleteMember(MemberUpdatingDto parMember)
        //{
        //    var result = await _mediator.DeletingMethodForTesingUnitOfWork(parMember);
        //    return Ok(result);
        //}
    }
}
