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
using System.IO;
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
            var requestModel = UploadModel<MedicalRecord_Data>.DicToModel(ViewBag.Decrypt as Dictionary<string, string>);

            #region 校验数据

            var verifyResult = BaseVerify.RequiredValidate<MedicalRecord_Data>(requestModel);
            if (!verifyResult.IsSuccess)
            {
                return buildResponseAndLog(verifyResult.ErrorStr, (ViewBag.log as BaseLog));
            }
            var medicalRecord = requestModel.UploadContent;
            //todo  根据ticketID 查询回复的信息  能查到  直接返回
            verifyResult = BaseVerify.ValidateCodeOrder(medicalRecord);
            if (!verifyResult.IsSuccess)
            {
                return buildResponseAndLog(verifyResult.ErrorStr, (ViewBag.log as BaseLog));
            }

            #endregion

            var medicalRecordJsonStr = JsonHelper.SerializeObject(medicalRecord);
            IFile fileAccess = new File_Azure();
            var medicalRecordPath = fileAccess.SaveMedicalFile(Encoding.UTF8.GetBytes(medicalRecordJsonStr),
               string.Format("{0}.txt", buildDataOrderVirtualPath(medicalRecord, ViewBag.AUTHCode)));
            medicalRecord.OrderType = OrderTypeEnum.InterfaceOrder;
            ICode_Order access = new DAL_Code_Order();
            ExcuteResult saveResult = access.SaveCodeOrder(medicalRecord, ViewBag.AUTHCode, medicalRecordPath);
            if (!saveResult.IsSuccess)
            {
                return base.buildResponseAndLog(saveResult.ErrorStr, (ViewBag.log as BaseLog));
            }
            return base.buildResponseAndLog(MessageStr.SyncStr, (ViewBag.log as BaseLog));
        }

        [HttpPost]
        public ActionResult FileCodeOrder()
        {
            var requestModel = UploadModel<MedicalRecord_File>.DicToModel(ViewBag.Decrypt as Dictionary<string, string>);

            #region 校验数据

            var verifyResult = BaseVerify.RequiredValidate<MedicalRecord_File>(requestModel);
            if (!verifyResult.IsSuccess)
            {
                return buildResponseAndLog(verifyResult.ErrorStr, (ViewBag.log as BaseLog));
            }
            var medicalRecord = requestModel.UploadContent;
            //todo  根据ticketID 查询回复的信息  能查到  直接返回
            verifyResult = BaseVerify.ValidateCodeOrder(medicalRecord);
            if (!verifyResult.IsSuccess)
            {
                return buildResponseAndLog(verifyResult.ErrorStr, (ViewBag.log as BaseLog));
            }

            #endregion

            IFile fileAccess = new File_Azure();
            string virtualPath = buildDataOrderVirtualPath(medicalRecord, ViewBag.AUTHCode);
            if (requestModel.UploadContent.Catalogs != null)
            {
                requestModel.UploadContent.Catalogs.ForEach(e => handleFile(e, fileAccess, virtualPath));
            }
            var medicalRecordJsonStr = JsonHelper.SerializeObject(medicalRecord);
            var medicalRecordPath = fileAccess.SaveMedicalFile(Encoding.UTF8.GetBytes(medicalRecordJsonStr),
               string.Format("{0}.txt", buildDataOrderVirtualPath(medicalRecord, ViewBag.AUTHCode)));
            medicalRecord.OrderType = OrderTypeEnum.InterfaceFileOrder;
            ICode_Order access = new DAL_Code_Order();
            ExcuteResult saveResult = access.SaveCodeOrder(medicalRecord, ViewBag.AUTHCode, medicalRecordPath);
            if (!saveResult.IsSuccess)
            {
                return base.buildResponseAndLog(saveResult.ErrorStr, (ViewBag.log as BaseLog));
            }
            return base.buildResponseAndLog(MessageStr.SyncStr, (ViewBag.log as BaseLog));
        }

        private string buildDataOrderVirtualPath(MedicalRecordBase medicalRecord, string authCode)
        {
            return string.Format("{0}/{1}/{2:yyyyMMdd}_{3}",
                authCode,
                medicalRecord.MedicalRecordNO,
                medicalRecord.DischargeDate,
                medicalRecord.AdmissionTimes);
        }

        private void handleFile(Catalog catalog, IFile fileAccess, string virtualPath)
        {
            if (catalog == null)
            {
                return;
            }
            if (catalog.CatalogFiles == null)
            {
                return;
            }
            catalog.CatalogFiles.ForEach(e =>
            {
                if (e.InputStream == null || e.InputStream.Length == 0)
                {
                    return;
                }
                var path = string.Format("{0}/{1}/{2}_{3}", virtualPath, catalog.CatalogName, e.FileOrder, e.FileName);
                e.FileURL = fileAccess.SaveMedicalFile(e.InputStream, path);
                string extension = Path.GetExtension(e.FileName);
                e.InputStream = null;//文件上传成功后 及时释放内存
                e.ConentType = ContentTypeCompare.GetContentType(extension);
            });
            if (catalog.SubCatalog != null)
            {
                catalog.SubCatalog.ForEach(e => handleFile(e, fileAccess, virtualPath));
            }
        }
    }
}