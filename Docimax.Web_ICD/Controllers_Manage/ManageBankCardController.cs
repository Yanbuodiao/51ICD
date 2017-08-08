using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using Docimax.Web_ICD.Convert;
using Docimax.Web_ICD.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docimax.Web_ICD.Controllers_Manage
{
    public class ManageBankCardController : Controller
    {
        public ActionResult Index(ICDTimePagedList<UserCertificationSearch, VerifyBankCardModel> model, string message = "")
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
            queryModel = access.GetBankCardVerifyList(queryModel);
            model.Content = queryModel.Content.Select(e => Model2ViewModel.Model2VerifyBankCardModel(e)).ToList();
            model.TotalRecords = queryModel.TotalRecords;
            return View(model);
        }
        public ActionResult Detail(string userID)
        {
            IUserAccess access = new DAL_UserAccess();
            var model = access.GetVerifyIdentityModel(userID);
            return View(Model2ViewModel.Model2VerifyBankCardModel(model));
        }

        public ActionResult ApproveDetail(string userID)
        {
            IUserAccess access = new DAL_UserAccess();
            var model = access.GetVerifyIdentityModel(userID);
            return View(Model2ViewModel.Model2VerifyBankCardModel(model));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveDetail(VerifyBankCardModel model, string approve, string refuse, string freeze, string unfreeze)
        {
            var identitymodel = ViewModel2Model.VerifyIdentityModel2Model(model);
            var userID = User.Identity.GetUserId();
            identitymodel.LastModifyUserID = userID;
            identitymodel.LastModifyTime = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(approve))
            {
                identitymodel.BankCertificationFlag = CertificateState.认证通过;
            }
            if (!string.IsNullOrWhiteSpace(refuse))
            {
                identitymodel.BankCertificationFlag = CertificateState.认证拒绝;
            }
            if (!string.IsNullOrWhiteSpace(freeze))
            {
                identitymodel.BankCertificationFlag = CertificateState.冻结;
            }
            if (!string.IsNullOrWhiteSpace(unfreeze))
            {
                identitymodel.BankCertificationFlag = CertificateState.认证通过;
            }
            IUserAccess access = new DAL_UserAccess();
            var result = access.AuditBankCard(identitymodel);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.ErrorStr);
                var newModel = access.GetVerifyIdentityModel(model.UserID);
                return View(Model2ViewModel.Model2VerifyBankCardModel(newModel));
            }
            return RedirectToAction("Index", new { message = string.Format("{0}银行卡认证{1}成功", model.UserName, identitymodel.BankCertificationFlag) });
        }
    }
}