﻿using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Model;
using System;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Docimax.Interface_ICD.Interface;
using Docimax.Data_ICD.DAL;
using System.Linq;
using Docimax.Common_ICD.File;
using Docimax.Common;
using Docimax.Interface_ICD.Model.UploadModel;
using Docimax.Common_ICD;

namespace Docimax.Web_ICD.Controllers
{
    [Authorize]
    public class ServiceItemController : Controller
    {
        public ActionResult Index(ICDTimePagedList<CodeOrderSearchModel, CodeOrderModel> model)
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

        public ActionResult Code(int orderID, OrderTypeEnum orderType)
        {
            if (orderType == OrderTypeEnum.InterfaceOrder)
            {
                return RedirectToAction("InterfaceOrdeCode", new { orderID = orderID });
            }
            ICode_Order access = new DAL_Code_Order();
            var model = access.GetCodeOrderDetail(User.Identity.GetUserId(), orderID);
            initalDiagnosis(model);
            initialOperate(model);
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

            model.LastModifyUserID = userID;
            model.LastModifyTime = DateTime.Now;
            model.OrderStatus = string.IsNullOrWhiteSpace(submit) ? ICDOrderState.编码中 : ICDOrderState.编码完成;

            var result = access.SaveCodeResult(model);
            if (result.IsSuccess)
            {
                if (!string.IsNullOrWhiteSpace(submit))
                {
                    return RedirectToAction("Index", "ServiceItem");
                }
                model = access.GetCodeOrderDetail(userID, model.CodeOrderID);
                ModelState.Remove("LastModifyStamp");
                initalDiagnosis(model);
                initialOperate(model);
                return View(model);
            }
            ModelState.AddModelError("", result.ErrorStr);
            return returnOriginModel(model, access, userID);
        }

        public ActionResult InterfaceOrdeCode(int orderID)
        {
            ICode_Order access = new DAL_Code_Order();
            var model = access.GetCodeOrder(orderID);
            if (model != null)
            {
                var bytes = FileHelper.GetFile(model.MedicalRecordPath);
                var str = System.Text.Encoding.UTF8.GetString(bytes);
                var mr = JsonHelper.DeserializeObject<MedicalRecordCoding>(str);
                mr = initalCode(mr, model.Diagnosis_ICD_VersionID, model.Operation_ICD_VersionID);
                ViewBag.PlatformOrderCode = model.PlatformOrderCode;
                return View(mr);
            }
            //todo  错误提示
            return View();
        }

        #region private Function

        private void initialOperate(CodeOrderModel model)
        {
            if (model.OperateList == null || model.OperateList.Count == 0)
            {
                model.OperateList = new System.Collections.Generic.List<Code_Operate> { 
                    new Code_Operate{},
                    new Code_Operate{},
                    new Code_Operate{},
                    new Code_Operate{},
                    new Code_Operate{},};
                return;
            }
            var newOperateList = new System.Collections.Generic.List<Code_Operate>();
            var maxIndex = model.OperateList.Max(e => e.OperateIndex) > 5 ? model.OperateList.Max(e => e.OperateIndex) : 5;
            for (int i = 0; i < maxIndex; i++)
            {
                var item = model.OperateList.FirstOrDefault(e => e.OperateIndex == i);
                if (item != null)
                {
                    newOperateList.Add(item);
                }
                else
                {
                    newOperateList.Add(new Code_Operate());
                }
            }
            model.OperateList = newOperateList;
        }

