using Application;
using Application.Common.HTTPResponse;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services.MemberServices;
using Application.Dtos.MemberDtos;
using AutoMapper;
using Domain.Entities;
using Infrastructure.UserDefineException;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services.MemberServices
{
    public class MemberServiceImp : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Member> _memberRepository;
        private readonly IMapper _mapper;
        public MemberServiceImp(IRepository<Member> memberRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public ResponseModel<MemberGettingDto> GetMemberByEmail(string email)
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
        public ResponseModel<int> Register(MemberCreatingDto memberCreateVM)
        {

            Member member = _mapper.Map<Member>(memberCreateVM);
            _memberRepository.Insert(member);
            _unitOfWork.Commit();

            return ResponseModel<int>.Success(ResponseCode.OK, ResponseMessage.CreateSuccessfully);
        }
        public ResponseModel<int> Update(MemberUpdatingDto memberUpdateVM)
        {
            Member member = _mapper.Map<Member>(memberUpdateVM);
            _memberRepository.Update(member);
            _unitOfWork.Commit();
            return ResponseModel<int>.Success(ResponseCode.OK, ResponseMessage.GetSuccessfully);
        }

        public ResponseModel<int> DeletingMethodForTesingUnitOfWork(MemberUpdatingDto memberUpdateVM)
        {
            Member member = _mapper.Map<Member>(memberUpdateVM);
            _memberRepository.Delete(member);
            _memberRepository.Update(member);
            _unitOfWork.Commit();
            return ResponseModel<int>.Success(ResponseCode.OK, ResponseMessage.DeleteSuccessfully);
        }
    }
}
