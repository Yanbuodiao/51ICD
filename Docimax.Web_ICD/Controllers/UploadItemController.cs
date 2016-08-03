using Docimax.Common_ICD.File;
using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Docimax.Web_ICD.Controllers
{
    [Authorize]
    public class UploadItemController : Controller
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
            model = access.GetUpLoadedCodeOrderList(model);
            //model.PageList = model.Content.ToPagedList(model.Page, model.PageSize);
            return View(model);
        }
        public ActionResult Create(int orderID = 0, string caseNum = null, string notifyStr = null)
        {
            if (!string.IsNullOrWhiteSpace(caseNum))
            {
                ViewBag.StatusMessage = string.Format("病案号：{0}{1}", caseNum, notifyStr);
            }
            ICode_Order code_Order_Access = new DAL_Code_Order();
            CodeOrderModel model = null;
            if (orderID == 0)
            {
                model = code_Order_Access.GetNewCodeOrder(User.Identity.GetUserId(), "ICD编码服务");
                ViewBag.Title = "创建新订单";
                ViewBag.CaseNumDisabled = true;
            }
            else
            {
                model = code_Order_Access.GetCodeOrderDetail(User.Identity.GetUserId(), orderID);
                ViewBag.Title = "订单更新";
                ViewBag.CaseNumDisabled = false;
            }
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
                model.ServiceID = 1;
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
            ICode_Order access = new DAL_Code_Order();
            var model = access.GetCodeOrderDetail(User.Identity.GetUserId(), orderID);
            return View(model);
        }
        public ActionResult Update(int orderID)
        {
            ICode_Order access = new DAL_Code_Order();
            var model = access.GetCodeOrderDetail(User.Identity.GetUserId(), orderID);
            return View(model);
        }

        public ActionResult ShowPic(string picURL, string contentType)
        {
            return File(FileHelper.GetFile(picURL), contentType);
        }
    }
}