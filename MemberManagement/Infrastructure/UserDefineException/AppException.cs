using System;

namespace Infrastructure.UserDefineException
{
    public class AppException:Exception
    {
        public AppException(string message):base(message)
        {
        }
        public AppException()
        {
        }
    }
}
