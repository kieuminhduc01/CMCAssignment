using System;

namespace Common.UserDefinedException
{
    public class MemberException:Exception
    {
        public MemberException()
        {
        }
        public MemberException(string message) : base(message)
        {
        }
    }
}
