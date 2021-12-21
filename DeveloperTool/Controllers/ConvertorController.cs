using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Convertors;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DeveloperTool.Controllers
{

    [Route("convertor")]
    [ApiController]
    public class ConvertorController : ControllerBase
    {
        #region String ↔ HexString
        /// <summary>
        /// 문자를 16진수 문자로 변환
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        // GET: Convertor/string-to-hexstring
        [EnableCors("CORS")]
        [HttpGet("string-to-hexstring")]
        public string StringToHexString([Required] string input)
        {
            return String_Convertor.StringToHexString(input);
        }

        /// <summary>
        /// 16진수 문자를 문자로 변환
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        // GET: Convertor/hexstring-to-string
        [EnableCors("CORS")]
        [HttpGet("hexstring-to-string")]
        public string HexStringToString([Required] string input)
        {
            return String_Convertor.HexStringToString(input);
        }
        #endregion

        #region CreateSQL → C#Class
        /// <summary>
        /// 테이블 생성 쿼리를 붙여 넣어 C# 클래스 자동 생성 (한 줄로 입력 필수 - 주소록에 붙였다가 붙여 넣기)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [EnableCors("CORS")]
        [HttpPost("createsql-to-csclass")]
        public ActionResult<string> CreateSQLToClass(CreateSQL_INPUT data)
        {
            string result = string.Empty;


            #region 테이블 정보
            string tableName = string.Empty;
            try
            {
                tableName = data.CreateSQL.Split("(")[0].Split("`")[1];
            }
            catch (System.IndexOutOfRangeException)
            {
                return "false";
            }

            string tableComment = string.Empty;
            try
            {
                tableComment = data.CreateSQL.Replace("COMMENT =", "COMMENT=").Split("COMMENT=")[1].Split("'")[1].Split("'")[0];
            }

            catch (System.IndexOutOfRangeException)
            {
            }
            #endregion

            #region 컬럼 정보
            string[] attributes = data.CreateSQL.Replace(data.CreateSQL.Split("(")[0], "").Replace("(", "").Split(",");
            List<CreateSQL_param> items = new List<CreateSQL_param>();
            foreach(string str in attributes )
            {
                if (str.Split("`").Length < 4)
                {
                    string paramName = str.Split("`")[1];
                    string paramType = str.Split("`")[2].Split(")")[0];
                    string paramComment = string.Empty;
                    try
                    {
                        string comment = str.Replace("COMMENT '", "COMMENT'");
                        if (comment.Contains("COMMENT'"))
                            paramComment = comment.Split("COMMENT'")[1].Split("'")[0];
                        else
                        { 
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        
                    }

                    // 문자열
                    if (
                        paramType.Contains("char") || paramType.Contains("CHAR") ||
                        paramType.Contains("varchar") || paramType.Contains("VARCHAR") ||
                        paramType.Contains("longtext") || paramType.Contains("LONGTEXT") ||
                        paramType.Contains("text") || paramType.Contains("TEXT") ||

                        paramType.Contains("json") || paramType.Contains("JSON"))
                        paramType = "string";

                    // bool
                    else if (paramType.Contains("tinyint") || paramType.Contains("TINYINT"))
                        paramType = "bool";

                    // int
                    else if (paramType.Contains("int") || paramType.Contains("INT"))
                        paramType = "int";

                    // float
                    else if (paramType.Contains("float") || paramType.Contains("FLOAT"))
                        paramType = "float";

                    // double
                    else if (paramType.Contains("double") || paramType.Contains("DOUBLE"))
                        paramType = "double";

                    // date time
                    else if (paramType.Contains("datetime") || paramType.Contains("DATETIME") ||
                             paramType.Contains("date") || paramType.Contains("DATE") ||
                             paramType.Contains("timestamp") || paramType.Contains("TIMESTAMP"))
                        paramType = (data.IsDateToString == false ? "DateTime" :"string");


                    if(new List<CreateSQL_param>(from item in items where item.Name == paramName select item).Count == 0)
                        items.Add( new CreateSQL_param() { Name = paramName, Type = paramType, Comment = paramComment });
                }
            }
            #endregion

            #region 결과물 생성

            // 클래스 설명
            if (tableComment != string.Empty)
                result += "\t///<summary>" + tableComment + "</summary>\r\n";

            // 클래스 이름
            result += "\tpublic class " + tableName + "\r\n";
            result += "\t{\r\n";

            // 변수
            foreach (var item in items)
            {
                // 변수 설명
                result += "\t\t///<summary>" + item.Comment + "</summary>\r\n";
                result += "\t\t///<example></example>\r\n";

                // 변수
                result += "\t\tpublic " + item.Type + " " + item.Name + (data.IsProperty == true ? " {get;set;}" :";") +"\r\n\r\n";
            }

            // ToString(Json)
            #region ToString
            if(data.IsToString)
                result += "\t\tpublic override string ToString() => System.Text.Json.JsonSerializer.Serialize<" + tableName + ">(this);\r\n";
            #endregion
            #endregion

            result += "\t}";

            return result;
        }
        #endregion

    }
    public class CreateSQL_INPUT
    {
        /// <summary>클래스를 Json형태의 데이터로 반환하는 ToString 함수를 재정의 합니다.</summary>
        /// <example>true</example>
        public bool IsToString { get; set; } = true;

        /// <summary>변수를 {get;set;} 형태로 출력합니다.</summary>
        /// <example>false</example>
        public bool IsProperty { get; set; } = false;

        /// <summary>날짜 포멧을 문자열로 출력합니다.</summary>
        /// <example>true</example>
        public bool IsDateToString { get; set; } = false;

        /// <summary>Table 생성 SQL을 개행 없이 입력.</summary>
        /// <example></example>
        public string CreateSQL { get; set; }
    }
    public class CreateSQL_param
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Comment { get; set; }
    }
}