        private void initalDiagnosis(CodeOrderModel model)
        {
            if (model.DiagnosisList == null || model.DiagnosisList.Count == 0)
            {
                model.DiagnosisList = new System.Collections.Generic.List<Code_Diagnosis> { 
                    new Code_Diagnosis{Description="主要诊断"},
                    new Code_Diagnosis{Description="其他诊断"},
                    new Code_Diagnosis{},
                    new Code_Diagnosis{},
                    new Code_Diagnosis{},
                };
                return;
            }
            var newDiagnosisList = new System.Collections.Generic.List<Code_Diagnosis>();
            var maxIndex = model.DiagnosisList.Max(e => e.DiagnosisIndex) > 5 ? model.DiagnosisList.Max(e => e.DiagnosisIndex) : 5;
            for (int i = 0; i < maxIndex; i++)
            {
                var item = model.DiagnosisList.FirstOrDefault(e => e.DiagnosisIndex == i);
                if (item != null)
                {
                    newDiagnosisList.Add(item);
                }
                else
                {
                    newDiagnosisList.Add(new Code_Diagnosis { Description = i == 0 ? "主要诊断" : (i == 1 ? "其他诊断" : null) });
                }
            }
            model.DiagnosisList = newDiagnosisList;
        }

        private MedicalRecordCoding initalCode(MedicalRecordCoding mr, int diagnosisVersionId, int operateVersionId)
        {
            if (mr.DiagnosisCodeResultList == null)
            {
                //初始化诊断编码结果
                if (mr.DischargeDiagnosisList != null)//首页有结果直接取首页结果
                {
                    mr.DiagnosisCodeResultList = mr.DischargeDiagnosisList
                        .Select(e => new DiagnosisCodeResult { Index = e.Index, Diagnosis = e.Diagnosis, DiagnosisCode = ICDVersionList.GetCodeByName(diagnosisVersionId, e.Diagnosis) }).ToList();
                }
                else if (mr.DischargeRecord != null && mr.DischargeRecord.DischargeDiagnosis != null)//首页没结果去出院记录或者死亡记录中取结果
                {
                    mr.DiagnosisCodeResultList = mr.DischargeRecord.DischargeDiagnosis.Split(';').Where(e => !string.IsNullOrWhiteSpace(e))
                        .Select(t => new DiagnosisCodeResult { Index = 1, Diagnosis = t, DiagnosisCode = ICDVersionList.GetCodeByName(diagnosisVersionId, t) }).ToList();
                }
                else if (mr.DeathNote != null && mr.DeathNote.DeathDiagnosis != null)
                {
                    mr.DiagnosisCodeResultList = mr.DeathNote.DeathDiagnosis.Split(';').Where(e => !string.IsNullOrWhiteSpace(e))
                        .Select(t => new DiagnosisCodeResult { Index = 1, Diagnosis = t, DiagnosisCode = ICDVersionList.GetCodeByName(diagnosisVersionId, t) }).ToList();
                }
            }
            if (mr.OperationCodeResultList == null)
            {
                //初始化手术操作编码结果
                if (mr.ProceduresList != null)//首页有结果直接取首页结果
                {
                    mr.OperationCodeResultList = mr.ProceduresList
                        .Select(e => new OperationCodeResult { Index = e.Index, OperationName = e.OperationName, OperationCode = ICDVersionList.GetCodeByName(operateVersionId, e.OperationName) }).ToList();
                }
                else if (mr.OperationDetailList != null)//先去手术详细记录中取手术名称
                {
                    mr.OperationCodeResultList = mr.OperationDetailList
                        .Select(e => new OperationCodeResult { OperationName = e.OperationName, OperationCode = ICDVersionList.GetCodeByName(operateVersionId, e.OperationName) }).ToList();
                    //todo   需要调研临时医嘱中操作的进入首页结果中
                }
            }
            return mr;
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
            if (model.DiagnosisList.All(e => string.IsNullOrWhiteSpace(e.DisplayText)))
            {
                return "请输入至少一个诊断";
            }
            if (model.OperateList.All(e => string.IsNullOrWhiteSpace(e.DisplayText)))
            {
                return "请输入至少一个操作";
            }
            for (int i = 0; i < model.DiagnosisList.Count; i++)
            {
                var item = model.DiagnosisList[i]; if (!string.IsNullOrWhiteSpace(item.DisplayText))
                {
                    var temp = item.DisplayText.Split('-');
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
                if (!string.IsNullOrWhiteSpace(item.DisplayText))
                {
                    var temp = item.DisplayText.Split('-');
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

        #endregion
    }
}