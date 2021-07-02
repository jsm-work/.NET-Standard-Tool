using System;
using System.Collections.Generic;
using System.Text;

namespace API_Server
{
    public class ErrorCodeMessage
    {
        private Dictionary<ERROR_LANGUAGE, string> dic_ErrorCode { get; set; }

        public string Message(ERROR_LANGUAGE language = ERROR_LANGUAGE.eng)
        {
            if (dic_ErrorCode.ContainsKey(language) == false)
                language = ERROR_LANGUAGE.eng;

            return dic_ErrorCode[language];
        }

        public ErrorCodeMessage(ERROR_CODES code, Dictionary<ERROR_LANGUAGE, string> Msg_LNG_TO_ERRORCODE)
        {
            if (Msg_LNG_TO_ERRORCODE.ContainsKey(ERROR_LANGUAGE.eng) == false)
                throw new Exception("ErrorCodeItem-기본 언어:eng 값이 없음.");

            dic_ErrorCode = Msg_LNG_TO_ERRORCODE;
        }
    }

    public static class ErrorCode
    {
        private static Dictionary<ERROR_CODES, ErrorCodeMessage> ErrorCodes = new Dictionary<ERROR_CODES, ErrorCodeMessage>()
        {
            { ERROR_CODES.성공, new ErrorCodeMessage(ERROR_CODES.성공, new Dictionary<ERROR_LANGUAGE, string>()
                                                    {
                                                        { ERROR_LANGUAGE.eng, "successfull" },
                                                        { ERROR_LANGUAGE.kor, "성공" },
                                                    })
            },
            { ERROR_CODES.생성_성공, new ErrorCodeMessage(ERROR_CODES.생성_성공, new Dictionary<ERROR_LANGUAGE, string>()
                                                    {
                                                        { ERROR_LANGUAGE.eng, "registration successfull" },
                                                        { ERROR_LANGUAGE.kor, "등록 성공" },
                                                    })
            },
            { ERROR_CODES.제거_성공, new ErrorCodeMessage(ERROR_CODES.제거_성공, new Dictionary<ERROR_LANGUAGE, string>()
                                                    {
                                                        { ERROR_LANGUAGE.eng, "removal successfull" },
                                                        { ERROR_LANGUAGE.kor, "제거 성공" },
                                                    })
            },
            { ERROR_CODES.생성_실패, new ErrorCodeMessage(ERROR_CODES.생성_실패, new Dictionary<ERROR_LANGUAGE, string>()
                                                    {
                                                        { ERROR_LANGUAGE.eng, "Registration failure" },
                                                        { ERROR_LANGUAGE.kor, "등록 실패" },
                                                    })
            },
            { ERROR_CODES.존재하지_않는_앱키, new ErrorCodeMessage(ERROR_CODES.존재하지_않는_앱키, new Dictionary<ERROR_LANGUAGE, string>()
                                                    {
                                                        { ERROR_LANGUAGE.eng, "This app key does not exist" },
                                                        { ERROR_LANGUAGE.kor, "존재하지 않는 앱키" },
                                                    })
            },
            { ERROR_CODES.존재하지_않는_데이터, new ErrorCodeMessage(ERROR_CODES.존재하지_않는_데이터, new Dictionary<ERROR_LANGUAGE, string>()
                                                    {
                                                        { ERROR_LANGUAGE.eng, "This data does not exist" },
                                                        { ERROR_LANGUAGE.kor, "존재하지 않는 데이터" },
                                                    })
            },
            { ERROR_CODES.존재하지_않는_부모데이터, new ErrorCodeMessage(ERROR_CODES.존재하지_않는_부모데이터, new Dictionary<ERROR_LANGUAGE, string>()
                                                    {
                                                        { ERROR_LANGUAGE.eng, "Parent data does not exist" },
                                                        { ERROR_LANGUAGE.kor, "존재하지 않는 부모데이터" },
                                                    })
            },
            { ERROR_CODES.중복된_데이터, new ErrorCodeMessage(ERROR_CODES.중복된_데이터, new Dictionary<ERROR_LANGUAGE, string>()
                                                    {
                                                        { ERROR_LANGUAGE.eng, "This is redundant data" },
                                                        { ERROR_LANGUAGE.kor, "중복된 데이터" },
                                                    })
            },
            { ERROR_CODES.데이터베이스에_값_등록_실패, new ErrorCodeMessage(ERROR_CODES.데이터베이스에_값_등록_실패, new Dictionary<ERROR_LANGUAGE, string>()
                                                    {
                                                        { ERROR_LANGUAGE.eng, "Database registration failed" },
                                                        { ERROR_LANGUAGE.kor, "Database 등록에 실패" },
                                                    })
            },
            { ERROR_CODES.로그_등록_실패, new ErrorCodeMessage(ERROR_CODES.로그_등록_실패, new Dictionary<ERROR_LANGUAGE, string>()
                                                    {
                                                        { ERROR_LANGUAGE.eng, "Log registration failed" },
                                                        { ERROR_LANGUAGE.kor, "로그 등록에 실패" },
                                                    })
            },
            { ERROR_CODES.확인되지_않은_에러, new ErrorCodeMessage(ERROR_CODES.확인되지_않은_에러, new Dictionary<ERROR_LANGUAGE, string>()
                                                    {
                                                        { ERROR_LANGUAGE.eng, "An unconfirmed error has occurred" },
                                                        { ERROR_LANGUAGE.kor, "확인되지 않은 에러" },
                                                    })
            },
        };

        public static string Message(ERROR_CODES code, ERROR_LANGUAGE language = ERROR_LANGUAGE.eng)
        {
            return ErrorCodes[code].Message(language);
        }
    }
}
