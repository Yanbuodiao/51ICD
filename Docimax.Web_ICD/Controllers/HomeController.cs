﻿using Docimax.Interface_ICD.Model;
using System.Web.Mvc;
using Docimax.Interface_ICD.Enum;
using Microsoft.AspNet.Identity;
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
            model = initialList(model);
            return View(model);
        }

        public PartialViewResult _Tools(ICDPagedList<CodeSearchModel, ICDModel> model)
        {
            model = initialList(model);
            return PartialView(model);
        }

        public ActionResult DropDownParial(ICDTypeEnum icdType, string dropDownListName)
        {
            ViewData["ddlName"] = dropDownListName;
            IService access = new DAL_Service();
            Dictionary<int, string> items = access.GetICDVersion(icdType);
            var selectList = new SelectList(items, "Key", "Value");
            return PartialView(selectList);
        }

        public ActionResult ICDDetail(ICDTypeEnum icdType, int icdDetailID)
        {
            IICDRepository access = new DAL_ICDRepository();
            var model = access.GetICDModel(icdDetailID, icdType);
            if (model!=null)
            {
                model.ICDType = icdType;
            }
            return View(model);
        }

        public ActionResult CanEdit()
        {
            if (User==null||User.Identity==null||string.IsNullOrWhiteSpace(User.Identity.GetUserId()))
            {
                return Content("false");
            }
            return Content("true");
        }


        #region 私有方法

        private List<ICDModel> initialICDList(ICDPagedList<CodeSearchModel, ICDModel> model)
        {
            if (model != null && model.TextFilter != null && model.TextFilter.Contains('-'))
            {
                model.TextFilter = model.TextFilter.Split('-')[0];
            }
            var result = ICDVersionList.GetICDList(model.SearchModel.IcdVersionID, model.TextFilter, int.MaxValue);
            model.TotalRecords = result.Count();
            result = result.Skip((model.PageIndex - 1) * model.PageSize)
                    .Take(model.PageSize).ToList();
            return result;
        }
        private ICDPagedList<CodeSearchModel, ICDModel> initialList(ICDPagedList<CodeSearchModel, ICDModel> model)
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
            return model;
        }

        #endregion
    }
}