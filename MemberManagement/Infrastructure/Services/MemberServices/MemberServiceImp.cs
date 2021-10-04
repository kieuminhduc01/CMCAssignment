using Application;
using Application.Common.HTTPResponse;
using Application.Common.Interfaces.Services.MemberServices;
using Application.Dtos.MemberDtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services.MemberServices
{
    public class MemberServiceImp : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MemberServiceImp(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseModel<MemberGettingDto>> GetMemberByEmail(string email)
        {

            Member member = await _unitOfWork.Members().Get(email);
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
            _unitOfWork.Members().Add(member);
            _unitOfWork.Complete();
            return ResponseModel<int>.Success(ResponseCode.OK, ResponseMessage.CreateSuccessfully);
        }
        public async Task<ResponseModel<int>> Update(MemberUpdatingDto memberUpdateVM)
        {
            Member member = _mapper.Map<Member>(memberUpdateVM);
            _unitOfWork.Members().Update(member);
            _unitOfWork.Complete();
            return ResponseModel<int>.Success(ResponseCode.OK, ResponseMessage.GetSuccessfully);
        }

        public ResponseModel<int> DeletingMethodForTesingUnitOfWork(MemberUpdatingDto memberUpdateVM)
        {
            Member member = _mapper.Map<Member>(memberUpdateVM);
            _unitOfWork.Members().Delete(member);
            _unitOfWork.Members().Update(member);
            _unitOfWork.Complete();
            return ResponseModel<int>.Success(ResponseCode.OK, ResponseMessage.DeleteSuccessfully);
        }

        Task<ResponseModel<int>> IMemberService.Update(MemberUpdatingDto memberUpdateVM)
        {
            throw new NotImplementedException();
        }

        Task<ResponseModel<int>> IMemberService.DeletingMethodForTesingUnitOfWork(MemberUpdatingDto memberUpdateVM)
        {
            throw new NotImplementedException();
        }
    }
}
