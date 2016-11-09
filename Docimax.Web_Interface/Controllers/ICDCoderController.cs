using Docimax.Common;
using Docimax.Common_ICD.Verify;
using Docimax.Interface_ICD.Model.UploadModel;
using Docimax.Interface_ICD.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Docimax.Common_ICD.Message;

namespace Docimax.Web_Interface.Controllers
{
    public class ICDCoderController : BaseController
    {
        [HttpPost]
        public ActionResult NewCodeOrder()
        {
            var requestModel = UploadModel<CodeModel_N041>.DicToModel(ViewBag.Decrypt as Dictionary<string, string>);
            var verifyResult = BaseVerify.RequiredValidate<CodeModel_N041>(requestModel);
            if (!verifyResult.IsSuccess)
            {
                return base.buildResponseAndLog(verifyResult.ErrorStr, (ViewBag.log as BaseLog));
            }
            //todo 基础验证
            //todo 保存相关记录
            //todo 返回同步通知
            return base.buildResponseAndLog(MessageStr.SyncStr, (ViewBag.log as BaseLog));
        }
    }
}