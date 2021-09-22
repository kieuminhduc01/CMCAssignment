using System;

namespace Common.UserDefinedException
{
    public class VerifyTokenException: Exception
    {
        public VerifyTokenException()
        {
        }
        public VerifyTokenException(string message) : base(message)
        {
        }
    }
}
