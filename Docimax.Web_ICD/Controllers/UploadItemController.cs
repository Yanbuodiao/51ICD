using Docimax.Common_ICD.File;
using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Docimax.Web_ICD.Controllers
{
    [Authorize]
    public class UploadItemController : Controller
    {
        public ActionResult Index(string TextFilter = null, int page = 1, int pageSize = 10, DateTime? BeginDate = null, DateTime? EndDate = null, ICDOrderState? OrderState = null)
        {
            var model = new ICDPagedList<CodeOrderSearchModel, CodeOrderModel>
            {
                BeginDate = BeginDate ?? DateTime.Now.Date.AddDays(-2),
                EndDate = EndDate ?? DateTime.Now.Date,
                SearchModel = new CodeOrderSearchModel { },
                CurrentPage = page,
                TextFilter = TextFilter,
                PageSize = pageSize,
            };
            if (OrderState != null)
            {
                model.SearchModel.OrderState = OrderState ?? ICDOrderState.新创建;
            }
            model.SearchModel.UserID = User.Identity.GetUserId();
            ICode_Order access = new DAL_Code_Order();
            model = access.GetCodeOrderList(model);
            return View(model);
        }
        public ActionResult Create(string caseNum = null, string notifyStr = null)
        {
            if (!string.IsNullOrWhiteSpace(caseNum))
            {
                ViewBag.StatusMessage = string.Format("病案号：{0}{1}", caseNum, notifyStr);
            }
            ICode_Order code_Order_Access = new DAL_Code_Order();
            var model = code_Order_Access.GetNewCodeOrder(User.Identity.GetUserId(), "ICD编码服务");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CodeOrderModel model, string submit)
        {
            if (ModelState.IsValid)
            {
                model.UploadedList = new List<UploadedItemModel>();
                model.CreateTime = DateTime.Now;
                model.LastModifyTime = DateTime.Now;
                model.CreateUserID = User.Identity.GetUserId();
                model.LastModifyUserID = User.Identity.GetUserId();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file != null && file.ContentLength > 0)
                    {
                        var itemID = int.Parse(Request.Files.AllKeys[i]);
                        var uploadedFile = new UploadedItemModel
                        {
                            AttachURL = FileHelper.SaveMedicalRecord(file, model.ORGCode, model.CaseNum),
                            AttachFileName = file.FileName,
                            ContentType = file.ContentType,
                            ItemID = itemID,
                            GIndex = model.UploadedList.Count(e => e.ItemID == itemID),
                            CreateTime = DateTime.Now,
                            LastModifyTime = DateTime.Now,
                            CreateUserID = User.Identity.GetUserId(),
                            LastModifyUserID = User.Identity.GetUserId(),
                        };
                        model.UploadedList.Add(uploadedFile);
                    }
                }
                ICode_Order codeOrderAccess = new DAL_Code_Order();
                var result = codeOrderAccess.SaveNewCodeOrder(model, !string.IsNullOrWhiteSpace(submit));
                if (result.Result)
                {
                    return RedirectToAction("Create", "UploadItem", new { caseNum = model.CaseNum, notifyStr = "保存成功" });
                }
                ModelState.AddModelError("", result.ErrorStr);
                return View(model);
            }
            return View(model);
        }
        public ActionResult Detail(int orderID)
        {
            var model = new CodeOrderModel();
            return View(model);
        }
        public ActionResult Update(int orderID)
        {
            var model = new CodeOrderModel();
            return View(model);
        }
    }
}