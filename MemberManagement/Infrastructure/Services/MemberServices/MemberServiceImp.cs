using Application.Common.HTTPResponse;
using Application.Common.Interfaces.Repositories.MemberRepo;
using Application.Common.Interfaces.Services.MemberServices;
using Application.Dtos.MemberDtos;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.UserDefineException;
using System;
using System.Collections.Generic;

namespace Infrastructure.Services.MemberServices
{
    public class MemberServiceImp:IMemberService
    {
        private readonly IMemberRepo _memberRepository;
        public MemberServiceImp(IMemberRepo memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public ResponseModel<MemberGettingDto> GetMemberByEmail(string email)
        {
            try
            {
                Member member = _memberRepository.GetMemberByEmail(email);
                if (member == null)
                {
                    throw new Exception(ResponseMessage.CouldNotFound);
                }
                MemberGettingDto memberGetVM = new MemberGettingDto
                {
                    Name = member.Name,
                    Dob = member.Dob,
                    Email = member.Email,
                    EmailOptIn = member.EmailOpt,
                    Gender = member.Gender,
                    MobileNumber = member.MobileNumber
                };
                var result = new List<MemberGettingDto> { memberGetVM };
                return new ResponseModel<MemberGettingDto>
                {
                    Message = ResponseMessage.GetSuccessfully,
                    ResponseCode = ResponseCode.OK,
                    Results = result,
                    TotalRecordsInDb = result.Count,
                    TotalResults = result.Count
                };
            }
            catch (Exception e)
            {
                return new ResponseModel<MemberGettingDto>
                {
                    Message = e.Message,
                    ResponseCode = ResponseCode.BadRequest,
                    Results = null,
                    TotalRecordsInDb = 0,
                    TotalResults = 0
                };
            }
        }
        public ResponseModel<int> Register(MemberCreatingDto memberCreateVM)
        {
            try
            {
                Member member = new Member
                {
                    Email = memberCreateVM.Email,
                    Name = memberCreateVM.Name,
                    Dob = memberCreateVM.Dob,
                    Gender = memberCreateVM.Gender,
                    MobileNumber = memberCreateVM.MobileNumber,
                    Password = Encoding.MD5Hash(memberCreateVM.Password),
                    EmailOpt = memberCreateVM.EmailOptIn
                };
                var result = _memberRepository.AddNewMember(member);
                if (result < 1)
                {
                    throw new MemberManagementException(ResponseMessage.CreateFailed);
                }
                return new ResponseModel<int>
                {
                    Message = ResponseMessage.CreateSuccessfully,
                    ResponseCode = ResponseCode.Created,
                    TotalResults = result,
                };
            }
            catch (Exception e)
            {
                return new ResponseModel<int>
                {
                    Message = e.Message,
                    ResponseCode = ResponseCode.BadRequest,
                    TotalResults = 0,
                };
            }
        }
        public ResponseModel<int> Update(MemberUpdatingDto memberUpdateVM)
        {
            try
            {
                Member member = new Member
                {
                    Email = memberUpdateVM.Email,
                    Name = memberUpdateVM.Name,
                    MobileNumber = memberUpdateVM.MobileNumber,
                    Gender = memberUpdateVM.Gender,
                    Dob = memberUpdateVM.Dob,
                    EmailOpt = memberUpdateVM.EmailOptIn
                };
                var result = _memberRepository.UpdateMember(member);
                if (result < 1)
                {
                    throw new MemberManagementException(ResponseMessage.UpdateFailed);
                }
                return new ResponseModel<int>
                {
                    Message = ResponseMessage.UpdateSuccessfully,
                    ResponseCode = ResponseCode.OK,
                    TotalResults = result,
                };
            }
            catch (Exception e)
            {
                return new ResponseModel<int>
                {
                    Message = e.Message,
                    ResponseCode = ResponseCode.BadRequest,
                    TotalResults = 0,
                };
            }
        }
    }
}
