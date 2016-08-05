using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Model;
using System;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Docimax.Interface_ICD.Interface;
using Docimax.Data_ICD.DAL;

namespace Docimax.Web_ICD.Controllers
{
    [Authorize]
    public class ServiceItemController : Controller
    {
        public ActionResult Index(ICDPagedList<CodeOrderSearchModel, CodeOrderModel> model)
        {
            if (model.SearchModel == null)
            {
                model.SearchModel = new CodeOrderSearchModel();
            }
            model.BeginDate = model.BeginDate < DateTime.Now.AddYears(-50) ? DateTime.Now.Date.AddDays(-2) : model.BeginDate;
            model.EndDate = model.EndDate < DateTime.Now.AddYears(-50) ? DateTime.Now.Date : model.EndDate;
            model.SearchModel.UserID = User.Identity.GetUserId();
            ICode_Order access = new DAL_Code_Order();
            model = access.GetMyCodeOrderList(model);
            return View(model);
        }

        public ActionResult Code(int orderID)
        {
            ICode_Order access = new DAL_Code_Order();
            var model = access.GetCodeOrderDetail(User.Identity.GetUserId(), orderID);
            return View(model);
        }
    }
}