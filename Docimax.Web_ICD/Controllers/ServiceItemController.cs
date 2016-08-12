using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Model;
using System;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Docimax.Interface_ICD.Interface;
using Docimax.Data_ICD.DAL;
using System.Linq;

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
            model.BeginDate = model.BeginDate < DateTime.Now.AddYears(-50) ? DateTime.Now.Date.AddDays(-10) : model.BeginDate;
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
            model.DiagnosisList = new System.Collections.Generic.List<Code_Diagnosis> { 
                new Code_Diagnosis{Description="主要诊断"},
                new Code_Diagnosis{Description="其他诊断"},
                new Code_Diagnosis{},
                new Code_Diagnosis{},
                new Code_Diagnosis{},
            };
            model.OperateList = new System.Collections.Generic.List<Code_Operate> { 
                new Code_Operate{},
                new Code_Operate{},
                new Code_Operate{},
                new Code_Operate{},
                new Code_Operate{},
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Code(CodeOrderModel model, string submit)
        {
            ICode_Order access = new DAL_Code_Order();
            var userID = User.Identity.GetUserId();
            var validateResult = validateCodeModel(ref model, userID);
            if (!string.IsNullOrWhiteSpace(validateResult))
            {
                ModelState.AddModelError("", validateResult);
                return returnOriginModel(model, access, userID);
            }
            var result = access.SaveCodeResult(model);
            if (result.Result)
            {
                if (!string.IsNullOrWhiteSpace(submit))
                {
                    return RedirectToAction("Index", "ServiceItem");
                }
                return View(model);
            }
            ModelState.AddModelError("", result.ErrorStr);
            return returnOriginModel(model, access, userID);
        }

        private ActionResult returnOriginModel(CodeOrderModel model, ICode_Order access, string userID)
        {
            var newModel = access.GetCodeOrderDetail(userID, model.CodeOrderID);
            newModel.OperateList = model.OperateList;
            newModel.DiagnosisList = model.DiagnosisList;
            return View(newModel);
        }

        private string validateCodeModel(ref CodeOrderModel model, string userID)
        {
            if (model.PickedUserID != userID)
            {
                return "您无权编辑此订单";
            }
            if (model.DiagnosisList.All(e => string.IsNullOrWhiteSpace(e.ICD_Content)))
            {
                return "请输入至少一个诊断";
            }
            if (model.OperateList.All(e => string.IsNullOrWhiteSpace(e.ICDContent)))
            {
                return "请输入至少一个操作";
            }
            for (int i = 0; i < model.DiagnosisList.Count; i++)
            {
                var item = model.DiagnosisList[i]; if (!string.IsNullOrWhiteSpace(item.ICD_Content))
                {
                    var temp = item.ICD_Content.Split('-');
                    if (temp.Length == 2)
                    {
                        item.ICD_Code = temp[0];
                        item.ICD_Content = temp[1];
                        item.DiagnosisIndex = i;
                        item.CreateTime = DateTime.Now;
                        item.CreateUserID = userID;
                        item.LastModifyTime = DateTime.Now;
                        item.LastModifyUserID = userID;
                    }
                    else
                    {
                        return "请输入合法的诊断";
                    }
                }
            }
            for (int i = 0; i < model.OperateList.Count; i++)
            {
                var item = model.OperateList[i];
                if (!string.IsNullOrWhiteSpace(item.ICDContent))
                {
                    var temp = item.ICDContent.Split('-');
                    if (temp.Length == 2)
                    {
                        item.ICDCode = temp[0];
                        item.ICDContent = temp[1];
                        item.OperateIndex = i;
                        item.CreateTime = DateTime.Now;
                        item.CreateUserID = userID;
                        item.LastModifyTime = DateTime.Now;
                        item.LastModifyUserID = userID;
                    }
                    else
                    {
                        return "请输入合法的操作";
                    }
                }
            }
            return string.Empty;
        }
    }
}