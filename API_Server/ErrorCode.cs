using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace API_Server
{
    public static class Status_Codes
    {
        public const int _100_계속 = StatusCodes.Status100Continue;
        public const int _101_프로토콜_전환 = StatusCodes.Status101SwitchingProtocols;
        public const int _102_처리중 = StatusCodes.Status102Processing;

        public const int _200_확인 = StatusCodes.Status200OK;
        public const int _201_생성_완료 = StatusCodes.Status201Created;
        public const int _202_수락_완료 = StatusCodes.Status202Accepted;
        public const int _203_권한_없음 = StatusCodes.Status203NonAuthoritative;
        public const int _204_콘텐츠_없음 = StatusCodes.Status204NoContent;
        public const int _205_콘텐츠_재설정 = StatusCodes.Status205ResetContent;
        public const int _206_부분_내용 = StatusCodes.Status206PartialContent;
        public const int _207_다중_상태 = StatusCodes.Status207MultiStatus;
        public const int _208_이미_보고됨 = StatusCodes.Status208AlreadyReported;
        public const int _226_이미_사용됨 = StatusCodes.Status226IMUsed;

        public const int _300_다중_선택 = StatusCodes.Status300MultipleChoices;
        public const int _301_MovedPermanently = StatusCodes.Status301MovedPermanently;
        public const int _302_발견 = StatusCodes.Status302Found;
        public const int _303_SeeOther = StatusCodes.Status303SeeOther;
        public const int _304_수정되지_않음 = StatusCodes.Status304NotModified;
        public const int _305_프록시_사용 = StatusCodes.Status305UseProxy;
        public const int _306_프록시_전환 = StatusCodes.Status306SwitchProxy;
        public const int _307_TemporaryRedirect = StatusCodes.Status307TemporaryRedirect;
        public const int _308_PermanentRedirect = StatusCodes.Status308PermanentRedirect;

        public const int _400_잘못된_요청 = StatusCodes.Status400BadRequest;
        public const int _401_Unauthorized = StatusCodes.Status401Unauthorized;
        public const int _402_PaymentRequired = StatusCodes.Status402PaymentRequired;
        public const int _403_Forbidden = StatusCodes.Status403Forbidden;
        public const int _404_찾을_수_없음 = StatusCodes.Status404NotFound;
        public const int _405_허용되지_않은_방식 = StatusCodes.Status405MethodNotAllowed;
        public const int _406_NotAcceptable = StatusCodes.Status406NotAcceptable;
        public const int _407_ProxyAuthenticationRequired = StatusCodes.Status407ProxyAuthenticationRequired;
        public const int _408_RequestTimeout = StatusCodes.Status408RequestTimeout;
        public const int _409_Conflict = StatusCodes.Status409Conflict;
        public const int _410_Gone = StatusCodes.Status410Gone;
        public const int _411_데이터_길이_부족 = StatusCodes.Status411LengthRequired;
        public const int _412_전제조건_실패 = StatusCodes.Status412PreconditionFailed;
        public const int _413_요청_개체가_너무_큼 = StatusCodes.Status413RequestEntityTooLarge;
        public const int _413_입력_개체가_너무_큼 = StatusCodes.Status413PayloadTooLarge;
        public const int _414_요청_URI가_너무_김 = StatusCodes.Status414RequestUriTooLong;
        public const int _414_URI가_너무_김 = StatusCodes.Status414UriTooLong;
        public const int _415_지원되지_않는_미디어_유형 = StatusCodes.Status415UnsupportedMediaType;
        public const int _416_요청된_범위_미충족 = StatusCodes.Status416RequestedRangeNotSatisfiable;
        public const int _416_RangeNotSatisfiable = StatusCodes.Status416RangeNotSatisfiable;
        public const int _417_ExpectationFailed = StatusCodes.Status417ExpectationFailed;
        public const int _418_ImATeapot = StatusCodes.Status418ImATeapot;
        public const int _419_AuthenticationTimeout = StatusCodes.Status419AuthenticationTimeout;
        public const int _421_MisdirectedRequest = StatusCodes.Status421MisdirectedRequest;
        public const int _422_UnprocessableEntity = StatusCodes.Status422UnprocessableEntity;
        public const int _423_Locked = StatusCodes.Status423Locked;
        public const int _424_FailedDependency = StatusCodes.Status424FailedDependency;
        public const int _426_UpgradeRequired = StatusCodes.Status426UpgradeRequired;
        public const int _428_PreconditionRequired = StatusCodes.Status428PreconditionRequired;
        public const int _429_TooManyRequests = StatusCodes.Status429TooManyRequests;
        public const int _431_RequestHeaderFieldsTooLarge = StatusCodes.Status431RequestHeaderFieldsTooLarge;
        public const int _451_UnavailableForLegalReasons = StatusCodes.Status451UnavailableForLegalReasons;

        public const int _500_InternalServerError = StatusCodes.Status500InternalServerError;
        public const int _501_NotImplemented = StatusCodes.Status501NotImplemented;
        public const int _502_잘못된_게이트웨이 = StatusCodes.Status502BadGateway;
        public const int _503_ServiceUnavailable = StatusCodes.Status503ServiceUnavailable;
        public const int _504_GatewayTimeout = StatusCodes.Status504GatewayTimeout;
        public const int _505_HttpVersionNotsupported = StatusCodes.Status505HttpVersionNotsupported;
        public const int _506_VariantAlsoNegotiates = StatusCodes.Status506VariantAlsoNegotiates;
        public const int _507_InsufficientStorage = StatusCodes.Status507InsufficientStorage;
        public const int _508_루프_감지 = StatusCodes.Status508LoopDetected;
        public const int _510_NotExtended = StatusCodes.Status510NotExtended;
        public const int _511_NetworkAuthenticationRequired = StatusCodes.Status511NetworkAuthenticationRequired;

    }

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
