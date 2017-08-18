using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docimax.Web_Interface.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index()
        {
            return Content("N:" + Guid.NewGuid().ToString("N") + "<br/>" + "Default:" + Guid.NewGuid().ToString());
        }
    }
}