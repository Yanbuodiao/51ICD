using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docimax.Web_ICD.Controllers
{
    public class ICDController : Controller
    {
        // GET: ICD
        public ActionResult UpdateICDSummary(ICDModel model)
        {
            IICDRepository access = new DAL_ICDRepository();
            var icdExcuteResult = access.UpdateICDSummary(model);
            return Json(icdExcuteResult);
        }
    }
}