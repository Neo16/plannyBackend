using System;
using System.Collections.Generic;
using System.Text;

namespace PlannyBackend.BLL.Exceptions
{
    public enum ErrorCode
    {
        InvalidArgument = 400,
        Forbidden = 403,
        NotFound = 404
    }
}

