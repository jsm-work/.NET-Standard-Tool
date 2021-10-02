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
            { ERROR_CODES.로그인_성공, new ErrorCodeMessage(ERROR_CODES.로그인_성공, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "Login successful" },
                { ERROR_LANGUAGE.kor, "로그인 성공" },
            })},            
            { ERROR_CODES.로그인_실패, new ErrorCodeMessage(ERROR_CODES.로그인_실패, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "Login failed" },
                { ERROR_LANGUAGE.kor, "로그인 실패" },
            })},
            { ERROR_CODES.계정_중복, new ErrorCodeMessage(ERROR_CODES.계정_중복, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "Already registered account" },
                { ERROR_LANGUAGE.kor, "이미 등록된 계정" },
            })},
            { ERROR_CODES.이메일_중복, new ErrorCodeMessage(ERROR_CODES.이메일_중복, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "Already registered email" },
                { ERROR_LANGUAGE.kor, "이미 등록된 이메일" },
            })},
            { ERROR_CODES.올바르지_않은_메일_형식, new ErrorCodeMessage(ERROR_CODES.올바르지_않은_메일_형식, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "Email Invalid Mail Format" },
                { ERROR_LANGUAGE.kor, "올바르지 않은 메일 형식" },
            })},


            
            #region 성공
            { ERROR_CODES.성공, new ErrorCodeMessage(ERROR_CODES.성공, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "successfull" },
                { ERROR_LANGUAGE.kor, "성공" },
            })},
            { ERROR_CODES.생성_성공, new ErrorCodeMessage(ERROR_CODES.생성_성공, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "registration successfull" },
                { ERROR_LANGUAGE.kor, "생성 성공" },
            })},
            { ERROR_CODES.읽기_성공, new ErrorCodeMessage(ERROR_CODES.읽기_성공, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "load successfull" },
                { ERROR_LANGUAGE.kor, "읽기 성공" },
            })},
            { ERROR_CODES.수정_성공, new ErrorCodeMessage(ERROR_CODES.수정_성공, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "modification successfull" },
                { ERROR_LANGUAGE.kor, "수정 성공" },
            })},
            { ERROR_CODES.삭제_성공, new ErrorCodeMessage(ERROR_CODES.삭제_성공, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "removal successfull" },
                { ERROR_LANGUAGE.kor, "삭제 성공" },
            })},
	        #endregion

            #region 실패
            { ERROR_CODES.실패, new ErrorCodeMessage(ERROR_CODES.실패, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "failure" },
                { ERROR_LANGUAGE.kor, "실패" },
            })},
            { ERROR_CODES.생성_실패, new ErrorCodeMessage(ERROR_CODES.생성_실패, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "Registration failure" },
                { ERROR_LANGUAGE.kor, "생성 실패" },
            })},
            { ERROR_CODES.읽기_실패, new ErrorCodeMessage(ERROR_CODES.읽기_실패, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "load failure" },
                { ERROR_LANGUAGE.kor, "읽기 실패" },
            })},
            { ERROR_CODES.수정_실패, new ErrorCodeMessage(ERROR_CODES.수정_실패, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "modification failure" },
                { ERROR_LANGUAGE.kor, "수정 실패" },
            })},
            { ERROR_CODES.삭제_실패, new ErrorCodeMessage(ERROR_CODES.삭제_실패, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "removal failure" },
                { ERROR_LANGUAGE.kor, "삭제 실패" },
            })},
	        #endregion

            { ERROR_CODES.존재하지_않는_앱키, new ErrorCodeMessage(ERROR_CODES.존재하지_않는_앱키, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "This app key does not exist" },
                { ERROR_LANGUAGE.kor, "존재하지 않는 앱키" },
            })},
            { ERROR_CODES.존재하지_않는_데이터, new ErrorCodeMessage(ERROR_CODES.존재하지_않는_데이터, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "This data does not exist" },
                { ERROR_LANGUAGE.kor, "존재하지 않는 데이터" },
            })},
            { ERROR_CODES.존재하지_않는_부모데이터, new ErrorCodeMessage(ERROR_CODES.존재하지_않는_부모데이터, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "Parent data does not exist" },
                { ERROR_LANGUAGE.kor, "존재하지 않는 부모데이터" },
            })},
            { ERROR_CODES.중복된_데이터, new ErrorCodeMessage(ERROR_CODES.중복된_데이터, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "This is redundant data" },
                { ERROR_LANGUAGE.kor, "중복된 데이터" },
            })},
            { ERROR_CODES.데이터_허용_길이_초과, new ErrorCodeMessage(ERROR_CODES.데이터_허용_길이_초과, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "Data allowed length exceeded" },
                { ERROR_LANGUAGE.kor, "데이터 허용 길이 초과" },
            })},
            { ERROR_CODES.데이터_변경_사항_없음, new ErrorCodeMessage(ERROR_CODES.데이터_변경_사항_없음, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "Data no change" },
                { ERROR_LANGUAGE.kor, "데이터 변경 사항 없음" },
            })},
            


            { ERROR_CODES.데이터베이스에_값_등록_실패, new ErrorCodeMessage(ERROR_CODES.데이터베이스에_값_등록_실패, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "Database registration failed" },
                { ERROR_LANGUAGE.kor, "Database 등록에 실패" },
            })},
            { ERROR_CODES.로그_등록_실패, new ErrorCodeMessage(ERROR_CODES.로그_등록_실패, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "Log registration failed" },
                { ERROR_LANGUAGE.kor, "로그 등록에 실패" },
            })},
            { ERROR_CODES.확인되지_않은_에러, new ErrorCodeMessage(ERROR_CODES.확인되지_않은_에러, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "An unconfirmed error has occurred" },
                { ERROR_LANGUAGE.kor, "확인되지 않은 에러" },
            })},
            { ERROR_CODES.권한_없음, new ErrorCodeMessage(ERROR_CODES.권한_없음, new Dictionary<ERROR_LANGUAGE, string>(){
                { ERROR_LANGUAGE.eng, "No authority" },
                { ERROR_LANGUAGE.kor, "권한 없음" },
            })},
        };

        public static string Message(ERROR_CODES code, ERROR_LANGUAGE language = ERROR_LANGUAGE.eng)
        {
            return ErrorCodes[code].Message(language);
        }
    }
}
