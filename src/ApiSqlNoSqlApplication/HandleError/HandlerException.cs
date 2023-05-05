using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiNoSqlApplication.HandleError
{
    public class HandleException : Exception
    {
        public HttpStatusCode Code { get; }
        public object Errors { get; }
        public HandleException(HttpStatusCode code, object errors)
        {
            Code = code;
            Errors = errors ?? new List<string>();
        }
    }
}
