using System;

namespace Common.UserDefinedException
{
    public class LoginFailException:Exception
    {
        public LoginFailException()
        {
        }
        public LoginFailException(string message) : base(message)
        {
        }
    }
}
