
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

        public List<ICDModel> GetICD_Operate_ModelList(string queryStr, int icd_VersionID=2)
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
    }
}