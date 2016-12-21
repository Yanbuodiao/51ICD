
using Docimax.Data_ICD.Entity;
using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Docimax.Data_ICD.DAL
{
    public class DAL_ICDRepository : IICDRepository
    {
        public List<ICDModel> GetICD_Diagnosis_ModelList(string queryStr, int icd_VersionID = 1)
        {
            using (var entity = new Entity_Read())
            {
                if (string.IsNullOrWhiteSpace(queryStr))
                {
                    return new List<ICDModel>();
                }
                queryStr = queryStr.Trim();
                var result = entity.BaseDic_ICD_Diagnosis_Repository.Where(e => e.ICD_VersionID == icd_VersionID &&
                    (e.ICD_Code.StartsWith(queryStr) ||
                     e.ICD_Name.StartsWith(queryStr) ||
                     e.PinyinShort.StartsWith(queryStr))).Select(e => new
                 {
                     e.ICDID,
                     e.ICD_Code,
                     e.ICD_Name,
                     e.ICD_VersionID,
                     e.ICD_Description,
                     e.PinyinShort,
                 }).Take(10).ToList();

                return result.Select(e => new ICDModel
                {
                    ICDID = e.ICDID,
                    ICD_Code = e.ICD_Code,
                    ICD_Name = e.ICD_Name,
                    ICD_VersionID = e.ICD_VersionID ?? 0,
                    ICD_Description = e.ICD_Description,
                    PinyinShort = e.PinyinShort,
                }).ToList();
            }
        }
        public List<ICDModel> GetICD_Operate_ModelList(string queryStr, int icd_VersionID = 2)
        {
            using (var entity = new Entity_Read())
            {
                if (string.IsNullOrWhiteSpace(queryStr))
                {
                    return new List<ICDModel>();
                }
                queryStr = queryStr.Trim();
                var result = entity.BaseDic_ICD_Operate_Repository.Where(e => e.ICD_VersionID == icd_VersionID &&
                    (e.ICD_Code.StartsWith(queryStr) ||
                     e.ICD_Name.StartsWith(queryStr) ||
                     e.PinyinShort.StartsWith(queryStr))).Select(e => new
                     {
                         e.ICDID,
                         e.ICD_Code,
                         e.ICD_Name,
                         e.ICD_VersionID,
                         e.ICD_Description,
                         e.PinyinShort,
                     }).Take(10).ToList();

                return result.Select(e => new ICDModel
                {
                    ICDID = e.ICDID,
                    ICD_Code = e.ICD_Code,
                    ICD_Name = e.ICD_Name,
                    ICD_VersionID = e.ICD_VersionID ?? 0,
                    ICD_Description = e.ICD_Description,
                    PinyinShort = e.PinyinShort,
                }).ToList();
            }
        }
        public ICDVersionModel GetICDVersionWithICD(int icd_VersionID)
        {
            using (var entity = new Entity_Read())
            {
                var icdVersion = entity.BaseDic_ICD_Version.FirstOrDefault(e => e.ICD_VersionID == icd_VersionID);
                if (icdVersion == null)
                {
                    return null;
                }
                var resultOperate = entity.BaseDic_ICD_Operate_Repository.Where(e => e.ICD_VersionID == icd_VersionID).ToList();
                var resultDiagnosis = entity.BaseDic_ICD_Diagnosis_Repository.Where(e => e.ICD_VersionID == icd_VersionID).ToList();
                var result = new ICDVersionModel
                {
                    ICD_VersionID = icdVersion.ICD_VersionID,
                    ICD_VersionName = icdVersion.ICD_VersionName,
                    ICD_Description = icdVersion.ICD_Description,
                };
                if (resultOperate != null && resultOperate.Count > 0)
                {
                    result.ICDList = resultOperate.Select(e => new ICDModel
                    {
                        ICDID = e.ICDID,
                        ICD_Code = e.ICD_Code,
                        ICD_Name = e.ICD_Name,
                        ICD_VersionID = e.ICD_VersionID ?? 0,
                        ICD_Description = e.ICD_Description,
                        PinyinShort = e.PinyinShort,
                    }).ToList();
                }
                if (resultDiagnosis != null && resultDiagnosis.Count > 0)
                {
                    result.ICDList = resultDiagnosis.Select(e => new ICDModel
                    {
                        ICDID = e.ICDID,
                        ICD_Code = e.ICD_Code,
                        ICD_Name = e.ICD_Name,
                        ICD_VersionID = e.ICD_VersionID ?? 0,
                        ICD_Description = e.ICD_Description,
                        PinyinShort = e.PinyinShort,
                    }).ToList();
                }
                return result;
            }
        }
        public List<ICDVersionModel> GetICDVersionList(int icdType)
        {
            using (var entity = new Entity_Read())
            {
                var query = entity.BaseDic_ICD_Version.Where(e => e.ICD_Type == icdType).ToList();
                return query.Select(e => new ICDVersionModel
                {
                    ICD_VersionID = e.ICD_VersionID,
                    ICD_Type = icdType,
                    ICD_VersionName = e.ICD_VersionName,
                    ICD_Description = e.ICD_Description,
                }).ToList();
            }
        }
        public ICDModel GetICDModel(int icdID, ICDTypeEnum icdType)
        {
            switch (icdType)
            {
                case ICDTypeEnum.诊断编码:
                    return getDiagnosisICD(icdID);
                case ICDTypeEnum.手术及操作编码:
                    return getOperateICD(icdID);
            }
            return null;
        }

        public ICDExcuteResult<string> UpdateICDSummary(ICDModel model)
        {
            if (model == null)
            {
                return new ICDExcuteResult<string> { IsSuccess = false, ErrorStr = "未找到相关记录" };
            }
            switch (model.ICDType)
            {
                case ICDTypeEnum.诊断编码:
                    return updateDiagnosisSummary(model);
                case ICDTypeEnum.手术及操作编码:
                    return updateOperateSummary(model);
            }
            return new ICDExcuteResult<string> { IsSuccess = false, ErrorStr = "未找到相关记录" };
        }

        #region 私有方法
        private ICDModel getDiagnosisICD(int icdID)
        {
            using (var entity = new Entity_Read())
            {
                var model = entity.BaseDic_ICD_Diagnosis_Repository.FirstOrDefault(e => e.ICDID == icdID);
                if (model != null)
                {
                    return new ICDModel
                    {
                        ICDID = icdID,
                        ICD_Code = model.ICD_Code,
                        ICD_Name = model.ICD_Name,
                        ICD_Description = model.ICD_Description,
                        LastModifyStamp = Convert.ToBase64String(model.LastModifyStamp),
                    };
                }
            }
            return null;
        }

        private ICDModel getOperateICD(int icdID)
        {
            using (var entity = new Entity_Read())
            {
                var model = entity.BaseDic_ICD_Operate_Repository.FirstOrDefault(e => e.ICDID == icdID);
                if (model != null)
                {
                    return new ICDModel
                    {
                        ICDID = icdID,
                        ICD_Code = model.ICD_Code,
                        ICD_Name = model.ICD_Name,
                        ICD_Description = model.ICD_Description,
                        LastModifyStamp = Convert.ToBase64String(model.LastModifyStamp),
                    };
                }
            }
            return null;
        }

        private ICDExcuteResult<string> updateDiagnosisSummary(ICDModel model)
        {
            using (var entity = new Entity_Write())
            {
                using (var trasanction = entity.Database.BeginTransaction())
                {
                    try
                    {
                        var entityModel = entity.BaseDic_ICD_Diagnosis_Repository.FirstOrDefault(e => e.ICDID == model.ICDID);
                        if (entityModel == null)
                        {
                            return new ICDExcuteResult<string> { IsSuccess = false, ErrorStr = "未找到相关记录" };
                        }
                        if (model.LastModifyStamp != Convert.ToBase64String(entityModel.LastModifyStamp))
                        {
                            return new ICDExcuteResult<string> { IsSuccess = false, ErrorStr = "有人快您一步，已经更新" };
                        }
                        entityModel.ICD_Description = model.ICD_Description;
                        entity.SaveChanges();
                        trasanction.Commit();
                        return new ICDExcuteResult<string> { IsSuccess = true, TResult = Convert.ToBase64String(entityModel.LastModifyStamp), };
                    }
                    catch (Exception ex)
                    {
                        //todo 记录相关日志
                        trasanction.Rollback();
                        return new ICDExcuteResult<string> { IsSuccess = false, ErrorStr = "系统罢工了" };
                    }

                }
            }
        }
        private ICDExcuteResult<string> updateOperateSummary(ICDModel model)
        {
            using (var entity = new Entity_Write())
            {
                using (var trasanction = entity.Database.BeginTransaction())
                {
                    try
                    {
                        var entityModel = entity.BaseDic_ICD_Operate_Repository.FirstOrDefault(e => e.ICDID == model.ICDID);
                        if (entityModel == null)
                        {
                            return new ICDExcuteResult<string> { IsSuccess = false, ErrorStr = "未找到相关记录" };
                        }
                        if (model.LastModifyStamp != Convert.ToBase64String(entityModel.LastModifyStamp))
                        {
                            return new ICDExcuteResult<string> { IsSuccess = false, ErrorStr = "有人快您一步，已经更新" };
                        }
                        entityModel.ICD_Description = model.ICD_Description;
                        entity.SaveChanges();
                        trasanction.Commit();
                        return new ICDExcuteResult<string> { IsSuccess = true, TResult = Convert.ToBase64String(entityModel.LastModifyStamp), };
                    }
                    catch (Exception ex)
                    {
                        //todo 记录相关日志
                        trasanction.Rollback();
                        return new ICDExcuteResult<string> { IsSuccess = false, ErrorStr = "系统罢工了" };
                    }
                }
            }
        }

        #endregion
    }
}