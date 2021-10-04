using Domain.Enums;
using MediatR;
using System;

namespace Infrastructure.Business.Commands.AddMemberCommand
{
    public class AddMemberCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public Gender Gender { get; set; }
        public DateTime Dob { get; set; }
        public string EmailOpt { get; set; }
    }
}
