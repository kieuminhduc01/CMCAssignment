using Application.Common.HTTPResponse;
using Application.Dtos.MemberDtos;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services.MemberServices
{
    public interface IMemberService
    {
        ResponseModel<int> Register(MemberCreatingDto memberCreateVM);
        Task<ResponseModel<MemberGettingDto>> GetMemberByEmail(string email);
        Task<ResponseModel<int>> Update(MemberUpdatingDto memberUpdateVM);
        Task<ResponseModel<int>> DeletingMethodForTesingUnitOfWork(MemberUpdatingDto memberUpdateVM);
    }
}
