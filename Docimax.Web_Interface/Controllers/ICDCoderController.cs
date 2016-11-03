using Docimax.Interface_ICD.Model.UploadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docimax.Web_Interface.Controllers
{
    public class ICDCoderController : BaseController
    {
        // GET: ICDCoder
        public ActionResult NewCodeOrder()
        {
            var requestModel = UploadModel<CodeModel_N041>.DicToModel(ViewBag.Decrypt as Dictionary<string, string>);
            requestModel.RecieveTime = DateTime.Now;

            return Content("sadfas");
        }
    }
}