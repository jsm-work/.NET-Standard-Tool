using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperTool.Controllers
{
    [Route("md5")]
    [ApiController]
    public class MD5Controller : ControllerBase
    {
        /// <summary>
        /// md5 변환
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        // GET: api/md5
        [EnableCors("CORS")]
        [HttpGet()]
        public ActionResult<string> GetMD5([Required]string input)
        {
            return Security.MD5.Encrypt(input);
        }
    }
}
