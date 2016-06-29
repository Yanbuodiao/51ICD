using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Docimax.Common
{
    public class HttpHelper
    {
        public static string GetIPFromRequest(HttpRequestBase request)
        {
            var ip = string.Empty;
            if (!string.IsNullOrEmpty(request.ServerVariables["HTTP_VIA"]))
                ip = Convert.ToString(request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            if (string.IsNullOrEmpty(ip))
                ip = Convert.ToString(request.ServerVariables["REMOTE_ADDR"]);
            return ip;
        }
    }
}