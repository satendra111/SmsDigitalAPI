using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concrete
{
    public class APIException : Exception
    {
        public ErrorCodes Error { get; }
        public Exception Exception { get; }
        public IDictionary<string, string> Context { get; }
        public APIException(ErrorCodes error, Exception exception = null, IDictionary<string, string> context = null)
        {
            Error = error;
            Exception = exception;
            Context = context;
        }
    }
}
