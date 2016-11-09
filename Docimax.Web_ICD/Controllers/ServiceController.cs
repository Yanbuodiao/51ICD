using Docimax.Common_ICD.File;
using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Docimax.Web_ICD.Controllers.Manage
{
    [Authorize]
    public class ServiceController : Controller
    {
        public ActionResult ServiceApply()
        {
            IService access = new DAL_Service();
            var services = access.GetAllSerevice();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ServiceApply(ServiceModel service)
        {
            IService access = new DAL_Service();
            service = access.GetServiceByID(service.ServiceID);
            ViewBag.Title = service.ServiceName;
            var model = new UserServiceModel { Service = service };
            return View("ServiceApplyDetail", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ServiceApplyDetail(UserServiceModel model)
        {

            var allFileTags = new List<string> { "100" };
            IService sAccess = new DAL_Service();
            if (allFileTags.Any(e => Request.Files[e] == null || Request.Files[e].ContentLength == 0))
            {
                ModelState.AddModelError("", "请上传必要的附件");
                var service = sAccess.GetServiceByID(model.Service.ServiceID);
                model.Service = service;
                return View(model);
            }
            model.UserID = User.Identity.GetUserId();
            model.Service.ServiceAttaches = new List<ServiceAttachModel>();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                if (file != null && file.ContentLength > 0)
                {
                    var attach = new ServiceAttachModel
                    {
                        ContentType = file.ContentType,
                        FileURL = FileHelper.SaveUserServiceAttachFile(file),
                        FileName=file.FileName,
                    };
                    attach.AttachType = (ServiceAttachType)int.Parse(Request.Files.AllKeys[i]);
                    model.Service.ServiceAttaches.Add(attach);
                }
            }
            IUserAccess access = new DAL_UserAccess();
            model.Service.CertificateStatus = CertificateState.发起认证申请;
            var result = access.ApplyServiceProvider(model);
            if (result.IsSuccess)
            {
                return RedirectToAction("Index", "Manage", new { message = Docimax.Web_ICD.Controllers.ManageController.ManageMessageId.ApplyServiceSuccess });
            }
            ModelState.AddModelError("", result.ErrorStr);
            model.Service = sAccess.GetServiceByID(model.Service.ServiceID); ;
            return View(model);
        }
    }
}