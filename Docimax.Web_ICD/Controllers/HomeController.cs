using Docimax.Interface_ICD.Model;
using System.Web.Mvc;
using Docimax.Interface_ICD.Enum;
using System.Collections.Generic;
using Docimax.Interface_ICD.Interface;
using Docimax.Data_ICD.DAL;
using System.Linq;
using Docimax.Common_ICD;

namespace Docimax.Web_ICD.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "51ICD平台描述页面.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "51ICD平台合同信息.";

            return View();
        }
        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult ShowDetail()
        {
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }
        public ActionResult Tools(ICDPagedList<CodeSearchModel, ICDModel> model)
        {
            if (model == null)
            {
                model = new ICDPagedList<CodeSearchModel, ICDModel>();
            }
            if (model.SearchModel == null)
            {
                model.SearchModel = new CodeSearchModel { ICDType = ICDTypeEnum.诊断编码, IcdVersionID = 1 };
            }

            IService access = new DAL_Service();
            Dictionary<int, string> items = access.GetICDVersion(model.SearchModel.ICDType);
            if (model.SearchModel.IcdVersionID == 0 && items != null && items.Count > 0)
            {
                model.SearchModel.IcdVersionID = items.FirstOrDefault().Key;
            }
            var selectList = new SelectList(items, "Key", "Value");
            ViewData["IcdVersion"] = selectList;
            model.Content = initialICDList(model);
            return View(model);
        }

        public ActionResult DropDownParial(ICDTypeEnum icdType, string dropDownListName)
        {
            ViewData["ddlName"] = dropDownListName;
            IService access = new DAL_Service();
            Dictionary<int, string> items = access.GetICDVersion(icdType);
            var selectList = new SelectList(items, "Key", "Value");
            return PartialView(selectList);
        }

        private List<ICDModel> initialICDList(ICDPagedList<CodeSearchModel, ICDModel> model)
        {
            var result = ICDVersionList.GetICDList(model.SearchModel.IcdVersionID, model.TextFilter, int.MaxValue);
            model.TotalRecords = result.Count();
            result = result.Skip((model.Page - 1) * model.PageSize)
                    .Take(model.PageSize).ToList();
            return result;
        }
    }
}