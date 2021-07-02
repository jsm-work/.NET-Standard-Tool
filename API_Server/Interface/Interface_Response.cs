using System;
using System.Collections.Generic;
using System.Text;

namespace API_Server
{
    interface Interface_Response
    {
        int Code { get; set; }

        string Message { get; set; }

        bool Log { get; set; }

        object Response { get; set; }

    }
}
