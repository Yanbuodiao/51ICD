
using Docimax.Data_ICD.Entity;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
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
    }
}