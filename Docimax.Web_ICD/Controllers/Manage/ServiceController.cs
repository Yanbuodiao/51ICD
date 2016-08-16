using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docimax.Web_ICD.Controllers.Manage
{
    [Authorize]
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult ServiceApply()
        {
            return View();
        }
    }
}