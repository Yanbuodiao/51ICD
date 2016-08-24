using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using Docimax.Web_ICD.Convert;
using Docimax.Web_ICD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docimax.Web_ICD.Controllers
{
    [Authorize(Roles = "DocimaxAdmins")]
    public class ManageUserServiceController : Controller
    {
        // GET: ManageUserService
        public ActionResult Index(ICDPagedList<UserCertificationSearch, VerifyIdentityViewModel> model)
        {
            if (model.SearchModel == null)
            {
                model.SearchModel = new UserCertificationSearch();
            }
            IUserAccess access = new DAL_UserAccess();
            //恶心代码
            var queryModel = new ICDPagedList<UserCertificationSearch, VerifyIdentityModel>
            {
                SearchModel = model.SearchModel,
                Page = model.Page,
                PageSize = model.PageSize,
                TextFilter = model.TextFilter,
            };
            queryModel = access.GetUpLoadedCodeOrderList(queryModel);
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
    }
}