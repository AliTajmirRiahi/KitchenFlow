using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Arta.Base.Core.Exceptions
{
    public class BaseException : Exception
    {
        public string ErrorCode { get; }
        public HttpStatusCode StatusCode { get; }

        public BaseException(string message, string errorCode, HttpStatusCode statusCode)
            : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }
    }
}
