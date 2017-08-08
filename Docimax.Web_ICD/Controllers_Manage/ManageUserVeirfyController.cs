using Docimax.Common_ICD;
using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using Docimax.Web_ICD.Convert;
using Docimax.Web_ICD.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Docimax.Web_ICD.Controllers
{
    [Authorize(Roles = ConstStr.PlatformRoleName)]
    public class ManageUserVeirfyController : Controller
    {
        public ActionResult Index(ICDTimePagedList<UserCertificationSearch, VerifyIdentityViewModel> model, string message = "")
        {
            ViewBag.StatusMessage = message;
            if (model.SearchModel == null)
            {
                model.SearchModel = new UserCertificationSearch { CertificateStatus = CertificateState.发起认证申请 };
            }
            IUserAccess access = new DAL_UserAccess();
            //恶心代码
            var queryModel = new ICDTimePagedList<UserCertificationSearch, VerifyUserModel>
            {
                SearchModel = model.SearchModel,
                Page = model.Page,
                PageSize = model.PageSize,
                TextFilter = model.TextFilter,
            };
            queryModel = access.GetUserVerifyList(queryModel);
            model.Content = queryModel.Content.Select(e => Model2ViewModel.Model2VerifyIdentityModel(e)).ToList();
            model.TotalRecords = queryModel.TotalRecords;
            return View(model);
        }

        public ActionResult Detail(string userID)
        {
            IUserAccess access = new DAL_UserAccess();
            var model = access.GetVerifyIdentityModel(userID);
            return View(Model2ViewModel.Model2VerifyIdentityModel(model));
        }

        public ActionResult ApproveDetail(string userID)
        {
            IUserAccess access = new DAL_UserAccess();
            var model = access.GetVerifyIdentityModel(userID);
            return View(Model2ViewModel.Model2VerifyIdentityModel(model));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveDetail(VerifyIdentityViewModel model, string approve, string refuse, string freeze, string unfreeze)
        {
            var identitymodel = ViewModel2Model.VerifyIdentityModel2Model(model);
            var userID = User.Identity.GetUserId();
            identitymodel.LastModifyUserID = userID;
            identitymodel.LastModifyTime = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(approve))
            {
                identitymodel.CertificateFlag = CertificateState.认证通过;
            }
            if (!string.IsNullOrWhiteSpace(refuse))
            {
                identitymodel.CertificateFlag = CertificateState.认证拒绝;
            }
            if (!string.IsNullOrWhiteSpace(freeze))
            {
                identitymodel.CertificateFlag = CertificateState.冻结;
            }
            if (!string.IsNullOrWhiteSpace(unfreeze))
            {
                identitymodel.CertificateFlag = CertificateState.认证通过;
            }
            IUserAccess access = new DAL_UserAccess();
            var result = access.AuditIdentityVerify(identitymodel);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.ErrorStr);
                var newModel = access.GetVerifyIdentityModel(model.UserID);
                return View(Model2ViewModel.Model2VerifyIdentityModel(newModel));
            }
            return RedirectToAction("Index", new { message = string.Format("{0}实名认证{1}成功", model.UserName, identitymodel.CertificateFlag) });
        }
    }
}