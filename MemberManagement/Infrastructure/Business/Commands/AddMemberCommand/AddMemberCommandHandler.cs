using Application;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Business.Commands.AddMemberCommand
{
    public class AddMemberCommandHandler : IRequestHandler<AddMemberCommand,bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddMemberCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AddMemberCommand request, CancellationToken cancellationToken)
        {
            Member member = _mapper.Map<Member>(request);
            await _unitOfWork.Members.Add(member);
            var result = _unitOfWork.Complete();
            if (result != null)
            {
                return true;
            }
            return false;
        }
    }
}
