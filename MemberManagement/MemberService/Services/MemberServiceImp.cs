using Common.HashCode;
using Common.HttpResponse;
using Common.UserDefinedException;
using Data.Entities;
using MemberRepository.Repository;
using MemberService.Dtos;
using System;
using System.Collections.Generic;

namespace MemberService.Services
{
    public class MemberServiceImp : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        public MemberServiceImp(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public ResponseModel<MemberGetVM> GetMemberByEmail(string email)
        {
            try
            {
                Member member = _memberRepository.GetMemberByEmail(email);
                if (member == null)
                {
                    throw new Exception(ResponseMessage.CouldNotFound);
                }
                MemberGetVM memberGetVM = new MemberGetVM
                {
                    Name = member.Name,
                    Dob = member.Dob,
                    Email = member.Email,
                    EmailOptIn = member.EmailOpt,
                    Gender = member.Gender,
                    MobileNumber = member.MobileNumber
                };
                var result = new List<MemberGetVM> { memberGetVM };
                return new ResponseModel<MemberGetVM>
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
                return new ResponseModel<MemberGetVM>
                {
                    Message = e.Message,
                    ResponseCode = ResponseCode.BadRequest,
                    Results = null,
                    TotalRecordsInDb = 0,
                    TotalResults = 0
                };
            }
        }
        public ResponseModel<int> Register(MemberCreateVM memberCreateVM)
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
                    throw new MemberException(ResponseMessage.CreateFailed);
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
        public ResponseModel<int> Update(MemberUpdateVM memberUpdateVM)
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
                    throw new MemberException(ResponseMessage.UpdateFailed);
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
