using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperTool.Controllers
{
    [Route("IP")]
    [ApiController]
    public class IPController : ControllerBase
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="_acc"></param>
        public IPController(Microsoft.AspNetCore.Http.IHttpContextAccessor _acc)
        {
            HttpContextAccessor = _acc;
        }

        #region IP
        /// <summary>
        /// 접속자 IP 주소
        /// </summary>
        public string IP { get { return HttpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(); } }
        private Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContextAccessor;
        #endregion

        [HttpGet]
        public string GetIP()
        {
            return IP;
        }
    }
}
