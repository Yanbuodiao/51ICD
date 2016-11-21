using Docimax.Common;
using Docimax.Common_ICD.File;
using Docimax.Common_ICD.Verify;
using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Message;
using Docimax.Interface_ICD.Model;
using Docimax.Interface_ICD.Model.Log;
using Docimax.Interface_ICD.Model.UploadModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Docimax.Web_Interface.Controllers
{
    public class ICDCoderController : BaseController
    {
        [HttpPost]
        public ActionResult NewCodeOrder()
        {
            var requestModel = UploadModel<MedicalRecordCoding>.DicToModel(ViewBag.Decrypt as Dictionary<string, string>);

            #region 校验数据

            var verifyResult = BaseVerify.RequiredValidate<MedicalRecordCoding>(requestModel);
            if (!verifyResult.IsSuccess)
            {
                return base.buildResponseAndLog(verifyResult.ErrorStr, (ViewBag.log as BaseLog));
            }
            var medicalRecord = requestModel.UploadContent;
            //todo  根据ticketID 查询回复的信息  能查到  直接返回
            verifyResult = Verify(medicalRecord);
            if (!verifyResult.IsSuccess)
            {
                return base.buildResponseAndLog(verifyResult.ErrorStr, (ViewBag.log as BaseLog));
            }

            #endregion

            var medicalRecordJsonStr = JsonHelper.SerializeObject(medicalRecord);
            IFile fileAccess = new File_Azure();
            var medicalRecordPath = fileAccess.SaveMedicalFile(Encoding.UTF8.GetBytes(medicalRecordJsonStr), buildVirtualPath(medicalRecord, ViewBag.AUTHCode));
            medicalRecord.OrderType = OrderTypeEnum.InterfaceOrder;
            ICode_Order access = new DAL_Code_Order();
            ExcuteResult saveResult = access.SaveCodeOrder(medicalRecord, ViewBag.AUTHCode, medicalRecordPath);
            if (!saveResult.IsSuccess)
            {
                return base.buildResponseAndLog(saveResult.ErrorStr, (ViewBag.log as BaseLog));
            }
            //todo 返回同步通知
            return base.buildResponseAndLog(MessageStr.SyncStr, (ViewBag.log as BaseLog));
        }

        private ExcuteResult Verify(MedicalRecordCoding medicalRecord)
        {
            var verifyResult = BaseVerify.ValidateNewCodeOrder(medicalRecord);
            if (!verifyResult.IsSuccess)
            {
                return verifyResult;
            }
            var tempkey = string.Format("{0}{1:yyyyMMdd}{2}", medicalRecord.MedicalRecordNO, medicalRecord.DischargeDate, medicalRecord.AdmissionTimes);
            if (HttpRuntime.Cache[tempkey] != null)
            {
                return new ExcuteResult { ErrorStr = MessageStr.MedicalRecordRepeat };
            }
            HttpRuntime.Cache.Insert(tempkey, 0, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 20));
            ICode_Order access = new DAL_Code_Order();
            if (access.IsCodeOrderExistByMecicalRecord(medicalRecord))
            {
                return new ExcuteResult { ErrorStr = MessageStr.MedicalRecordRepeat };
            }
            return new ExcuteResult { IsSuccess = true };
        }

        private string buildVirtualPath(MedicalRecordCoding medicalRecord, string authCode)
        {
            return string.Format("{0}/{1}/{2:yyyyMMdd}_{3}.txt",
                authCode,
                medicalRecord.MedicalRecordNO,
                medicalRecord.DischargeDate,
                medicalRecord.AdmissionTimes);
        }
    }
}