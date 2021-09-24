using Application.Common.HTTPResponse;
using Application.Common.Interfaces.Services.MemberServices;
using Application.Dtos.MemberDtos;
using Infrastructure.UserDefineException;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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
        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register(MemberCreatingDto parMember)
        {
            var result = _memberService.Register(parMember);
            if (result.ResponseCode.Equals(ResponseCode.Created))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("get-member-info/{email}")]
        public IActionResult GetMemberInfo(string email)
        {
            try
            {
                if (email == null)
                {
                    throw new MemberManagementException(ResponseMessage.ArgumentCanNotBeNull);
                }
                var result = _memberService.GetMemberByEmail(email);
                if (!result.ResponseCode.Equals(ResponseCode.OK))
                {
                    throw new MemberManagementException(result.Message);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                var result = new ResponseModel<MemberGettingDto>
                {
                    Message = e.Message,
                    ResponseCode = ResponseCode.BadRequest,
                    Results = null,
                    TotalRecordsInDb = 0,
                    TotalResults = 0
                };
                return BadRequest(result);
            }
        }
        [HttpPut("update")]
        public IActionResult GetMemberInfo(MemberUpdatingDto parMember)
        {
            try
            {
                if (parMember == null)
                {
                    throw new MemberManagementException(ResponseMessage.ArgumentCanNotBeNull);
                }
                var result = _memberService.Update(parMember);
                if (!result.ResponseCode.Equals(ResponseCode.OK))
                {
                    throw new MemberManagementException(result.Message);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                var result = new ResponseModel<int>
                {
                    Message = e.Message,
                    ResponseCode = ResponseCode.BadRequest,
                    TotalResults = 0,
                };
                return BadRequest(result);
            }
        }

    }
}
