using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperTool.Controllers
{
    [Route("aes128")]
    [ApiController]
    public class AES128Controller : ControllerBase
    {
        /// <summary>
        /// 암호화
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key_hexstring">최소 32자 / 최대 32자</param>
        /// <param name="iv_hexstring">최소 32자 / 최대 32자</param>
        /// <returns></returns>
        // GET: aes128/encrypt
        [EnableCors("CORS")]
        [HttpGet("encrypt")]
        public string GetEncrypt(   [Required]string data, 
                                    [DefaultValue("00060002000000000000000000000000")][Required][MinLength(32)][MaxLength(32)]string key_hexstring, 
                                    [DefaultValue("00000000000000000000000000000000")][Required][MinLength(32)][MaxLength(32)]string iv_hexstring)
        {
            return Security.AES128.Encrypt_String(data, key_hexstring, iv_hexstring);
        }

        /// <summary>
        /// 복호화
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key_hexstring"></param>
        /// <param name="iv_hexstring"></param>
        /// <returns></returns>
        // GET: aes128/decrypt
        [EnableCors("CORS")]
        [HttpGet("decrypt")]
        public string GetDecrypt([Required]string data, [DefaultValue("00060002000000000000000000000000")][Required]string key_hexstring, [DefaultValue("00000000000000000000000000000000")][Required]string iv_hexstring)
        {
            return Security.AES128.Decrypt_String(data, key_hexstring, iv_hexstring);
        }



    }
}
