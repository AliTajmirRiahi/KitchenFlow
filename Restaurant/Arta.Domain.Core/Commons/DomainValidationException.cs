using Arta.Base.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Arta.Domain.Core.Commons
{
    public class DomainValidationException : BaseException
    {
        public DomainValidationException(string message, string errorCode, HttpStatusCode statusCode) : base(message, errorCode, statusCode)
        {
        }
    }
}
