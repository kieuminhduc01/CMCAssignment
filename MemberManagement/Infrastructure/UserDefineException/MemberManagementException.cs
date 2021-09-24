using System;

namespace Infrastructure.UserDefineException
{
    public class MemberManagementException:Exception
    {
        public MemberManagementException(string message):base(message)
        {
        }
        public MemberManagementException()
        {
        }
    }
}
