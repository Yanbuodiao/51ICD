using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using System.Collections.Generic;
using Docimax.Common;
using System.Linq;
using System.Web.Mvc;

namespace Docimax.Web_ICD.Controller_WebChat
{
    public class ToolController : Controller
    {
        [AllowAnonymous]
        public ActionResult GetICDVersion(ICDTypeEnum icdType)
        {
            IService access = new DAL_Service();
            var items = access.GetICDVersion(icdType).Select(t => new ICDVersionModel { ICD_VersionID = t.Key, ICD_VersionName = t.Value });
            var itemsStr = JsonHelper.SerializeObject(items);
            return Content(itemsStr);
        }
    }
}