using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Arta.Base.Core.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message, string errorCode)
            : base(message,errorCode, HttpStatusCode.NotFound)
        {
        }
    }
}
