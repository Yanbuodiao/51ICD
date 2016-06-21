using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docimax.Web_ICD.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "51ICD平台描述页面.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "51ICD平台合同信息.";

            return View();
        }
        public ActionResult Detail()
        {
            return View();
        }
    }
}