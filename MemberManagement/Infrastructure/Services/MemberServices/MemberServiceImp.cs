using Application.Common.HTTPResponse;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services.MemberServices;
using Application.Dtos.MemberDtos;
using AutoMapper;
using Domain.Entities;
using Infrastructure.UserDefineException;
using System;
using System.Collections.Generic;

namespace Infrastructure.Services.MemberServices
{
    public class MemberServiceImp:IMemberService
    {
        private readonly IRepository<Member> _memberRepository;
        private readonly IMapper _mapper;
        public MemberServiceImp(IRepository<Member> memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }
        public ResponseModel<MemberGettingDto> GetMemberByEmail(string email)
        {
            try
            {
                Member member = _memberRepository.GetById(email);
                if (member == null)
                {
                    throw new Exception(ResponseMessage.CouldNotFound);
                }
                MemberGettingDto memberGetting = _mapper.Map<MemberGettingDto>(member);
                var result = new List<MemberGettingDto> { memberGetting };

                return ResponseModel<MemberGettingDto>.Success(result, ResponseCode.OK, ResponseMessage.GetSuccessfully);
            }
            catch (Exception e)
            {
                return ResponseModel<MemberGettingDto>.Fail(ResponseCode.BadRequest, e.Message);
            }
        }
        public ResponseModel<int> Register(MemberCreatingDto memberCreateVM)
        {
            try
            {
                Member member = _mapper.Map<Member>(memberCreateVM);
                _memberRepository.Insert(member);
                return ResponseModel<int>.Success(ResponseCode.OK, ResponseMessage.GetSuccessfully);
            }
            catch (Exception e)
            {
                return ResponseModel<int>.Fail(ResponseCode.BadRequest, e.Message);
            }
        }
        public ResponseModel<int> Update(MemberUpdatingDto memberUpdateVM)
        {
            try
            {
                Member member = _mapper.Map<Member>(memberUpdateVM);
                var result = _memberRepository.Update(member);
                if (result < 1)
                {
                    throw new MemberManagementException(ResponseMessage.UpdateFailed);
                }
                return ResponseModel<int>.Success(result, ResponseCode.OK, ResponseMessage.GetSuccessfully);
            }
            catch (Exception e)
            {
                return ResponseModel<int>.Fail(ResponseCode.BadRequest, e.Message);
            }
        }
    }
}
