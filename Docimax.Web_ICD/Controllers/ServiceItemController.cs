using Docimax.Interface_ICD.Enum;
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
using System.Collections.Generic;
using System.Text;

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
            if (orderType == OrderTypeEnum.InterfaceFileOrder)
            {
                return RedirectToAction("InterfaceFileOrdeCode", new { orderID = orderID });
            }
            ICode_Order access = new DAL_Code_Order();
            var model = access.GetCodeOrderDetail(User.Identity.GetUserId(), orderID);
            initalDiagnosis(model);
            initialOperate(model);
            ViewBag.PlatformOrderCode = model.PlatformOrderCode;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Code(CodeOrderModel model, string submit)
        {
            ViewBag.PlatformOrderCode = model.PlatformOrderCode;
            ModelState.Remove("LastModifyStamp");
            ICode_Order access = new DAL_Code_Order();
            var userID = User.Identity.GetUserId();
            var validateResult = validateCodeModel(model.CodeOrderID, model.DiagnosisCodeResultList, model.OperationCodeResultList, userID);
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
                ModelState.Clear();
                if (!string.IsNullOrWhiteSpace(submit))
                {
                    return RedirectToAction("Index", "ServiceItem");
                }
                model = access.GetCodeOrderDetail(userID, model.CodeOrderID);
                initalDiagnosis(model);
                initialOperate(model);
                return View(model);
            }
            ModelState.AddModelError("", result.ErrorStr);
            return returnOriginModel(model, access, userID);
        }

        public ActionResult InterfaceOrdeCode(int orderID)
        {
            var userId = User.Identity.GetUserId();
            ICode_Order access = new DAL_Code_Order();
            if (!access.CanCodingMR(orderID, userId))
            {
                ViewBag.ErrorStr = "您无权限查看该订单！";
                return View();
            }
            var inModel = access.GetCodeOrder(orderID);
            if (inModel != null)
            {
                var bytes = FileHelper.GetFile(inModel.MedicalRecordPath);
                var str = Encoding.UTF8.GetString(bytes);
                var mr = JsonHelper.DeserializeObject<MedicalRecord_Data>(str);
                if (mr != null)
                {
                    mr = initalCode(mr, inModel.Diagnosis_ICD_VersionID, inModel.Operation_ICD_VersionID);
                    mr.CodeOrderID = orderID;
                    mr.LastModifyStamp = inModel.LastModifyStamp;
                }
                ViewBag.PlatformOrderCode = inModel.PlatformOrderCode;
                return View(mr);
            }
            return View();
        }

        public ActionResult InterfaceFileOrdeCode(int orderID)
        {
            var userId = User.Identity.GetUserId();
            ICode_Order access = new DAL_Code_Order();
            if (!access.CanCodingMR(orderID, userId))
            {
                ViewBag.ErrorStr = "您无权限查看该订单！";
                return View();
            }
            var inModel = access.GetCodeOrder(orderID);
            if (inModel != null)
            {
                var bytes = FileHelper.GetFile(inModel.MedicalRecordPath);
                var str = Encoding.UTF8.GetString(bytes);
                var mr = JsonHelper.DeserializeObject<MedicalRecord_File>(str);
                if (mr != null)
                {
                    mr = initalCode(mr);
                    mr.CodeOrderID = orderID;
                    mr.LastModifyStamp = inModel.LastModifyStamp;
                }
                ViewBag.PlatformOrderCode = inModel.PlatformOrderCode;
                return View(mr);
            }
            return View();
        }

        public ActionResult AddDialogios(int dialogIndex)
        {
            ViewBag.Add = true;
            return PartialView("DialogiosPartial", new DiagnosisCodeResult { Index = dialogIndex });
        }

        public ActionResult AddOperate(int operateIndex)
        {
            ViewBag.Add = "true";
            return PartialView("OperatePartial", new OperationCodeResult { Index = operateIndex });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InterfaceOrdeCode(MedicalRecord_Data model, string submit)
        {
            ModelState.Remove("LastModifyStamp");
            var userID = User.Identity.GetUserId();
            var validateResult = validateCodeModel(model.CodeOrderID, model.DiagnosisCodeResultList, model.OperationCodeResultList, userID);
            if (!string.IsNullOrWhiteSpace(validateResult))
            {
                MedicalRecord_Data mr = getDataMr(model);
                ModelState.AddModelError("", validateResult);
                if (mr != null)
                {
                    return View(mr);
                }
                else
                {
                    ModelState.AddModelError("", "异常数据提交");
                    return View(model);
                }
            }
            model.LastModifyUserID = userID;
            model.LastModifyTime = DateTime.Now;
            model.OrderStatus = string.IsNullOrWhiteSpace(submit) ? ICDOrderState.编码中 : ICDOrderState.编码完成;

            ICode_Order access = new DAL_Code_Order();
            var result = access.SaveCodeResult(model);
            if (result.IsSuccess)
            {
                MedicalRecord_Data mr = getDataMr(model);
                IFile fileAccess = new File_Azure();
                var medicalRecordPath = fileAccess.SaveMedicalFile(Encoding.UTF8.GetBytes(JsonHelper.SerializeObject(mr)), mr.MedicalRecordPath);
                if (!string.IsNullOrWhiteSpace(submit))
                {
                    return RedirectToAction("Index", "ServiceItem");
                }
                else
                {                   
                    ModelState.Clear();
                    return View(mr);
                }
            }
            else
            {
                ModelState.AddModelError("", result.ErrorStr);
                MedicalRecord_Data mr = getDataMr(model);
                if (mr != null)
                {
                    return View(mr);
                }
                else
                {
                    ModelState.AddModelError("", "异常数据提交");
                    return View(model);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InterfaceFileOrdeCode(MedicalRecord_File model, string submit)
        {
            ModelState.Remove("LastModifyStamp");
            var userID = User.Identity.GetUserId();
            var validateResult = validateCodeModel(model.CodeOrderID, model.DiagnosisCodeResultList, model.OperationCodeResultList, userID);
            if (!string.IsNullOrWhiteSpace(validateResult))
            {
                MedicalRecord_File mr = getFileMr(model);
                ModelState.AddModelError("", validateResult);
                if (mr != null)
                {
                    return View(mr);
                }
                else
                {
                    ModelState.AddModelError("", "异常数据提交");
                    return View(model);
                }
            }
            model.LastModifyUserID = userID;
            model.LastModifyTime = DateTime.Now;
            model.OrderStatus = string.IsNullOrWhiteSpace(submit) ? ICDOrderState.编码中 : ICDOrderState.编码完成;

            ICode_Order access = new DAL_Code_Order();
            var result = access.SaveCodeResult(model);
            if (result.IsSuccess)
            {
                MedicalRecord_File mr = getFileMr(model);
                IFile fileAccess = new File_Azure();
                var medicalRecordPath = fileAccess.SaveMedicalFile(Encoding.UTF8.GetBytes(JsonHelper.SerializeObject(mr)), mr.MedicalRecordPath);
                if (!string.IsNullOrWhiteSpace(submit))
                {
                    return RedirectToAction("Index", "ServiceItem");
                }
                else
                {
                    ModelState.Clear();
                    return View(mr);
                }
            }
            else
            {
                MedicalRecord_File mr = getFileMr(model);
                ModelState.AddModelError("", result.ErrorStr);
                if (mr != null)
                {
                    return View(mr);
                }
                else
                {
                    ModelState.AddModelError("", "异常数据提交");
                    return View(model);
                }
            }
        }

        #region private Function

        private MedicalRecord_Data getDataMr(MedicalRecordBase model)
        {
            ICode_Order access = new DAL_Code_Order();
            MedicalRecord_Data mr = null;
            var inModel = access.GetCodeOrder(model.CodeOrderID);
            if (inModel != null)
            {
                var bytes = FileHelper.GetFile(inModel.MedicalRecordPath);
                var str = Encoding.UTF8.GetString(bytes);
                mr = JsonHelper.DeserializeObject<MedicalRecord_Data>(str);
                if (mr != null)
                {
                    mr.OperationCodeResultList = model.OperationCodeResultList;
                    mr.DiagnosisCodeResultList = model.DiagnosisCodeResultList;
                    mr.CodeOrderID = model.CodeOrderID;
                    mr.LastModifyStamp = inModel.LastModifyStamp;
                    mr.MedicalRecordPath = inModel.MedicalRecordPath;
                }
                ViewBag.PlatformOrderCode = inModel.PlatformOrderCode;
            }
            return mr;
        }

        private MedicalRecord_File getFileMr(MedicalRecordBase model)
        {
            ICode_Order access = new DAL_Code_Order();
            MedicalRecord_File mr = null;
            var inModel = access.GetCodeOrder(model.CodeOrderID);
            if (inModel != null)
            {
                var bytes = FileHelper.GetFile(inModel.MedicalRecordPath);
                var str = Encoding.UTF8.GetString(bytes);
                mr = JsonHelper.DeserializeObject<MedicalRecord_File>(str);
                if (mr != null)
                {
                    mr.OperationCodeResultList = model.OperationCodeResultList;
                    mr.DiagnosisCodeResultList = model.DiagnosisCodeResultList;
                    mr.CodeOrderID = model.CodeOrderID;
                    mr.LastModifyStamp = inModel.LastModifyStamp;
                    mr.MedicalRecordPath = inModel.MedicalRecordPath;
                }
                ViewBag.PlatformOrderCode = inModel.PlatformOrderCode;
            }
            return mr;
        }

        private void initialOperate(CodeOrderModel model)
        {
            if (model.OperationCodeResultList == null || model.OperationCodeResultList.Count == 0)
            {
                model.OperationCodeResultList = new List<OperationCodeResult> {
                    new OperationCodeResult{},
                    new OperationCodeResult{},};
                return;
            }
            var newOperateList = new List<OperationCodeResult>();
            var maxIndex = model.OperationCodeResultList.Max(e => e.Index) > 2 ? model.OperationCodeResultList.Max(e => e.Index) : 2;
            for (int i = 0; i < maxIndex; i++)
            {
                var item = model.OperationCodeResultList.FirstOrDefault(e => e.Index == i);
                if (item != null)
                {
                    newOperateList.Add(item);
                }
                else
                {
                    newOperateList.Add(new OperationCodeResult());
                }
            }
            model.OperationCodeResultList = newOperateList;
        }

        private void initalDiagnosis(CodeOrderModel model)
        {
            if (model.DiagnosisCodeResultList == null || model.DiagnosisCodeResultList.Count == 0)
            {
                model.DiagnosisCodeResultList = new List<DiagnosisCodeResult> {
                    new DiagnosisCodeResult{Description="主要诊断"},
                    new DiagnosisCodeResult{Description="其他诊断"},
                };
                return;
            }
            var newDiagnosisList = new List<DiagnosisCodeResult>();
            var maxIndex = model.DiagnosisCodeResultList.Max(e => e.Index) > 2 ? model.DiagnosisCodeResultList.Max(e => e.Index) : 2;
            for (int i = 0; i < maxIndex; i++)
            {
                var item = model.DiagnosisCodeResultList.FirstOrDefault(e => e.Index == i);
                if (item != null)
                {
                    newDiagnosisList.Add(item);
                }
                else
                {
                    newDiagnosisList.Add(new DiagnosisCodeResult { Description = i == 0 ? "主要诊断" : (i == 1 ? "其他诊断" : null) });
                }
            }
            model.DiagnosisCodeResultList = newDiagnosisList;
        }

        private MedicalRecord_Data initalCode(MedicalRecord_Data mr, int diagnosisVersionId, int operateVersionId)
        {
            if (mr.DiagnosisCodeResultList == null || mr.DiagnosisCodeResultList.Count == 0)
            {
                //初始化诊断编码结果
                if (mr.DischargeDiagnosisList != null)//首页有结果直接取首页结果
                {
                    mr.DiagnosisCodeResultList = mr.DischargeDiagnosisList
                        .Select(e => new DiagnosisCodeResult { Index = e.Index, Diagnosis = e.Diagnosis, DisplayText = ICDVersionList.GetCodeDispalyTextByClinicName(diagnosisVersionId, e.Diagnosis) }).ToList();
                }
                else if (mr.DischargeRecord != null && mr.DischargeRecord.DischargeDiagnosis != null)//首页没结果去出院记录或者死亡记录中取结果
                {
                    mr.DiagnosisCodeResultList = mr.DischargeRecord.DischargeDiagnosis.Split(';').Where(e => !string.IsNullOrWhiteSpace(e))
                        .Select(t => new DiagnosisCodeResult { Index = 1, Diagnosis = t, DisplayText = ICDVersionList.GetCodeDispalyTextByClinicName(diagnosisVersionId, t) }).ToList();
                }
                else if (mr.DeathNote != null && mr.DeathNote.DeathDiagnosis != null)
                {
                    mr.DiagnosisCodeResultList = mr.DeathNote.DeathDiagnosis.Split(';').Where(e => !string.IsNullOrWhiteSpace(e))
                        .Select(t => new DiagnosisCodeResult { Index = 1, Diagnosis = t, DisplayText = ICDVersionList.GetCodeDispalyTextByClinicName(diagnosisVersionId, t) }).ToList();
                }
            }
            if (mr.OperationCodeResultList == null || mr.OperationCodeResultList.Count == 0)
            {
                //初始化手术操作编码结果
                if (mr.ProceduresList != null)//首页有结果直接取首页结果
                {
                    mr.OperationCodeResultList = mr.ProceduresList
                        .Select(e => new OperationCodeResult { Index = e.Index, OperationName = e.OperationName, DisplayText = ICDVersionList.GetCodeDispalyTextByClinicName(operateVersionId, e.OperationName) }).ToList();
                }
                else if (mr.OperationDetailList != null)//先去手术详细记录中取手术名称
                {
                    mr.OperationCodeResultList = mr.OperationDetailList
                        .Select(e => new OperationCodeResult { OperationName = e.OperationName, DisplayText = ICDVersionList.GetCodeDispalyTextByClinicName(operateVersionId, e.OperationName) }).ToList();
                    //todo   需要调研临时医嘱中操作的进入首页结果中
                }
            }
            return mr;
        }

        private MedicalRecord_File initalCode(MedicalRecord_File model)
        {
            if (model.DiagnosisCodeResultList == null || model.DiagnosisCodeResultList.Count == 0)
            {
                model.DiagnosisCodeResultList = new List<DiagnosisCodeResult> {
                    new DiagnosisCodeResult{Description="主要诊断"},
                    new DiagnosisCodeResult{Description="其他诊断"},
                };
            }
            if (model.OperationCodeResultList == null || model.OperationCodeResultList.Count == 0)
            {
                model.OperationCodeResultList = new List<OperationCodeResult> {
                    new OperationCodeResult{},
                    new OperationCodeResult{},};
            }
            return model;
        }

        private ActionResult returnOriginModel(CodeOrderModel model, ICode_Order access, string userID)
        {
            var newModel = access.GetCodeOrderDetail(userID, model.CodeOrderID);
            newModel.OperationCodeResultList = model.OperationCodeResultList;
            newModel.DiagnosisCodeResultList = model.DiagnosisCodeResultList;
            return View(newModel);
        }

        private string validateCodeModel(int orderID, List<DiagnosisCodeResult> diagnosisList,
            List<OperationCodeResult> opetateList, string userId)
        {
            ICode_Order access = new DAL_Code_Order();
            if (!access.CanCodingMR(orderID, userId))
            {
                return "您无权编辑此订单";
            }
            if (diagnosisList.All(e => string.IsNullOrWhiteSpace(e.DisplayText)))
            {
                return "请输入至少一个诊断";
            }
            if (opetateList.All(e => string.IsNullOrWhiteSpace(e.DisplayText)))
            {
                return "请输入至少一个手术或操作";
            }
            for (int i = 0; i < diagnosisList.Count; i++)
            {
                var item = diagnosisList[i]; if (!string.IsNullOrWhiteSpace(item.DisplayText))
                {
                    var temp = item.DisplayText.Split('-');
                    if (temp.Length == 2)
                    {
                        item.ICDCode = temp[0];
                        item.ICDName = temp[1];
                        item.Index = i;
                        item.CreateTime = DateTime.Now;
                        item.CreateUserID = userId;
                        item.LastModifyTime = DateTime.Now;
                        item.LastModifyUserID = userId;
                    }
                    else
                    {
                        return "请输入合法的诊断";
                    }
                }
            }
            for (int i = 0; i < opetateList.Count; i++)
            {
                var item = opetateList[i];
                if (!string.IsNullOrWhiteSpace(item.DisplayText))
                {
                    var temp = item.DisplayText.Split('-');
                    if (temp.Length == 2)
                    {
                        item.ICDCode = temp[0];
                        item.ICDName = temp[1];
                        item.Index = i;
                        item.CreateTime = DateTime.Now;
                        item.CreateUserID = userId;
                        item.LastModifyTime = DateTime.Now;
                        item.LastModifyUserID = userId;
                    }
                    else
                    {
                        return "请输入合法的手术或操作";
                    }
                }
            }
            return string.Empty;
        }

        #endregion
    }
}