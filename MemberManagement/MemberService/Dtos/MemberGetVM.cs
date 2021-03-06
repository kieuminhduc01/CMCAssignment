using Data.Enums;
using System;

namespace MemberService.Dtos
{
    public class MemberGetVM
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public Gender Gender { get; set; }
        public DateTime Dob { get; set; }
        public string EmailOptIn { get; set; }
    }
}
