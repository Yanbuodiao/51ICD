using Docimax.Common;
using Docimax.Common.Encryption;
using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Message;
using Docimax.Interface_ICD.Model.Log;
using Docimax.Interface_ICD.Model.UploadModel;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Docimax.Web_Interface.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var log = initialLog(filterContext);
            var postStr = HttpHelper.GetStrByInputStream(filterContext.RequestContext.HttpContext.Request.InputStream);
            var uploadRequest = JsonHelper.DeserializeObject<UploadRequest>(postStr);

            #region 基础验证

            if (uploadRequest == null)
            {
                filterContext.Result = buildResponseAndLog(MessageStr.JsonDeserializeError, log);
                return;
            }
            if (string.IsNullOrWhiteSpace(uploadRequest.Version))
            {
                filterContext.Result = buildResponseAndLog(MessageStr.VersionNull, log);
                return;
            }
            if (uploadRequest.RequestTime < DateTime.Now.AddSeconds(-120))//超过120秒的请求不再处理
            {
                filterContext.Result = buildResponseAndLog(MessageStr.JsonDeserializeError, log);
                return;
            }
            IService serviceAccess = new DAL_Service();
            var orgModel = serviceAccess.GetOrgModelByAUTHCode(uploadRequest.AUTHCode);
            if (orgModel == null)
            {
                filterContext.Result = buildResponseAndLog(MessageStr.OrganizationNull, log);
                return;
            }
            if (orgModel.Certificate != CertificateState.认证通过)
            {
                filterContext.Result = buildResponseAndLog(MessageStr.OrganizationFail, log);
                return;
            }
            var checkSign = ICD_EncryptUtils.RSACheckSign(uploadRequest.ToSignDictionary(), uploadRequest.Sign, Server.MapPath(orgModel.CheckSignPubKeyPath));
            if (!checkSign)
            {
                filterContext.Result = buildResponseAndLog(MessageStr.CheckSignFail, log);
                return;
            }

            #endregion

            var requestContentStr = ICD_EncryptUtils.DecryptByAES(uploadRequest.EncryptedRequest, orgModel.EncryptKeyName, "utf-8");
            log.RequestTime = uploadRequest.RequestTime;
            ViewBag.Decrypt = requestContentStr.Split('&').ToDictionary(a => a.Split('=')[0], b => b.Split('=')[1]);
            ViewBag.AUTHCode = uploadRequest.AUTHCode;
            ViewBag.Log = log;
            base.OnActionExecuting(filterContext);
        }

        private BaseLog initialLog(ActionExecutingContext filterContext)
        {
            return new BaseLog
            {
                InterfaceName = (filterContext.ActionDescriptor).ActionName,
                GetRequestTime = DateTime.Now,
                RequestIP = HttpHelper.GetIPFromRequest(filterContext.HttpContext.Request),
                UserAgent = HttpHelper.GetUserAgent(filterContext.HttpContext.Request),
            };
        }

        internal ICDJsonResult buildResponseAndLog(string responseStr, BaseLog log)
        {
            var strArry = responseStr.Split(':');
            var response = new SyncResponse<string>
            {
                Code = log.ResponseCode = strArry[0],
                Msg = log.ResponseStr = strArry[1],
            };
            var responseJson = JsonHelper.SerializeObject(response);
            log.LogContent = responseJson;
            log.ResponseTime = DateTime.Now;
            saveLog(log);
            return new ICDJsonResult(responseJson);
        }

        private void saveLog(BaseLog log)
        {
            Task.Run(() =>
            {
                //todo 具体的日志保存记录
            });
        }
    }
}