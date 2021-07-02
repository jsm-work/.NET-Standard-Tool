using System;

namespace API_Server
{
    public class Result : Interface_Response
    {
        public Result(ERROR_CODES code,
                      object response,
                      bool log = false,
                      ERROR_LANGUAGE language = ERROR_LANGUAGE.kor)
        {
            Code = (int)code;
            Message = ErrorCode.Message(code, language);
            Log = log;
            Response = response;
        }

        public int Code { get; set; }
        public string Message { get; set; }
        public bool Log { get; set; }
        public object Response { get; set; }
    }
}
