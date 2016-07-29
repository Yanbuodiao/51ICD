using Docimax.Interface_ICD.Enum;
using System;
using System.Web.Mvc;

namespace Docimax.Web_ICD.Controllers
{
    [Authorize]
    public class ServiceItemController : Controller
    {
        // GET: ServiceItem
        public ActionResult Index(string TextFilter = null, int page = 1, int pageSize = 10, DateTime? BeginDate = null, DateTime? EndDate = null, ICDOrderState? OrderState = null)
        {
            return View();
        }
    }
}