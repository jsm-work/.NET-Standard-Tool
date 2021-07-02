using System;
using System.Collections.Generic;
using System.Text;

namespace API_Server
{
    public enum ERROR_CODES
    {
        /// <summary>
        /// 성공
        /// successfull
        /// </summary>
        성공 = 200,

        /// <summary>
        /// 성공 - 생성
        /// </summary>
        생성_성공 = 201,

        /// <summary>
        /// 성공 - 제거
        /// </summary>
        제거_성공 = 202,

        /// <summary>
        /// 존재하지 않는 앱키
        /// This app key does not exist.
        /// </summary>
        존재하지_않는_앱키 = -300,

        /// <summary>
        /// 실패 - 생성
        /// </summary>
        생성_실패 = -201,

        /// <summary>
        /// 중복된 데이터
        /// </summary>
        중복된_데이터 = -301,

        /// <summary>
        /// 존재하지 않는 데이터
        /// </summary>
        존재하지_않는_데이터 = -302,

        /// <summary>
        /// 존재하지 않는 부모 데이터
        /// </summary>
        존재하지_않는_부모데이터 = -303,


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
