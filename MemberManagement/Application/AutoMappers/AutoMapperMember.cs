using Application.Dtos.MemberDtos;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMappers
{
    public class AutoMapperMember:Profile
    {
        public AutoMapperMember()
        {
            CreateMap<MemberCreatingDto, Member>();
            CreateMap<MemberUpdatingDto, Member>();
            CreateMap<Member,MemberGettingDto>();
        }
    }
}
