using Docimax.Common;
using Docimax.Data_ICD.DAL.WebChat;
using Docimax.Interface_ICD.Interface.WebChat;
using Docimax.Interface_ICD.Model;
using Docimax.Web_ICD.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Docimax.Web_ICD.Controller_WebChat
{
    public class WebChatUserController : Controller
    {

        [AllowAnonymous]
        public ActionResult Get3rdSession(string userCode)
        {
            using (var webClient = new WebClient())
            {
                var webChatURL = string.Format("https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&js_code={2}", ConfigurationManager.AppSettings["WebChatAppID"], ConfigurationManager.AppSettings["WebChatAppSecret"], userCode);
                var responseStr = Encoding.UTF8.GetString(webClient.DownloadData(new Uri(webChatURL)));
                var webChatSession = JsonHelper.DeserializeObject<WebChatSession>(responseStr);
                var result = new ICDExcuteResult<string>();
                if (webChatSession != null && !string.IsNullOrEmpty(webChatSession.openid))
                {
                    webChatSession.InitializeSessionKey();
                    HttpRuntime.Cache.Insert(webChatSession.ICD_Session_Key, webChatSession, null, DateTime.UtcNow.AddSeconds(webChatSession.expires_in), System.Web.Caching.Cache.NoSlidingExpiration);
                    result.IsSuccess = true;
                    result.TResult = webChatSession.ICD_Session_Key;
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorStr = "Error";
                }
                var resultStr = JsonHelper.SerializeObject(result);
                return Content(resultStr);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult InitialUser(string icd_sessionKey, WebChatUserLoginInfo userLoginInfo)
        {
            var result = new ICDExcuteResult<string>();
            try
            {

                if (HttpRuntime.Cache[icd_sessionKey] != null && userLoginInfo != null)
                {
                    var webChatSession = HttpRuntime.Cache[icd_sessionKey] as WebChatSession;
                    if (webChatSession != null)
                    {
                        var toSignStr = string.Format("{0}{1}", userLoginInfo.rawData, webChatSession.session_key);
                        var hash = string.Join("", (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(toSignStr)).Select(b => b.ToString("x2")).ToArray());
                        if (hash == userLoginInfo.signature)//验签成功
                        {
                            var encryptKeyBytes = System.Convert.FromBase64String(webChatSession.session_key);
                            var encryptIVBytes = System.Convert.FromBase64String(userLoginInfo.iv);
                            var toDecryBytes = System.Convert.FromBase64String(userLoginInfo.encryptedData);

                            var encryptedStr = Encoding.UTF8.GetString(EncryptUtils.AesDecrypt(encryptKeyBytes, encryptIVBytes, toDecryBytes));
                            var webChatUserInfo = JsonHelper.DeserializeObject<WebChatUserInfo>(encryptedStr);
                            if (webChatUserInfo != null)
                            {
                                if (webChatSession.openid == webChatUserInfo.openId)
                                {
                                    IWebChatUser webChatUserAccess = new DAL_WebChatUser();
                                    webChatUserAccess.CreateOrUpdateUser(webChatUserInfo);
                                }
                                else
                                {
                                    //todo 记录错误日志 openID不一致
                                }
                            }
                            else
                            {
                                //todo 记录错误日志 解密或反序列化失败
                            }
                        }
                        else
                        {
                            //todo 记录错误日志 验签失败
                        }
                    }
                    else
                    {
                        //todo 记录错误日志 session 失效
                    }
                }
                else
                {
                    //todo 记录错误日志 sessionKey 无效
                }
                result.ErrorStr = "未获取到用户信息";
                var resultStr = JsonHelper.SerializeObject(result);
                return Content(resultStr);
            }
            catch (Exception ex)
            {
                //todo 记录异常
                result.ErrorStr = "未获取到用户信息";
                var resultStr = JsonHelper.SerializeObject(result);
                return Content(resultStr);
            }
        }
    }
}