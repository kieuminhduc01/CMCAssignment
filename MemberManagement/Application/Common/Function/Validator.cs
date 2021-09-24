using Domain.Enums;
using System;
using System.Text.RegularExpressions;

namespace Application.Common.Function
{
    public static class Validator
    {
        public static bool EmailValidate(this string email)
        {
            if (email != null)
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                if (match.Success)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool PhoneNumberValidate(this string phoneNumber)
        {
            if (phoneNumber != null)
            {
                Regex regex = new Regex(@"(84|0[3|5|7|8|9])+([0-9]{8})");
                Match match = regex.Match(phoneNumber);
                if (match.Success)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool GenderValidate(this Gender gender)
        {
            if (gender != null)
            {
                foreach (Gender currenGender in Enum.GetValues(typeof(Gender)))
                {
                    if (gender.Equals(currenGender))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
