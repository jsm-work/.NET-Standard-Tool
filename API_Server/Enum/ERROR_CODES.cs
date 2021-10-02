using System;
using System.Collections.Generic;
using System.Text;

namespace API_Server
{
    public enum ERROR_CODES
    {
        #region 로그인 성공 / 실패
        로그인_성공 = 100,
        로그인_실패 = -100,
        #endregion

        계정_중복 = -101,
        이메일_중복 = -102,
        올바르지_않은_메일_형식 = -103,

        #region C.R.U.D 성공/실패
        성공 = 200,
        생성_성공 = 201,
        읽기_성공 = 202,
        수정_성공 = 203,
        삭제_성공 = 204,

        실패 = -200,
        생성_실패 = -201,
        읽기_실패 = -202,
        수정_실패 = -203,
        삭제_실패 = -204,
        #endregion

        #region 인증
        존재하지_않는_앱키 = -300,      
        권한_없음 = -500,
        #endregion


        #region 데이터
        중복된_데이터 = -301,
        존재하지_않는_데이터 = -302,
        존재하지_않는_부모데이터 = -303,
        데이터_허용_길이_초과 = -304,
        데이터_변경_사항_없음 = -305,
        #endregion


        /// <summary>
        /// 확인되지 않은 에러
        /// </summary>
        확인되지_않은_에러 = -400,

        /// <summary>
        /// Database 등록에 실패했습니다.
        /// Database registration failed.
        /// </summary>
        데이터베이스에_값_등록_실패 = -401,

        /// <summary>
        /// 로그 등록 실패
        /// </summary>
        로그_등록_실패 = -402,




    }
}
