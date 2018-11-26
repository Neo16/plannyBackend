using System;
using System.Collections.Generic;
using System.Text;

namespace PlannyBackend.BLL.Exceptions
{
    [Serializable]
    public class BusinessLogicException : System.Exception
    {
        public ErrorCode ErrorCode { get; set; }

        public BusinessLogicException() { }
        public BusinessLogicException(string message) : base(message) { }
        public BusinessLogicException(string message, Exception inner) : base(message, inner) { }
    }
}
