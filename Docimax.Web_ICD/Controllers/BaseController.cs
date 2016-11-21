using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Docimax.Web_ICD.Controllers
{
    public class BaseController : Controller, IActionFilter
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
    }
}