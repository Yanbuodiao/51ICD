using Docimax.Common;
using Docimax.Common.Encryption;
using Docimax.Common_ICD.Message;
using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model.UploadModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Docimax.Web_Interface.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var postStr = HttpHelper.GetStrByInputStream(filterContext.RequestContext.HttpContext.Request.InputStream);//通过输入流获得字符串   
            var uploadRequest = JsonHelper.DeserializeObject<UploadRequest>(postStr);
            if (uploadRequest == null)
            {
                //todo  记录日志 
                var errorStr = MessageStr.JsonDeserializeError.Split(':');
                var response = new SyncResponse<string>
                {
                    Code = errorStr[0],
                    Msg = errorStr[1],
                };
                filterContext.Result = new ICDJsonResult(JsonHelper.SerializeObject(response)); ;
                return;
            }
            IService serviceAccess = new DAL_Service();
            var orgModel = serviceAccess.GetOrgModelByCode(uploadRequest.ORGCode);
            if (orgModel == null)
            {
                //todo  记录日志  
                var errorStr = MessageStr.OrganizationNull.Split(':');
                var response = new SyncResponse<string>
                {
                    Code = errorStr[0],
                    Msg = errorStr[1],
                };
                filterContext.Result = new ICDJsonResult(JsonHelper.SerializeObject(response)); ;
                return;
            }
            var checkSign = ICD_EncryptUtils.RSACheckSign(uploadRequest.ToSignDictionary(), uploadRequest.Sign, Server.MapPath(orgModel.CheckSignPubKeyPath));
            if (!checkSign)
            {
                //todo  记录日志  
                var errorStr = MessageStr.CheckSignFail.Split(':');
                var response = new SyncResponse<string>
                {
                    Code = errorStr[0],
                    Msg = errorStr[1],
                };
                filterContext.Result = new ICDJsonResult(JsonHelper.SerializeObject(response)); ;
                return;
            }
            var requestContentStr = ICD_EncryptUtils.DecryptByAES(uploadRequest.EncryptedRequest, orgModel.EncryptKeyName, "utf-8");
            var requestDic = requestContentStr.Split('&').ToDictionary(a => a.Split('=')[0], b => b.Split('=')[1]);
            ViewBag.Decrypt = requestDic;
            ViewBag.ORGCode = uploadRequest.ORGCode;
            base.OnActionExecuting(filterContext);
        }
    }
}