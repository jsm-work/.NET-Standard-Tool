using System;
using System.Collections.Generic;
using System.Text;

namespace API_Server
{
    interface Interface_Code<T>
    {
        int Code { get; set; }
        string Message { get; set; }
        bool Log { get; set; }
        T Response { get; set; }
    }
}
