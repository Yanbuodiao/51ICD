using Docimax.Common;
using Docimax.Common_ICD;
using Docimax.Common_ICD.File;
using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using Docimax.Interface_ICD.Model.UploadModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Docimax.Web_ICD.Controllers
{
    [Authorize]
    public class UploadItemController : BaseController
    {
        public ActionResult Index(ICDTimePagedList<CodeOrderSearchModel, CodeOrderModel> model)
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
                handleFile(model);
                ICode_Order codeOrderAccess = new DAL_Code_Order();
                var result = codeOrderAccess.SaveNewCodeOrder(model, !string.IsNullOrWhiteSpace(submit));
                if (result.IsSuccess)
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
            handleFile(model);
            ICode_Order codeOrderAccess = new DAL_Code_Order();
            var result = codeOrderAccess.SaveNewCodeOrder(model, !string.IsNullOrWhiteSpace(submit));
            if (result.IsSuccess)
            {
                return RedirectToAction("Index", "UploadItem", new { caseNum = model.CaseNum, notifyStr = string.IsNullOrWhiteSpace(submit) ? "保存成功" : "提交成功" });
            }
            ModelState.AddModelError("", result.ErrorStr);
            return View(model);
        }

        private void handleFile(CodeOrderModel model)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                if (file != null && file.ContentLength > 0)
                {
                    var itemID = int.Parse(Request.Files.AllKeys[i]);
                    var fileNameIndex = file.FileName.LastIndexOf(@"\");
                    var fileName = file.FileName;
                    if (fileNameIndex > 0)
                    {
                        fileName = fileName.Substring(fileNameIndex + 1, fileName.Length - fileNameIndex - 1);
                    }
                    var uploadedFile = new UploadedItemModel
                    {
                        AttachURL = FileHelper.SaveMedicalRecord(file, model.ORGCode, model.CaseNum),
                        AttachFileName = fileName,
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
        }

        public ActionResult Detail(int orderID, OrderTypeEnum orderType)
        {
            ICode_Order access = new DAL_Code_Order();
            var uid = User.Identity.GetUserId();
            if (!access.CanViewingMR(orderID, uid))
            {
                return View();
            }
            if (orderType == OrderTypeEnum.InterfaceOrder)
            {
                return RedirectToAction("InterfaceOrderDetail", new { orderID = orderID });
            }
            if (orderType==OrderTypeEnum.InterfaceFileOrder)
            {
                return RedirectToAction("InterfaceFileOrderDetail", new { orderID = orderID });
            }
            var model = access.GetCodeOrderDetail(User.Identity.GetUserId(), orderID);
            ViewBag.PlatformOrderCode = model.PlatformOrderCode;
            return View(model);
        }
        public ActionResult ShowPic(string picURL, string contentType)
        {
            //todo  增加授权认证，不是谁想看图片就看的
            return File(FileHelper.GetPicFile(picURL), contentType);
        }

        [Authorize(Roles = ConstStr.PlatformRoleName)]
        public ActionResult ShowUserPic(string picURL, string contentType)
        {
            //todo  增加授权认证，不是谁想看图片就看的
            return File(FileHelper.GetPicFile(picURL), contentType);

        }

        public ActionResult InterfaceOrderDetail(int orderID)
        {
            ICode_Order access = new DAL_Code_Order();
            var uid = User.Identity.GetUserId();
            if (!access.CanViewingMR(orderID, uid))
            {
                return View();
            }
            var model = access.GetCodeOrder(orderID);
            if (model != null)
            {
                var bytes = FileHelper.GetFile(model.MedicalRecordPath);
                var str = System.Text.Encoding.UTF8.GetString(bytes);
                var mr = JsonHelper.DeserializeObject<MedicalRecord_Data>(str);
                ViewBag.PlatformOrderCode = model.PlatformOrderCode;
                return View(mr);
            }
            return View();
        }

        public ActionResult InterfaceFileOrderDetail(int orderID)
        {
            ICode_Order access = new DAL_Code_Order();
            var uid = User.Identity.GetUserId();
            if (!access.CanViewingMR(orderID, uid))
            {
                return View();
            }
            var model = access.GetCodeOrder(orderID);
            if (model != null)
            {
                var bytes = FileHelper.GetFile(model.MedicalRecordPath);
                var str = System.Text.Encoding.UTF8.GetString(bytes);
                var mr = JsonHelper.DeserializeObject<MedicalRecord_File>(str);
                mr.Catalogs = mr.Catalogs.OrderBy(e => e.CatalogOrder).ToList();
                ViewBag.PlatformOrderCode = model.PlatformOrderCode;
                return View(mr);
            }
            return View();
        }
    }
}