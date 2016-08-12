using Docimax.Common_ICD;
using System.Web.Mvc;

namespace Docimax.Web_ICD.Controllers
{
    public class DictionaryController : Controller
    {
        public ActionResult ICD_Diagnosis_Typeahead(string queryStr = null)
        {
            return Json(ICDVersionList.GetICDList(1, queryStr), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ICD_Operate_Typeahead(string queryStr = null)
        {
            return Json(ICDVersionList.GetICDList(2, queryStr), JsonRequestBehavior.AllowGet);
        }
    }
}