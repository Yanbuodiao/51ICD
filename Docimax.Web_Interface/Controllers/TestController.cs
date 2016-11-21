using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docimax.Web_Interface.Controllers
{
    public class TestController : BaseController
    {
        public ActionResult Index()
        {
            return Content(Guid.NewGuid().ToString("N"));
        }
    }
}