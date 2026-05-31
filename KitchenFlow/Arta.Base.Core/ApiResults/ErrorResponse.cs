using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arta.Base.Core.ApiResults
{
    public class ErrorResponse
    {
        public ErrorDetail Error { get; set; } = default!;
    }

    public class ErrorDetail
    {
        public string Code { get; set; } = default!;
        public string Message { get; set; } = default!;
        public string Target { get; set; } = default!;
        public List<ErrorSubDetail> Details { get; set; } = default!;
        public InnerError InnerError { get; set; } = default!;
    }

    public class ErrorSubDetail
    {
        public string Code { get; set; } = default!;
        public string Target { get; set; } = default!;
        public string Message { get; set; } = default!;
    }

    public class InnerError
    {
        public object Trace { get; set; } = default!;
        public object Context { get; set; } = default!;
    }

}
