using Docimax.Common_ICD;
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
        public ActionResult Create(int codeOrderID = 0, string caseNum = null, string notifyStr = null)
        {
            if (!string.IsNullOrWhiteSpace(caseNum))
            {
                ViewBag.StatusMessage = string.Format("病案号：{0}{1}", caseNum, notifyStr);
            }
            ICode_Order code_Order_Access = new DAL_Code_Order();
            CodeOrderModel model = null;
            if (codeOrderID == 0)
            {
                model = code_Order_Access.GetNewCodeOrder(User.Identity.GetUserId(), "ICD编码");
                model.CaseNum = string.Empty;
                ViewBag.Sign = "New";
            }
            else
            {
                model = code_Order_Access.GetCodeOrderDetail(User.Identity.GetUserId(), codeOrderID);
            }
            ViewBag.Title = "创建新订单";
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CodeOrderModel model, string submit)
        {
            if (ModelState.IsValid)
            {
                if (model.CodeOrderID == 0)
                {
                    model.CreateTime = DateTime.Now;
                    model.CreateUserID = User.Identity.GetUserId();
                    model.ServiceID = 1;
                }
                model.UploadedList = new List<UploadedItemModel>();
                model.LastModifyTime = DateTime.Now;
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
                    if (!string.IsNullOrWhiteSpace(submit))
                    {
                        return RedirectToAction("Create", "UploadItem", new { caseNum = model.CaseNum, notifyStr = "提交成功" });
                    }
                    return RedirectToAction("Create", "UploadItem", new { codeOrderID = result.TResult, caseNum = model.CaseNum, notifyStr = "保存成功" });
                }
                ModelState.AddModelError("", result.ErrorStr);
                return View(model);
            }
            return View(model);
        }

        public ActionResult Update(int orderID)
        {
            ICode_Order code_Order_Access = new DAL_Code_Order();
            var model = code_Order_Access.GetCodeOrderDetail(User.Identity.GetUserId(), orderID);
            ViewBag.Title = "更新订单";
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CodeOrderModel model, string submit)
        {
            model.UploadedList = new List<UploadedItemModel>();
            model.LastModifyTime = DateTime.Now;
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
                return RedirectToAction("Index", "UploadItem", new { caseNum = model.CaseNum, notifyStr = string.IsNullOrWhiteSpace(submit) ? "保存成功" : "提交成功" });
            }
            ModelState.AddModelError("", result.ErrorStr);
            return View(model);
        }

        public ActionResult Detail(int orderID)
        {
            ICode_Order access = new DAL_Code_Order();
            var model = access.GetCodeOrderDetail(User.Identity.GetUserId(), orderID);
            return View(model);
        }
        public ActionResult ShowPic(string picURL, string contentType)
        {
            //todo  增加授权认证，不是谁想看图片就看的
            return File(FileHelper.GetFile(picURL), contentType);
        }

        [Authorize(Roles = ConstStr.PlatformRoleName)]
        public ActionResult ShowUserPic(string picURL, string contentType)
        {
            //todo  增加授权认证，不是谁想看图片就看的
            return File(FileHelper.GetFile(picURL), contentType);
             
        }
    }
}