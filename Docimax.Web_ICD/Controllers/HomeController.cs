using Docimax.Common;
using Docimax.Common_ICD;
using Docimax.Data_ICD.DAL;
using Docimax.Data_ICD.DAL.WebChat;
using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Interface.WebChat;
using Docimax.Interface_ICD.Model;
using Docimax.Interface_ICD.Model.WebChat;
using Docimax.Web_ICD.Models;
using JiebaNet.Segmenter;
using JiebaNet.Segmenter.PosSeg;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult ICDList(ICDPagedList<CodeSearchModel, ICDModel> model)
        {
            model = initialList(model);
            var itemsStr = JsonHelper.SerializeObject(model);
            return Content(itemsStr);
        }

        public ActionResult ICDViewModelList(ICDPagedList<CodeSearchModel, ICDViewModel> model)
        {
            model.Content = initialICDViewModelList(model);
            var itemsStr = JsonHelper.SerializeObject(model);
            return Content(itemsStr);
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
            if (model != null)
            {
                model.ICDType = icdType;
            }
            return View(model);
        }

        public ActionResult GetICDDetail(ICDTypeEnum icdType, int icdDetailID)
        {
            IICDRepository access = new DAL_ICDRepository();
            var model = access.GetICDModel(icdDetailID, icdType);
            var modelStr = JsonHelper.SerializeObject(model);
            return Content(modelStr);
        }

        public ActionResult CanEdit()
        {
            if (User == null || User.Identity == null || string.IsNullOrWhiteSpace(User.Identity.GetUserId()))
            {
                return Content("false");
            }
            return Content("true");
        }


        #region 私有方法

        private List<ICDViewModel> initialICDViewModelList(ICDPagedList<CodeSearchModel, ICDViewModel> model)
        {
            if (model != null && model.TextFilter != null && model.TextFilter.Contains('-'))
            {
                model.TextFilter = model.TextFilter.Split('-')[0];
            }
            saveSearchLog(model);
            var result = ICDVersionList.GetICDViewModelList(model.TextFilter, model.SearchModel.ICDType.GetHashCode());
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
            saveSearchLog(model);
            IService access = new DAL_Service();
            Dictionary<int, string> items = access.GetICDVersion(model.SearchModel.ICDType);
            if (model.SearchModel.IcdVersionID == 0 && items != null && items.Count > 0)
            {
                model.SearchModel.IcdVersionID = items.FirstOrDefault().Key;
            }
            var selectList = new SelectList(items, "Key", "Value");
            ViewData["IcdVersion"] = selectList;
            model.Content = buildICDList(model);
            return model;
        }
        private List<ICDModel> buildICDList(ICDPagedList<CodeSearchModel, ICDModel> model)
        {
            if (model != null && model.TextFilter != null && model.TextFilter.Contains('-'))
            {
                model.TextFilter = model.TextFilter.Split('-')[0];
            }
            var result = ICDVersionList.GetICDList(model.SearchModel.IcdVersionID, model.TextFilter);
            model.TotalRecords = result.Count();
            result = result.Skip((model.PageIndex - 1) * model.PageSize)
                    .Take(model.PageSize).ToList();
            return result;
        }

        private void saveSearchLog(ICDPagedList<CodeSearchModel, ICDModel> model)
        {
            if (model != null)
            {
                var logModel = new WebChatUserSearchLog
                {
                    SearchFilter = model.TextFilter,
                };
                if (model.SearchModel != null)
                {
                    if (!string.IsNullOrWhiteSpace(model.SearchModel.ICDSessionKey))
                    {
                        if (HttpRuntime.Cache[model.SearchModel.ICDSessionKey] != null && (HttpRuntime.Cache[model.SearchModel.ICDSessionKey] is WebChatSession))
                        {
                            var webChatSession = HttpRuntime.Cache[model.SearchModel.ICDSessionKey] as WebChatSession;
                            logModel.OpentId = webChatSession.openid;
                        }
                    }
                    if (model.TextFilter!=null&&!model.TextFilter.Trim().Contains(" "))
                    {
                        var jbSeg = new JiebaSegmenter();
                        jbSeg.LoadUserDict(Path.Combine(ConfigManager.ConfigFileBaseDir, "51ICDdict.txt"));
                        var posSeg = new PosSegmenter(jbSeg);
                        var tokens = posSeg.Cut(model.TextFilter.ToUpper(), true);
                        var segmentResult = string.Join(" ", tokens.Select(token => string.Format("{0} ", token.Word, token.Flag))).Trim();
                        model.TextFilter = segmentResult;
                        logModel.SegmentResult = segmentResult;
                    }
                    logModel.ICDVersionID = model.SearchModel.IcdVersionID == 0 ? -1 : model.SearchModel.IcdVersionID;
                    logModel.ICDVersionType = model.SearchModel.ICDType.GetHashCode();
                }
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    IWebChatUserSearchLog logAccess = new DAL_WebChatUserSearchLog();
                    logAccess.InsertUserSearchLog(logModel);
                });
            }
        }

        private void saveSearchLog(ICDPagedList<CodeSearchModel, ICDViewModel> model)
        {
            var newModel = new ICDPagedList<CodeSearchModel, ICDModel>
            {
                SearchModel = model.SearchModel,
                TextFilter = model.TextFilter,
            };
            saveSearchLog(newModel);
            model.TextFilter = newModel.TextFilter;
        }

        #endregion
    }
}