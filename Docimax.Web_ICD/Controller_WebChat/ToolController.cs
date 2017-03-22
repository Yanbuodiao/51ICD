using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using System.Collections.Generic;
using Docimax.Common;
using System.Linq;
using System.Web.Mvc;
using Docimax.Interface_ICD.Interface.WebChat;
using Docimax.Data_ICD.DAL.WebChat;
using System.Web;
using Docimax.Web_ICD.Models;

namespace Docimax.Web_ICD.Controller_WebChat
{
    public class ToolController : Controller
    {
        [AllowAnonymous]
        public ActionResult GetICDVersion(ICDTypeEnum icdType, string sessionkey = null)
        {
            IService access = new DAL_Service();
            var items = (access.GetICDVersion(icdType).Select(t => new ICDVersionModel { ICD_VersionID = t.Key, ICD_VersionName = t.Value })).ToList();

            #region 设置默认版本

            if (!string.IsNullOrWhiteSpace(sessionkey))
            {
                if (HttpRuntime.Cache[sessionkey] != null)
                {
                    var webChatSession = HttpRuntime.Cache[sessionkey] as WebChatSession;
                    IWebChatUser accessWebChat = new DAL_WebChatUser();
                    var defaultICDVersion = accessWebChat.GetDefaultICDVersion(webChatSession.openid, icdType);
                    if (defaultICDVersion > 0)
                    {
                        items.ForEach(c =>
                        {
                            if (c.ICD_VersionID == defaultICDVersion)
                            {
                                c.IsDefault = true;
                            }
                        });
                    }
                }
            }

            #endregion

            var itemsStr = JsonHelper.SerializeObject(items);
            return Content(itemsStr);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult SetDefaultICDVersion(ICDTypeEnum icdType, string sessionkey, int icdVersionID)
        {
            if (!string.IsNullOrWhiteSpace(sessionkey))
            {
                if (HttpRuntime.Cache[sessionkey] != null)
                {
                    var webChatSession = HttpRuntime.Cache[sessionkey] as WebChatSession;
                    IWebChatUser accessWebChat = new DAL_WebChatUser();
                    accessWebChat.SetDefaultICDVersion(icdType, webChatSession.openid, icdVersionID);
                }
            }
            return Content("");
        }
    }
}