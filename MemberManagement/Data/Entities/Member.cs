using Data.Enums;
using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class Member
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public Gender Gender { get; set; }
        public DateTime Dob { get; set; }
        public string EmailOpt { get; set; }

        //Foreign relation
        public ICollection<RefreshToken> RefreshTokens { get; set; }

    }
}
