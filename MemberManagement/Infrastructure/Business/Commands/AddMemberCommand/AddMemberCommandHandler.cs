using Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Business.Commands.AddMemberCommand
{
    public class AddMemberCommandHandler : IRequestHandler<AddMemberCommand, bool>
    {
        private readonly ApplicationDBContext applicationDBContext;

        public AddMemberCommandHandler(ApplicationDBContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }

        public Task<bool> Handle(AddMemberCommand request, CancellationToken cancellationToken)
        {
            Aoo
            throw new NotImplementedException();
        }
    }
}
