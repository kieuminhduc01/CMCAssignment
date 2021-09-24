using Domain.Enums;
using System;

namespace Application.Dtos.MemberDtos
{
    public class MemberCreatingDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public Gender Gender { get; set; }
        public DateTime Dob { get; set; }
        public string EmailOptIn { get; set; }
    }
}
