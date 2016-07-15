using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Docimax.Web_ICD.Controllers
{
    public class UploadItemController : Controller
    {
        // GET: ICDItem
        public ActionResult Index()
        {
            IEnumerable<CodeOrderModel> model = new List<CodeOrderModel>();
            return View(model);
        }
        public ActionResult Create()
        {
            var aa = Request.Url.AbsolutePath;
            ICode_Order code_Order_Access = new DAL_Code_Order();
            var model = code_Order_Access.GetNewCodeOrder(User.Identity.GetUserId(), "ICD编码服务");
            return View(model);
        }
    }
}