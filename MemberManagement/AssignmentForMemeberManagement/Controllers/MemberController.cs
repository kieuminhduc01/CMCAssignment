using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MemberService.Services;
using MemberService.Dtos;
using Common.HttpResponse;
using Common.UserDefinedException;
using System;

namespace AssignmentForMemeberManagement.Controllers
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
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(MemberCreateVM parMember)
        {
            var result = _memberService.Register(parMember);
            if (result.ResponseCode.Equals(ResponseCode.Created))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("{email}")]
        public IActionResult GetMemberInfo(string email)
        {
            try
            {
                if (email == null)
                {
                    throw new MemberException(ResponseMessage.ArgumentCanNotBeNull);
                }
                var result = _memberService.GetMemberByEmail(email);
                if (!result.ResponseCode.Equals(ResponseCode.OK))
                {
                    throw new MemberException(result.Message);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                var result = new ResponseModel<MemberGetVM>
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
        [HttpPut]
        public IActionResult GetMemberInfo(MemberUpdateVM parMember)
        {
            try
            {
                if (parMember == null)
                {
                    throw new MemberException(ResponseMessage.ArgumentCanNotBeNull);
                }
                var result = _memberService.Update(parMember);
                if (!result.ResponseCode.Equals(ResponseCode.OK))
                {
                    throw new MemberException(result.Message);
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
