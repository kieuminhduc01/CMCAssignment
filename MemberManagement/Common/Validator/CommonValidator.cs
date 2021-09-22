using System.Text.RegularExpressions;

namespace Common.Validator
{
    public class CommonValidator
    {
        public static bool EmailValidate(string email)
        {
            if (email == null)
            {
                return true;
            }
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
            {
                return true;
            }
            return false;
        }
    }
}
