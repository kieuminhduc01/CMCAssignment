using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Business.Commands.AddMemberCommand
{
    public class AddmemberCommandValidator : AbstractValidator<AddMemberCommand>
    {
        public AddmemberCommandValidator()
        {
            
        }
    }
}
