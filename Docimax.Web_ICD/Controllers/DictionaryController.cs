using Docimax.Common_ICD;
using System.Web.Mvc;

namespace Docimax.Web_ICD.Controllers
{
    public class DictionaryController : Controller
    {
         [AllowAnonymous]
        public ActionResult ICD_Diagnosis_Typeahead(string queryStr = null,int icdVersionID=1)
        {
            return Json(ICDVersionList.GetICDList(icdVersionID, queryStr), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ICD_Operate_Typeahead(string queryStr = null, int icdVersionID = 2)
        {
            return Json(ICDVersionList.GetICDList(icdVersionID, queryStr), JsonRequestBehavior.AllowGet);
        }
    }
}