using System;

namespace API_Server
{
    public class Result<T> : Interface_Code<T>
    where T : class
    {
        public Result(ERROR_CODES code,
                      T response,
                      bool log = false,
                      ERROR_LANGUAGE language = ERROR_LANGUAGE.kor)
        {
            Code = (int)code;
            Message = ErrorCode.Message(code, language);
            Log = log;
            Response = response;
        }

        /// <summary>응답코드</summary>
        /// <example>200</example>
        public int Code { get; set; }

        /// <summary>응답메시지</summary>
        /// <example></example>
        public string Message { get; set; }

        /// <summary>로그 작성 여부</summary>
        /// <example>true</example>
        public bool Log { get; set; }

        /// <summary>반환값</summary>
        /// <example></example>
        public T Response { get; set; }
    }


    /// <summary>
    /// 사용을 권장하지 않음(Result<T>사용!!)
    /// </summary>
    public class Result 
    {
        /// <summary>
        /// 사용을 권장하지 않음(Result<T>사용!!)
        /// </summary>
        /// <param name="code"></param>
        /// <param name="response"></param>
        /// <param name="log"></param>
        /// <param name="language"></param>
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
