using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace Docimax.Web_ICD.Controllers
{
    public class ICDController : Controller
    {
        public ActionResult UpdateICDSummary(ICDModel model)
        {
            var userID = User.Identity.GetUserId();
            IICDRepository access = new DAL_ICDRepository();
            var icdExcuteResult = access.UpdateICDSummary(model,userID);
            return Json(icdExcuteResult);
        }
    }
}