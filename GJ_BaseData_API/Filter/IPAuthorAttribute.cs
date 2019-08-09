using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using log4net;

namespace GJ_BaseData_API.Filter
{
    public class IPAuthorAttribute : AuthorizeAttribute
    {
        private ILog log = LogManager.GetLogger($"IPAuthor过滤器");
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            string userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(userHostAddress))
            {
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                    userHostAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(',')[0].Trim();
            }
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.UserHostAddress;
            }

            //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
            {

                //if (userHostAddress == "117.34.118.23"|| userHostAddress== "117.34.118.31"|| userHostAddress.Contains("218.201.129.")||userHostAddress.Contains("61.150."))
                if (userHostAddress.Contains("218.201.129.") )
                {
                    return true;
                }
            }
            log.Info($" IP被拦截，IP不正确{userHostAddress}");
            return false;
        }

        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

    }

}