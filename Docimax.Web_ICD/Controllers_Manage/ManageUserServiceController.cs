using Docimax.Common_ICD;
using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace Docimax.Web_ICD.Controllers_Manage
{
    [Authorize(Roles = ConstStr.PlatformRoleName)]
    public class ManageUserServiceController : Controller
    {
        public ActionResult Index(ICDPagedList<UserServiceSearch, UserServiceModel> model, string message = "")
        {
            ViewBag.StatusMessage = message;
            if (model.SearchModel == null)
            {
                model.SearchModel = new UserServiceSearch { CertificateStatus = CertificateState.发起认证申请 };
            }
            IUserAccess access = new DAL_UserAccess();
            model = access.GetUserServiceList(model);
            return View(model);
        }

        public ActionResult Detail(int user_serviceID)
        {
            IUserAccess access = new DAL_UserAccess();
            var model = access.GetUserService(user_serviceID);
            return View(model);
        }

        public ActionResult ApproveDetail(int user_serviceID)
        {
            IUserAccess access = new DAL_UserAccess();
            var model = access.GetUserService(user_serviceID);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveDetail(UserServiceModel model, string approve, string refuse, string freeze, string unfreeze)
        {
            var userID = User.Identity.GetUserId();
            model.LastModifyUserID = userID;
            model.LastModifyTime = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(approve))
            {
                model.CertificateFlag = CertificateState.认证通过;
            }
            if (!string.IsNullOrWhiteSpace(refuse))
            {
                model.CertificateFlag = CertificateState.认证拒绝;
            }
            if (!string.IsNullOrWhiteSpace(freeze))
            {
                model.CertificateFlag = CertificateState.冻结;
            }
            if (!string.IsNullOrWhiteSpace(unfreeze))
            {
                model.CertificateFlag = CertificateState.认证通过;
            }
            IUserAccess access = new DAL_UserAccess();
            var result = access.AuditServiceProvider(model);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.ErrorStr);
                var newModel = access.GetUserService(model.User_ServiceID);
                return View(newModel);
            }
            return RedirectToAction("Index", new { message = string.Format("{0}{1}服务{2}成功", model.UserName, model.Service.ServiceName, model.CertificateFlag) });
        }
    }
}