using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
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
            IService access = new DAL_Service();
            var services = access.GetAllSerevice();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ServiceApply(UserServiceApplyModel model)
        {
            return View(model);
        }
    }
}