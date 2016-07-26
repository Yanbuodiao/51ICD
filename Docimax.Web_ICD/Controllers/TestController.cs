using Docimax.Common_ICD.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docimax.Web_ICD.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestShowFile()
        {
            var url = "51icd-sysblob/user-attach/160726-113115739.jpg";
            return File(FileHelper.GetFile(url), "image/jpeg");
        }

    }
}