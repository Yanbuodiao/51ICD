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
        public static string GetIPFromRequest(HttpRequestBase request)
        {
            var ip = string.Empty;
            if (!string.IsNullOrEmpty(request.ServerVariables["HTTP_VIA"]))
                ip = Convert.ToString(request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            if (string.IsNullOrEmpty(ip))
                ip = Convert.ToString(request.ServerVariables["REMOTE_ADDR"]);
            return ip;
        }

        /// <summary>
        /// 把流转化为string（默认编码格式为utf-8）
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns>按照gbk转化后的字串</returns>
        public static string GetStrByInputStream(Stream stream)
        {
            return GetStrByInputStream(stream, Encoding.UTF8);
        }

        /// <summary>
        /// 按照指定的编码格式把流转化为string
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="encode">编码格式</param>
        /// <returns>转化后的字串</returns>
        public static string GetStrByInputStream(Stream stream, Encoding encode)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, (int)stream.Length);
            string postStr = encode.GetString(bytes);
            return postStr;
        }
    }
}