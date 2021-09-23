using System.Text.RegularExpressions;

namespace Common.Validator
{
    public class CommonValidator
    {
        public static bool EmailValidate(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
            {
                return true;
            }
            return false;
        }
        public static bool PhoneNumberValidate(string phoneNumber)
        {
            Regex regex = new Regex(@"(84|0[3|5|7|8|9])+([0-9]{8})");
            Match match = regex.Match(phoneNumber);
            if (match.Success)
            {
                return true;
            }
            return false;
        }
    }
}
