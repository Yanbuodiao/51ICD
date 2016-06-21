using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Docimax.Common
{
    public class HttpHelper
    {
        public static string GetIPFromRequest(HttpRequest request)
        {
            var ip = string.Empty;
            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            if (string.IsNullOrEmpty(ip))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            return ip;
        }
    }
}