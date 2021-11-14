using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperTool.Controllers
{
    [Route("external")]
    [ApiController]
    public class ExternalAPIController : ControllerBase
    {
        /// <summary>
        /// 환율 정보
        /// </summary>
        /// <returns></returns>
        // GET: 
        [EnableCors("CORS")]
        [HttpGet("exchange-rate")]
        [Produces("application/xml")]
        public ActionResult<API_Server.Result> GetExchangeRate(string searchdata = "")
        {
            //open API - https://www.koreaexim.go.kr/site/program/openapi/openApiView?menuid=001003002002001&apino=2&viewtype=C


            string authkey = "kfdmzVSOQKzGAJGeyYDWlBP5H8hsuCQW";
            string json = API.REST_API.Get("https://www.koreaexim.go.kr/site/program/financial/exchangeJSON?data=AP01&authkey="+authkey + (searchdata.Length == 0 ? "" : "&searchdate="+ searchdata));

            List<ExchangeRate> a = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ExchangeRate>>(json);


            return new API_Server.Result(API_Server.ERROR_CODES.성공, a);
        }
    }
    public class ExchangeRate
    {
        public string 통화_코드 { get { return Cur_unit; } set { Cur_unit = value; } }

        public string 가격 { get { return Bkpr; } set { Bkpr = value; } }

        public string 통화_단위 { get { return Cur_nm; } set { } }

        public string 통화_기호 { get { return 국가코드.Get통화단위(Cur_unit.Substring(0,3)).통화_단위_기호; } set { } }

        /// <summary>조회 결과(1 : 성공, 2 : DATA코드 오류, 3 : 인증코드 오류, 4 : 일일제한횟수 마감)</summary>
        public int Result { get; set; }

        /// <summary>통화코드</summary>
        public string Cur_unit { get; set; }

        /// <summary>전신환(송금) 받으실때</summary>
        public string Ttb { get; set; }

        /// <summary>전신환(송금) 보내실때</summary>
        public string Tts { get; set; }

        /// <summary>매매 기준율</summary>
        public string Deal_bas_r { get; set; }

        /// <summary>장부가격</summary>
        public string Bkpr { get; set; }

        /// <summary>년환가료율</summary>
        public string Yy_efee_r { get; set; }

        /// <summary>10일환가료율</summary>
        public string Ten_dd_efee_r { get; set; }

        /// <summary></summary>
        public string Kftc_bkpr { get; set; }

        /// <summary>서울외국환중개 매매기준율</summary>
        public string Kftc_deal_bas_r { get; set; }

        /// <summary>국가/통화명</summary>
        public string Cur_nm { get; set; }
    }

    public static class 국가코드
    {
        static List<통화단위> Items = new List<통화단위>() {
            new 통화단위("USD", "미국",           "$",    "달러"),
            new 통화단위("KRW", "한국",           "₩",    "원"),
            new 통화단위("JPY", "일본",           "¥",    "엔"),
            new 통화단위("EUR", "유럽",           "€",   "유로"),
            new 통화단위("GBP", "영국",           "£",    "파운드"),
            new 통화단위("HKD", "홍콩",           "HK$",  "홍콩 달러"),
            new 통화단위("CNH", "중국",           "¥",    "위안"),
            new 통화단위("CHF", "스위스",         "Fr",   "프랑"),
            new 통화단위("SEK", "스웨덴",         "Kr",   "크로나"),
            new 통화단위("SGD", "싱가포르",       "S$",   "싱가포르 달러"),
            new 통화단위("THB", "태국",           "฿",    "바트"),
            new 통화단위("AED", "아랍에미리트",   "AED",  "디르함"),
            new 통화단위("SAR", "사우디아라비아", "﷼",    "리얄"),
            new 통화단위("AUD", "호주",           "AU$",  "호주 달러"),
            new 통화단위("BHD", "바레인",         "BD",   "디나르"),
            new 통화단위("BND", "브루나이",       "BN$",  "브루나이 달러"),
            new 통화단위("CAD", "캐나다",         "CA$",  "캐나다 달러"),
            new 통화단위("NOK", "노르웨이",       "Kr",   "크로네"),
            new 통화단위("DKK", "덴마크",         "Kr",   "크로네"),
            new 통화단위("IDR", "인도네시아",     "Rp",   "루피아"),
            new 통화단위("KWD", "쿠웨이트",       "د.ك",  "디나르"),
            new 통화단위("MYR", "말레이시아",     "RM",   "링깃"),
            new 통화단위("NZD", "뉴질랜드",       "NZ$",  "뉴질랜드 달러")
    };

        public static 통화단위 Get통화단위(string code)
        {
            List<통화단위> result = new List<통화단위>(from item in Items where item.통화_코드 == code select item);
            if (result.Count == 0)
                return new 통화단위("","","","");
            else
                return result[0];
        }
    }

    public class 통화단위
    {
        public 통화단위(string _통화_코드, string _국가, string _화페_단위_기호, string _화페_단위_발음)
        {
            통화_코드 = _통화_코드;
            국가 = _국가;
            통화_단위_기호 = _화페_단위_기호;
            통화_단위_발음 = _화페_단위_발음;
        }

        public string 국가 { get; set; }
        public string 통화_코드 { get; set; }
        public string 통화_단위_기호 { get; set; }
        public string 통화_단위_발음 { get; set; }
    }
}