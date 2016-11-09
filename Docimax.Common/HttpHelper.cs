using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Docimax.Common
{
    public class HttpHelper
    {
        /// <summary>
        /// 从请求内容获取请求发起方的IP
        /// </summary>
        /// <param name="request">请求体</param>
        /// <returns>IP地址</returns>
        public static string GetIPFromRequest(HttpRequestBase request)
        {
            var ip = string.Empty;
            if (!string.IsNullOrEmpty(request.ServerVariables["HTTP_VIA"]))
                ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
                ip = request.ServerVariables["REMOTE_ADDR"];
            return ip;
        }

        /// <summary>
        /// 根据请求体获取请求发起的平台
        /// </summary>
        /// <param name="request">请求提</param>
        /// <returns>平台</returns>
        public static string GetUserAgent(HttpRequestBase request)
        {
            var userAgent = string.Empty;
            if (request.Browser.Capabilities[""] != null)
            {
                return request.Browser.Capabilities[""].ToString();
            }
            else
                return request.Browser.Browser;
        }


        /// <summary>
        /// 按照指定的编码格式把流转化为string
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="charset">编码格式</param>
        /// <returns>转化后的字串</returns>
        public static string GetStrByInputStream(Stream stream, string charset = "utf-8")
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, (int)stream.Length);
            string postStr = Encoding.GetEncoding(charset).GetString(bytes);
            return postStr;
        }
    }
}