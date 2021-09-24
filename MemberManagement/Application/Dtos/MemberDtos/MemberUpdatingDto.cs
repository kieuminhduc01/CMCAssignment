using Domain.Enums;
using System;

namespace Application.Dtos.MemberDtos
{
    public class MemberUpdatingDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public Gender Gender { get; set; }
        public DateTime Dob { get; set; }
        public string EmailOptIn { get; set; }
    }
}
