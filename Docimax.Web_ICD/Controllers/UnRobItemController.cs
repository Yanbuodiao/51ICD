using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace Docimax.Web_ICD.Controllers
{
    [Authorize]
    public class UnRobItemController : Controller
    {
        // GET: UnRobItem
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var model = new ICDPagedList<CodeOrderSearchModel, CodeOrderModel>
            {
                BeginDate = DateTime.Now.Date.AddDays(-30),
                EndDate = DateTime.Now.Date,
                SearchModel = new CodeOrderSearchModel { OrderState = ICDOrderState.待抢单 },
                CurrentPage = page,
                PageSize = pageSize,
            };
            model.SearchModel.UserID = User.Identity.GetUserId();
            ICode_Order access = new DAL_Code_Order();
            model = access.GetUnClaimOrderList(model);
            return View(model);
        }
    }
}