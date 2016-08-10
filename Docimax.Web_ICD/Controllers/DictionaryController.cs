using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docimax.Web_ICD.Controllers
{
    public class DictionaryController : Controller
    {
        // GET: Dictionary
        public ActionResult ICD_Diagnosis_Typeahead(string queryStr = null)
        {
            IICDRepository icdRepositoryAccess = new DAL_ICDRepository();
            return Json(icdRepositoryAccess.GetICD_Diagnosis_ModelList(queryStr, 1), JsonRequestBehavior.AllowGet);
        }
    }
}