using Docimax.Common_ICD.File;
using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Mvc;

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
            ICode_Order code_Order_Access = new DAL_Code_Order();
            var model = code_Order_Access.GetNewCodeOrder(User.Identity.GetUserId(), "ICD编码服务");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CodeOrderModel model)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file != null && file.ContentLength > 0)
                    {
                        var icdFile = new ICDFile
                        {
                            FileURL = FileHelper.SaveUserAttachFile(file),
                            ContentType = file.ContentType,
                            AttachType = int.Parse(Request.Files.AllKeys[i]),
                        };
                    }
                }
            }
            return View(model);
        }
    }
}