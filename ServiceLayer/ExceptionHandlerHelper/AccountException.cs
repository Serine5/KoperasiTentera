using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ExceptionHandlerHelper
{
    public class AccountException : Exception
    {
        public int ErrorCode { get; }

        public AccountException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
        public static AccountException Create(int errorCode, string message) => new(errorCode, message);
    }
    public static class AccountErrorCodes
    {
        public const int AccountAlreadyExists = 1001;
        public const int InvalidOtp = 1002;
        public const int UserNotFound = 1003;
        public const int PinMismatch = 1004;
        public const int InvalidEmail = 1005;
        public const int InvalidPhoneNumber = 1006;
        public const int UnexpectedError = 5000;
    }

}
